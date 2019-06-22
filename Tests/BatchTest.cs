using FELFEL.Domain;
using System;
using Xunit;

namespace Inventory.test
{
    public class BatchTest :IDisposable
    {
        private Batch batch;

        public BatchTest()
        {
            var product = new Product
            {
                Id = 1
            };

            batch = new Batch(product, DateTime.Now, 100);

        }

        public void Dispose()
        {
            batch = null;
        }


        [Theory]
        [InlineData(21, false)]
        [InlineData(-4, true)]
        public void IsExpired_ShouldReturnExpectedValue(int days, bool expected)
        {

            batch.Expiration = batch.Expiration.AddDays(days);

            Assert.Equal(batch.IsExpired, expected);
        }

        [Fact]
        public void State_ShouldBeFresh()
        {
            batch.Expiration = batch.Expiration.AddDays(15);

            Assert.Equal(BatchState.fresh, batch.State);
        }

        [Fact]
        public void State_ShouldBeExpiring()
        {
            batch.Expiration = batch.Expiration.AddDays(7);

            Assert.Equal(BatchState.expiring, batch.State);
        }

        [Fact]
        public void StateAndIsExpired_ShouldConcord()
        {
            batch.Expiration = batch.Expiration.AddDays(7);

            Assert.NotEqual(BatchState.expired, batch.State);
            Assert.False(batch.IsExpired);
        }



    }
}
