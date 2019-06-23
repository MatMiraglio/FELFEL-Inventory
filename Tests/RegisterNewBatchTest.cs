using FELFEL.Domain;
using FELFEL.UseCases;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Inventory.test
{
    public class RegisterNewBatchTest : IDisposable
    {
        private RegisterNewBatch registerCommand;
        private Mock<IUnitOfWork> mockUOW;
        private Mock<IBatchRepository> mockRepo;
        private Product product;

        public RegisterNewBatchTest()
        {
            mockUOW = new Mock<IUnitOfWork>();


            mockRepo = new Mock<IBatchRepository>();


            product = new Product
            {
                Id = 1,
                Name = "Spaghetti"
            };

            mockUOW.Setup(x => x.Batches).Returns(mockRepo.Object);
            mockUOW.Setup(x => x.Batches.Add(It.IsAny<Batch>()));

            mockUOW.Setup(x => x.Products.GetAsync(2)).ReturnsAsync((Product) null);
            mockUOW.Setup(x => x.Products.GetAsync(1)).ReturnsAsync(product);


            registerCommand = new RegisterNewBatch(mockUOW.Object);
        }

        public void Dispose()
        {
            registerCommand = null;
        }

        [Fact]
        public async Task Execute_ShouldRegisterBatch()
        {
            //Arrange
            var RequestModel = new RegisterNewBatchRequest(1, DateTime.Today.AddDays(20), 100);
            
            //Act
            var batch = await registerCommand.ExecuteAsync(RequestModel);

            //Assert
            mockUOW.Verify(m => m.CompleteAsync(), Times.Once);
            mockRepo.Verify(m => m.Add(batch), Times.Once);
            Assert.NotNull(batch);
        }

        [Fact]
        public async Task Execute_ShouldRaiseEvent()
        {
            //Arrange
            List<Batch> batchesFromEvent = new List<Batch>();

            registerCommand.BatchRegistered += delegate (object sender, BatchEventArgs e)
            {
                batchesFromEvent.Add(e.Batch);
            };
            var RequestModel = new RegisterNewBatchRequest(1, DateTime.Today.AddDays(20), 100);

            //Act
            var batch = await registerCommand.ExecuteAsync(RequestModel);

            //Assert
            Assert.Single(batchesFromEvent);
            Assert.Contains(batch, batchesFromEvent);
        }

        [Fact]
        public void Execute_ShouldThrowArgumentExeption()
        {
            //Arrange
            var RequestModel = new RegisterNewBatchRequest(1, DateTime.Today.AddDays(-1), 100);

            //Act and Assert
            Assert.ThrowsAsync<ArgumentException>(() => registerCommand.ExecuteAsync(RequestModel));
            mockUOW.Verify(m => m.CompleteAsync(), Times.Never);
        }

        [Fact]
        public void Execute_ShouldThrowKeyNotFoundExeption()
        {
            //Arrange
            var RequestModel = new RegisterNewBatchRequest(2, DateTime.Today.AddDays(20), 100);

            //Act and Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => registerCommand.ExecuteAsync(RequestModel));
            mockUOW.Verify(m => m.CompleteAsync(), Times.Never);
        }

    }
}
