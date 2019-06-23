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
        private Mock<IUnitOfWork> mock;
        private Mock<IBatchRepository> mockRepo;
        private Product product;

        public RegisterNewBatchTest()
        {
            mock = new Mock<IUnitOfWork>();
            mockRepo = new Mock<IBatchRepository>();


            product = new Product
            {
                Id = 1,
                Name = "Spaghetti"
            };

            mock.Setup(x => x.Batches).Returns(mockRepo.Object);
            mock.Setup(x => x.Products.GetAsync(2)).ReturnsAsync((Product) null);
            mock.Setup(x => x.Products.GetAsync(1)).ReturnsAsync(product);


            registerCommand = new RegisterNewBatch(mock.Object);
        }

        public void Dispose()
        {
            this.registerCommand = null;
        }

        [Fact]
        public void Execute_ShouldThrowArgumentExeption()
        {
            var RequestModel = new RegisterNewBatchRequest(1, DateTime.Today.AddDays(-1), 100);

            Assert.ThrowsAsync<ArgumentException>(() => registerCommand.ExecuteAsync(RequestModel));
            mock.Verify(m => m.CompleteAsync(), Times.Never);
        }

        [Fact]
        public void Execute_ShouldThrowKeyNotFoundExeption()
        {
            var RequestModel = new RegisterNewBatchRequest(2, DateTime.Today.AddDays(20), 100);

            Assert.ThrowsAsync<KeyNotFoundException>(() => registerCommand.ExecuteAsync(RequestModel));
            mock.Verify(m => m.CompleteAsync(), Times.Never);
        }

        [Fact]
        public void Execute_ShouldRegisterBatch()
        {
            var RequestModel = new RegisterNewBatchRequest(1, DateTime.Today.AddDays(20), 100);
            var batch = registerCommand.ExecuteAsync(RequestModel);

            mock.Verify(m => m.CompleteAsync(), Times.Once);

            Assert.NotNull(batch);
        }

        [Fact]
        public async Task Execute_ShouldRaiseEvent()
        {
            List<Batch> batchesFromEvent = new List<Batch>();

            registerCommand.BatchRegistered += delegate (object sender, BatchEventArgs e)
            {
                batchesFromEvent.Add(e.Batch);
            };

            var RequestModel = new RegisterNewBatchRequest(1, DateTime.Today.AddDays(20), 100);
            var batch = await registerCommand.ExecuteAsync(RequestModel);

            Assert.Single(batchesFromEvent);
            Assert.Contains(batch, batchesFromEvent);
        }
    }
}
