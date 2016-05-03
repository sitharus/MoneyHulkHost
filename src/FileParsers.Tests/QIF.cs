using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FileParsers.Tests
{
    public class QIF
    {
        public static IEnumerable<object[]> SampleQIF = new List<object[]>  {
            new object[] {
            @"!Type:Bank
D04/03/16
MMOORE WILSONS
T-9.42
^
D04/03/16
MAP#7592197 FROM P J HUTCHINGS ;Transfer from P J HUTCHINGS - 02
T250.00
^"
            }
        };

        [Theory]
        [MemberData("SampleQIF")]
        public async Task ParsesBasicQIF(string data)
        {
            var result = await FileParsers.QIF.Parse(data);
            Assert.NotNull(result);
        }
    }
}
