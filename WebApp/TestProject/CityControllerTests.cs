using Api.Application.Controllers;
using Api.Application.ViewModel;
using Api.Services.DTOs;
using Api.Services.Repositories.Interfaces;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class CityControllerTests
    {
        [Fact]
        public async Task GetAll_Returns200_WhenThereIsData()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var citiesList = fixture.CreateMany<CityDTO>(10).ToList();

            mockCityRepository.Setup(repo => repo.GetAll()).ReturnsAsync(citiesList);

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.GetAll();

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<List<CityView>>>(result);

            // Verify HTTP Status Code 200 (OK)
            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, (actionResult.Result as OkObjectResult).StatusCode);
        }

        [Fact]
        public async Task GetAll_Returns204_WhenNoData()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var emptyCitiesList = new List<CityDTO>(); // Empty list

            mockCityRepository.Setup(repo => repo.GetAll()).ReturnsAsync(emptyCitiesList);

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.GetAll();

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<List<CityView>>>(result);

            // Verify HTTP Status Code 204 (NoContent)
            Assert.IsType<NoContentResult>(actionResult.Result);
            Assert.Equal(204, (actionResult.Result as NoContentResult).StatusCode);
        }

        [Fact]
        public async Task GetById_Returns200_WhenThereIsData()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var cityId = 1;
            var cityDto = fixture.Create<CityDTO>();

            mockCityRepository.Setup(repo => repo.GetById(cityId)).ReturnsAsync(cityDto);

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.GetById(cityId);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);

            // Verify HTTP Status Code 200 (OK)
            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, (actionResult.Result as OkObjectResult).StatusCode);

            // Verify CityView returned the same as CityDTO
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedCity = Assert.IsAssignableFrom<CityView>(okResult.Value);

            Assert.Equal(cityDto.CityName, returnedCity.CityName);
            Assert.Equal(cityDto.StateName, returnedCity.StateName);
        }

        [Fact]
        public async Task GetById_Returns404_WhenNoData()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var cityId = 1;

            // No data
            mockCityRepository.Setup(repo => repo.GetById(cityId)).ReturnsAsync((CityDTO)null);

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.GetById(cityId);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);

            // Verify HTTP Status Code 404 (NotFound)
            Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, (actionResult.Result as NotFoundResult).StatusCode);
        }

        [Fact]
        public async Task Create_Returns201_WhenCreated()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var newCityView = fixture.Create<CityView>();

            mockCityRepository.Setup(repo => repo.Create(It.IsAny<CityDTO>())).ReturnsAsync(fixture.Create<CityDTO>());

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.Create(newCityView);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);

            // Verify HTTP Status Code 201 (Created)
            Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(201, (actionResult.Result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Create_Returns400_WhenCityViewIsNull()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.Create(null);

            // ASSERT
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);

            // Verify HTTP Status Code 400 (BadRequest)
            Assert.Equal(400, badRequestResult.StatusCode);
        }


        [Fact]
        public async Task Update_Returns200_WhenUpdated()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var existingCityId = 1;
            var updatedCityView = fixture.Create<CityView>();

            mockCityRepository.Setup(repo => repo.GetById(existingCityId)).ReturnsAsync(fixture.Create<CityDTO>());
            mockCityRepository.Setup(repo => repo.Update(It.IsAny<CityDTO>(), existingCityId)).Returns(Task.FromResult(fixture.Create<CityDTO>()));



            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.Update(updatedCityView, existingCityId);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);

            // Verify HTTP Status Code 200 (OK)
            Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equal(200, (actionResult.Result as OkObjectResult).StatusCode);
        }

        [Fact]
        public async Task Update_Returns404_WhenUpdatedCityIsNull()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var existingCityId = 1;
            var updatedCityView = fixture.Create<CityView>();

            mockCityRepository.Setup(repo => repo.Update(It.IsAny<CityDTO>(), existingCityId)).ReturnsAsync((CityDTO)null);

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.Update(updatedCityView, existingCityId);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);

            // Verify HTTP Status Code 404 (NotFound)
            Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, (actionResult.Result as NotFoundResult).StatusCode);
        }


        [Fact]
        public async Task Update_Returns400_WhenCityViewIsNull()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var existingCityId = 1;
            var invalidCityView = new CityView();

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.Update(invalidCityView, existingCityId);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);

            // Verify HTTP Status Code 400 (BadRequest)
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(400, (actionResult.Result as BadRequestObjectResult).StatusCode);
            Assert.Equal("Invalid input. CityName and StateName are required.", (actionResult.Result as BadRequestObjectResult).Value); // Verifique a mensagem de erro específica
        }

        [Fact]
        public async Task Delete_Returns200_WhenDeleted()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var existingCityId = 1;

            mockCityRepository.Setup(repo => repo.Delete(existingCityId)).ReturnsAsync(true);

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.Delete(existingCityId);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);

            // Verify HTTP Status Code 200 (OK)
            Assert.Equal(200, objectResult.StatusCode);
        }

        [Fact]
        public async Task Delete_Returns404_NotDeleted()
        {
            // ARRANGE
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());

            var mockCityRepository = fixture.Freeze<Mock<ICityRepository>>();
            var nonExistingCityId = 1;

            mockCityRepository.Setup(repo => repo.Delete(nonExistingCityId)).ReturnsAsync(false);

            var controller = new CityController(mockCityRepository.Object);

            // ACT
            var result = await controller.Delete(nonExistingCityId);

            // ASSERT
            var actionResult = Assert.IsType<ActionResult<CityView>>(result);

            // Verify HTTP Status Code 404 (NotFound)
            Assert.IsType<NotFoundResult>(actionResult.Result);
            Assert.Equal(404, (actionResult.Result as NotFoundResult).StatusCode);
        }



    }

}


