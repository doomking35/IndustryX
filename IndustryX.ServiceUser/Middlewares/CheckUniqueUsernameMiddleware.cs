using Amazon.Runtime.Internal;
using IndustryX.ServiceUser.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;

namespace IndustryX.ServiceUser.Middlewares
{
    public class CheckUniqueUsernameMiddleware
    {
        private readonly RequestDelegate _next;

        public static readonly ConfigurationBuilder configuration = (ConfigurationBuilder)new ConfigurationBuilder().AddJsonFile("appsettings.json");
        public static readonly IConfigurationRoot configurationRoot = configuration.Build();
        static readonly string connectionString = configurationRoot["ConnectionStrings:mongoDB"];
        private readonly IMongoCollection<User> client = new MongoClient(connectionString).GetDatabase("USERDB").GetCollection<User>("users");

        public CheckUniqueUsernameMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/UserService/AddUser" && context.Request.Method == "POST")
            {
                try
                {
                    #region RequestStreamRegion...
                    var RequestBody = new StreamReader(context.Request.BodyReader.AsStream()).ReadToEnd();
                    byte[] content1 = Encoding.UTF8.GetBytes(RequestBody.Replace("string", "NoString"));
                    var bodyAsText = Encoding.Default.GetString(content1);
                    var requestBodyStream = new MemoryStream();
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    requestBodyStream.Write(content1, 0, content1.Length);
                    context.Request.Body = requestBodyStream;
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                    #endregion

                    var userData = JsonConvert.DeserializeObject<User>(bodyAsText);

                    bool isValidEmail = IsValidEmail(userData.Email);
                    bool isUniqueUsername = IsUniqueUsername(userData.UserName.ToLower());
                    bool isUniqueEmail = IsUniqueEmail(userData.Email);
                    #region UniqueRegion...
                    if (isUniqueUsername && isUniqueEmail && isValidEmail)
                    {
                        context.Request.EnableBuffering();
                        await _next(context);
                    }
                    else
                    {
                        //TODO : Error Handling error message should be in JSON format
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var errorMessage = "";
                        errorMessage = !isUniqueUsername ? errorMessage + "{\"error\": \"Username already taken!\"}" : "";
                        errorMessage = !isUniqueEmail ? errorMessage + "{\"error\": \"E-mail already taken!\"}" : errorMessage;
                        errorMessage = !isValidEmail  ? errorMessage + "{\"error\": \"Invalid E-Mail adress!\"}" : errorMessage;
                        await context.Response.WriteAsync(errorMessage);
                    }
                    #endregion
                }
                catch (JsonException)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("{\"error\": \"Geçersiz JSON formatı\"}");
                }
            }
            else
            {
                await _next(context);
            }
        }

        private bool IsUniqueUsername(string username) => client.Find(u => u.UserName.ToLower() == username).FirstOrDefault() == null;
        private bool IsUniqueEmail(string email) => client.Find(u => u.Email == email).FirstOrDefault() == null;
        private bool IsValidEmail(string email) 
        {
            try {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException) { return false; }         
        }
    }
    public static class CheckUniqueUsernameMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckUniqueUsernameMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckUniqueUsernameMiddleware>();
        }
    }
}
