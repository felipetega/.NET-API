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

public class CityControllerTests
{
    [Fact]
    public async Task CityController_GetAll_ReturnListCount()
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

        // Verify Property Values Check
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedCities = Assert.IsAssignableFrom<List<CityView>>(okResult.Value);

        Assert.Equal(citiesList.Count, returnedCities.Count);

        // Example: Check the properties of the first returned city
        var firstReturnedCity = returnedCities[0];
        var firstCityDTO = citiesList[0];

        Assert.Equal(firstCityDTO.CityName, firstReturnedCity.CityName);
        Assert.Equal(firstCityDTO.StateName, firstReturnedCity.StateName);
    }

}
