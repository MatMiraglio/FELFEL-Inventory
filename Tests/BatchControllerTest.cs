using FELFEL.UseCases.ModifyBatchStock;
using FELFEL.UseCases.RegisterNewBatch;
using FELFEL.UseCases.Repositories;
using FELFEL.WebApi.Controllers;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FELFEL.Domain;
using FELFEL.WebApi.InputModels;

namespace Inventory.test
{
    public class BatchControllerTest : IDisposable
    {
        BatchController controller;
        private Mock<IBatchRepository> mockRepo;
        private Mock<IRegisterNewBatch> mockRegister;
        private RegisterNewBatchRequest registerRequest;
        private Mock<IModifyBatchStock> mockModify;
        private int _404 = 404;

        public BatchControllerTest()
        {
            mockRepo = new Mock<IBatchRepository>();
            var batch = new Batch
            {
                Id = 200
            };
            mockRepo.Setup( x => x.GetBatchAsync(200)).ReturnsAsync(batch);
            mockRepo.Setup(x => x.GetBatchAsync(404)).ReturnsAsync((Batch) null);
            mockRepo.Setup(x => x.GetStock(404)).Throws(new InvalidOperationException());
            mockRepo.Setup(x => x.GetStock(200)).ReturnsAsync(100);
            mockRepo.Setup(x => x.GetBatchWithHistoryAsync(200)).ReturnsAsync(batch);
            mockRepo.Setup(x => x.GetBatchWithHistoryAsync(404)).ReturnsAsync((Batch) null);
            var lazyRepo = new Lazy<IBatchRepository>( () => mockRepo.Object );


            mockRegister = new Mock<IRegisterNewBatch>();

            registerRequest = new RegisterNewBatchRequest(200,DateTime.Today.AddDays(21), 500);
            mockRegister.Setup(x => x.ExecuteAsync(It.IsAny<RegisterNewBatchRequest>())).ReturnsAsync(batch);
            var lazyRegister = new Lazy<IRegisterNewBatch>(() => mockRegister.Object);

            mockModify = new Mock<IModifyBatchStock>();
            var lazyModify = new Lazy<IModifyBatchStock>(() => mockModify.Object);


            controller = new BatchController(lazyRepo, lazyRegister, lazyModify);
        }

        public void Dispose()
        {
            controller = null;
        }

        [Fact]
        public async Task GetAllBatches_ShouldBeOk()
        {
            //Arrange

            //Act
            var result = await controller.GetAllBatches();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
            mockRepo.Verify(_ => _.GetBatchesDeatiledAsync(), Times.Once);
        }

        [Fact]
        public async Task GetBatch_ShouldReturnNotFound()
        {
            //Arrange

            //Act
            var result = await controller.GetBatch(5);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            mockRepo.Verify(_ => _.GetBatchAsync(5), Times.Once);
        }

        [Fact]
        public async Task GetBatch_ShouldReturnOk()
        {
            //Arrange

            //Act
            var result = await controller.GetBatch(200) as OkObjectResult;
            var content = result.Value as Batch;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Batch>(result.Value);
            Assert.Equal(200, content.Id);
            mockRepo.Verify(_ => _.GetBatchAsync(200), Times.Once);
        }

        [Fact]
        public async Task GetStock_ShouldReturnNotFound()
        {
            //Arrange

            //Act
            var result = await controller.GetStock(404) as NotFoundObjectResult;
            var content = result.Value as string;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<string>(result.Value);
            Assert.Contains("404", content);
            mockRepo.Verify(_ => _.GetStock(404), Times.Once);
        }

        [Fact]
        public async Task GetStock_ShouldReturnOk()
        {
            //Arrange

            //Act
            var result = await controller.GetStock(200) as OkObjectResult;
            var content = result.Value;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<int>(result.Value);
            Assert.Equal(100, content);
            mockRepo.Verify(_ => _.GetStock(200), Times.Once);
        }

        [Fact]
        public async Task GetBatchHistory_ShouldReturnOk()
        {
            //Arrange

            //Act
            var result = await controller.GetBatchHistory(200) as OkObjectResult;
            var content = result.Value as Batch;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Batch>(content);
            mockRepo.Verify(_ => _.GetBatchWithHistoryAsync(200), Times.Once);
        }

        [Fact]
        public async Task GetBatchHistory_ShouldReturnNotFound()
        {
            //Arrange

            //Act
            var result = await controller.GetBatchHistory(404) as NotFoundObjectResult;
            var content = result.Value as string;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<string>(content);
            Assert.Contains("404", content);
            mockRepo.Verify(_ => _.GetBatchWithHistoryAsync(404), Times.Once);
        }

        [Fact]
        public async Task RegisterNewBatch_ShouldReturnOk()
        {
            //Arrange
            var exp = DateTime.Today.AddDays(21);

            var request = new NewBatch
            {
                ProductId = 200,
                Expiration = exp,
                UnitAmount = 500
            };

            //Act
            var result = await controller.RegisterNewBatch(request) as CreatedAtActionResult;

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }


    }
}
