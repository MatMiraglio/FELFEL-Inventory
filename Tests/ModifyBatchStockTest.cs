using FELFEL.Domain;
using FELFEL.UseCases;
using FELFEL.UseCases.ModifyBatchStock;
using FELFEL.UseCases.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.test
{
    public class ModifyBatchStockTest : IDisposable
    {
        private ModifyBatchStock modifyStockCommand;
        private Mock<IUnitOfWork> mockUOW;
        private Mock<IBatchRepository> mockRepo;

        public ModifyBatchStockTest()
        {
            mockUOW = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IBatchRepository>();

            Batch batch = new Batch();

            mockUOW.Setup(x => x.Batches.GetAsync(1)).ReturnsAsync(batch);
            mockUOW.Setup(x => x.Batches.GetAsync(2)).ReturnsAsync((Batch)null);

            modifyStockCommand = new ModifyBatchStock(mockUOW.Object);
        }

        public void Dispose()
        {
            modifyStockCommand = null;
        }

        [Theory]
        [InlineData(100, "sss")]
        [InlineData(300, "ssdsdsdfsdf")]
        [InlineData(200, "reason$#$#$#$")]
        [InlineData(int.MaxValue, "hhhhhhhh")]
        [InlineData(343444, "mmmmmmmmmm")]
        public async Task Execute_ShouldModifyBatchStock(int newUnitAmount, string reason)
        {
            //Arrange
            var RequestModel = new ModifyBatchStockRequest(1, newUnitAmount, reason);

            //Act
            var batch = await modifyStockCommand.ExecuteAsync(RequestModel);

            //Assert
            mockUOW.Verify(m => m.CompleteAsync(), Times.Once);

            Assert.Equal(newUnitAmount, batch.RemainingUnits);
            Assert.Equal(1, batch.History.Count);
            Assert.NotNull(batch);
        }


        [Fact]
        public void Execute_ShouldThrowKeyNotFoundExeption()
        {
            //Arrange
            var RequestModel = new ModifyBatchStockRequest(2, 100, "Reason");

            //Act and Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => modifyStockCommand.ExecuteAsync(RequestModel));
            mockUOW.Verify(m => m.CompleteAsync(), Times.Never);
        }


        [Theory]
        [InlineData(-100, "sss")]
        [InlineData(100, "")]
        [InlineData(100, null)]
        [InlineData(-100, "    ")]
        [InlineData(0, null)]
        public void Execute_ShouldThrowArgumentExeption(int newUnitAmount, string reason)
        {
            //Arrange
            var RequestModel = new ModifyBatchStockRequest(1, newUnitAmount, reason);

            //Act and Assert
            Assert.ThrowsAsync<ArgumentException>(() => modifyStockCommand.ExecuteAsync(RequestModel));
            mockUOW.Verify(m => m.CompleteAsync(), Times.Never);
        }
    }
}
