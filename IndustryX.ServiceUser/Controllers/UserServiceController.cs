using IndustryX.ServiceUser.DAL;
using IndustryX.ServiceUser.Models;
using IndustryX.ServiceUser.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace IndustryX.ServiceUser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserServiceController : ControllerBase
    {    
        private readonly ILogger<UserServiceController> _logger;
        private readonly MongoDBService _mongoDBService;
        UserService userService = new UserService();
        public UserServiceController(ILogger<UserServiceController> logger, MongoDBService mongoDBService)
        {
            _logger = logger;
            _mongoDBService = mongoDBService;
        }
        [HttpGet]
        [Route("GetUserById")]
        public Task<User> GetUserById(int id) => Task.FromResult(userService.GetUserById(id));
        [HttpGet]
        [Route("GetUserByName")]
        public Task<User> GetUserByName(string username) => Task.FromResult(userService.GetUserByUserName(username));
        [HttpPost]
        [Route("AddUser")]
        public ObjectResult AddUser(User user)
        {
            try { userService.CreateUser(user); }
            catch (Exception ex) { return BadRequest(ex.Message);}
            return Ok(true);
        }
        [HttpPost]
        [Route("DeleteUser")]
        public ObjectResult DeleteUser(int id)
        {
            try { userService.DeleteUser(id); }
            catch (Exception ex) { return BadRequest(ex.Message); }
            return Ok(true);
        }
        [HttpGet]
        [Route("GetStatusByUserName")]
        public Task<User> GetStatusByUserName(string username) => Task.FromResult(userService.GetStatusByUserName(username));
        
    }
}
