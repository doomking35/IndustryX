using System.Globalization;
using Xunit;

namespace IndustryX.XUnitTest
{
    public class UnitTestInit
    {
        [Fact]
        public void ToPrettyDate_ShouldArgumentNullException_WhenCultureIsNull()
        {
            var result = Record.Exception(() => DateTime.Now.ToPrettyDate(null));
            Assert.NotNull(result);
            var exception = Assert.IsType<ArgumentNullException>(result);
            var actual = exception.ParamName;
            const string expected = "culture";
            Assert.Equal(expected, actual);
        }

        [Theory, InlineData(new object[] { "de-DE", "2017.12.19", "19 Dezember 2017" })]
        public void ToPrettyDate_ShouldAssertTrue_WhenCultureIsGerman(string cultureCode, string textDate, string expected)
        {
            var culture = CultureInfo.CreateSpecificCulture(cultureCode);
            var date = DateTime.ParseExact(textDate, "yyyy.MM.dd", culture);
            var actual = date.ToPrettyDate(culture);
            Assert.Equal(expected, actual);
        }


        [Theory, ClassData(typeof(CultureTestTheoryData))]
        public void ToPrettyDate_ShouldAssertTrue_WhenCultureIsDefined(CultureTestParameter parameter)
        {
            var actual = parameter.Actual.ToPrettyDate(parameter.Culture);
            var expected = parameter.Expected;
            Assert.Equal(expected, actual);
        }
    }

    public static class DateTimeExtensions
    {
        public static string ToPrettyDate(this DateTime date, CultureInfo? culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }
            return date.ToString("dd MMMM yyyy", culture);
        }
    }

    public class CultureTestParameter
    {
        public required CultureInfo Culture { get; set; }
        public DateTime Actual { get; set; }
        public required string Expected { get; set; }
        public static IEnumerable<object[]> StaticParameter => new List<CultureTestParameter[]>
    {
        new CultureTestParameter[]
        {
            new CultureTestParameter
            {
                Culture = CultureInfo.CreateSpecificCulture("it-IT"),
                Actual = new DateTime(1988,06,05),
                Expected = "05 giugno 1988"
            }
        }
    };

        [Theory, MemberData(nameof(StaticParameter))]
        public void ToPrettyDate_ShouldAssertTrue_WhenCultureIsItalian(CultureTestParameter parameter)
        {
            var actual = parameter.Actual.ToPrettyDate(parameter.Culture);
            var expected = parameter.Expected;
            Assert.Equal(expected, actual);
        }
    }

    public class CultureTestTheoryData : TheoryData<CultureTestParameter>
    {
        public CultureTestTheoryData()
        {
            Add(new CultureTestParameter
            {
                Culture = CultureInfo.CreateSpecificCulture("tr-TR"),
                Actual = new DateTime(2024, 03, 11),
                Expected = "11 Mart 2024"
            });
            Add(new CultureTestParameter
            {
                Culture = CultureInfo.CreateSpecificCulture("en-US"),
                Actual = new DateTime(1987, 08, 13),
                Expected = "13 August 1987"
            });
            Add(new CultureTestParameter
            {
                Culture = CultureInfo.CreateSpecificCulture("it-IT"),
                Actual = new DateTime(1987, 08, 13),
                Expected = "13 agosto 1987"
            });
        }
    }
}
