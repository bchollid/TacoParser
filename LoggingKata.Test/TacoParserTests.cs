using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            var tacoParser = new TacoParser();

            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("34.271508,-84.798907,Taco Bell Cartersville...", -84.798907)]
        [InlineData("30.357759,-87.163888,Taco Bell Gulf Breez...",-87.163888)]
        public void ShouldParseLongitude(string line, double expected)
        {
            var newTacoParser = new TacoParser();

            var actual = newTacoParser.Parse(line);

            Assert.Equal(expected, actual.Location.Longitude);
        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", 34.073638)]
        [InlineData("34.271508,-84.798907,Taco Bell Cartersville...", 34.271508)]
        [InlineData("30.357759,-87.163888,Taco Bell Gulf Breez...", 30.357759)]
        public void ShouldParseLatitude(string line, double expected)
        {
            var newTacoParser = new TacoParser();

            var actual = newTacoParser.Parse(line);

            Assert.Equal(expected, actual.Location.Latitude);
        }
    }
}
