using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FileParsers.Tests
{
    
    public class QIF
    {


        [Fact]
        public async Task ParsesBasicQIF()
        {
            var result = await FileParsers.QIF.Parse(@"!Type:Bank
D04/03/16
MMOORE WILSONS
T-9.42
^");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FailsOnInvalidType()
        {
            await Assert.ThrowsAnyAsync<UnsupportedQIFTypeExecption>(async () => await FileParsers.QIF.Parse("!Type:RandomThing"));
        }

        [Fact]
        public async Task FailsWithoutType()
        {
            await Assert.ThrowsAnyAsync<UnsupportedQIFTypeExecption>(async () => await FileParsers.QIF.Parse("D04/03/16"));
        }


        const string qifWithInvalidLine = @"
!Type:Bank
D04/03/16
MMOORE WILSONS
T-9.42
XBAR
^";
        [Fact]
        public async Task FailsOnUnknownLine()
        {

            await Assert.ThrowsAnyAsync<InvalidQIFException>(async () => await FileParsers.QIF.Parse(qifWithInvalidLine));

        }

        [Fact]
        public async Task ReadsPositiveLines()
        {
            var result = await FileParsers.QIF.Parse(@"!Type:Bank
D04/03/16
MMOORE WILSONS
T50.00
^");
            Assert.Single(result.Transactions);
            var transaction = result.Transactions.First();
            Assert.Equal(transaction.Date, new DateTime(2016, 3, 4));
            Assert.Equal(transaction.Memo, "MOORE WILSONS");
            Assert.Equal(transaction.Amount, 50m);

        }

        [Fact]
        public async Task ReadsNegativeLines()
        {
            var result = await FileParsers.QIF.Parse(@"!Type:Bank
D29/02/16
MMOORE WILSONS
T-9.42
^");
            Assert.Single(result.Transactions);
            var transaction = result.Transactions.First();
            Assert.Equal(transaction.Date, new DateTime(2016, 2, 29));
            Assert.Equal(transaction.Memo, "MOORE WILSONS");
            Assert.Equal(transaction.Amount, -9.42m);

        }
    }
}
