using GeographyService.Controllers;
using GeographyService.Models;
using GeographyService.Models.Entities;
using GeographyService.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestGeographyService
{
    public class CountryControllerTest
    {
        private readonly Mock<ICountryRepository> mockRepos;
        private readonly Mock<ILogger> mockLogger;
        private readonly CountryController countryController;

        public CountryControllerTest()
        {
            mockRepos = new Mock<ICountryRepository>();
            ILogger logger = Mock.Of<ILogger<CountryController>>();
            countryController = new CountryController(mockRepos.Object, (ILogger<CountryController>)logger);
        }
        [Fact]
        public void GET_UnknownID_ReturnsNotFound()
        {
            mockRepos.Setup(repos => repos.GetCountry(1, 200))
                .Throws(new GeoException("Country does not exist in db."));
            var result = countryController.GetCountry(1, 200);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
        [Fact]
        public void GET_CorrectID_ReturnOkResult()
        {
            mockRepos.Setup(repos => repos.GetCountry(7, 13))
                .Returns(new Country { CountryId = 7, Name = "België", Surface = 30689 });
            var result = countryController.GetCountry(7, 13);
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public void GET_CorrectID_ReturnsCountry()
        {
            Country c = new Country { CountryId = 7, Name = "België", Surface = 30689 };
            mockRepos.Setup(repos => repos.GetCountry(7, 13))
                .Returns(c);
            var result = countryController.GetCountry(7, 13).Result as OkObjectResult;

            Assert.IsType<Country>(result.Value);
            Assert.Equal(13, (result.Value as Country).CountryId);
            Assert.Equal(c.CountryId, (result.Value as Country).CountryId);
            Assert.Equal(c.Name, (result.Value as Country).Name);
            Assert.Equal(c.Surface, (result.Value as Country).Surface);
        }
        [Fact]
        public void POST_ValidObject_ReturnsCreatedAtAction()
        {
            Country c = new Country("Zweden", 450295);
            var response = countryController.Post(7, c);
            Assert.IsType<CreatedAtActionResult>(response.Result);
        }
        [Fact]
        public void POST_ValidObject_ReturnsCorrectItem()
        {
            Country c = new Country("Zweden", 450295);
            var response = countryController.Post(7, c).Result as CreatedAtActionResult;
            var item = response.Value as Country;

            Assert.IsType<Country>(item);
            Assert.Equal(c.Name, item.Name);
            Assert.Equal(c.Surface, item.Surface);
        }
        [Fact]
        public void POST_InvalidObject_ReturnsBadRequest()
        {
            Country c = new Country("Zweden", 450295);
            countryController.ModelState.AddModelError("format error", "int expected");
            var response = countryController.Post(7, c).Result;

            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
