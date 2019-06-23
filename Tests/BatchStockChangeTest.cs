using FELFEL.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Inventory.test
{
    public class BatchStockChangeTest
    {
        [Theory]
        [InlineData(30, 25, -5)]
        [InlineData(10, 30, 20)]
        [InlineData(40, 50, 10)]
        [InlineData(50, 40, -10)]
        public void Difference_ShouldReturnExpectedValue(int oldAmount, int newAmount, int expectedDiff)
        {
            var change = new BatchStockChange
            {
                OldAmount = oldAmount,
                NewAmount = newAmount,
            };

            int actualDiff = change.AmountDifference;



            Assert.Equal(expectedDiff, actualDiff);
        }
    }
}
