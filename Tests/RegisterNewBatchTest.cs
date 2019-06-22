using FELFEL.Domain;
using FELFEL.UseCases;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Inventory.test
{
    public class RegisterNewBatchTest : IDisposable
    {
        private RegisterNewBatch registerCommand;
        private Mock<IUnitOfWork> mock;
        private Mock<IBatchRepository> repoMock;
        private Product product;

        public RegisterNewBatchTest()
        {
            mock = new Mock<IUnitOfWork>();
            repoMock = new Mock<IBatchRepository>();


            product = new Product
            {
                Id = 1,
                Name = "Spaghetti"
            };

            mock.Setup(x => x.Batches).Returns(repoMock.Object);
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
    }
}
