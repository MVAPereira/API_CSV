using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using PersonRESTful.Controllers;
using PersonRESTful.Models;
using PersonRESTful.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRESTful.Tests.Controllers
{
    public class PersonsControllerTests
    {
        private IPersonService _personService;
        private PersonsController _personsController;

        public PersonsControllerTests()
        {
            _personService = A.Fake<IPersonService>();
            _personsController = new PersonsController(_personService);
        }

        [Fact]
        public async Task PersonsController_GetAllPersons_ReturnsTaskIActionResult()
        {
            //Arrange
            var persons = A.Fake<IEnumerable<Person>>();
            A.CallTo(() => _personService.GetAllPersons()).Returns(persons);

            //Act
            var result = await _personsController.GetAllPersons();

            //Assert
            result.Should().BeAssignableTo<IActionResult>();
        }

        [Fact]
        public async Task PersonsController_GetAllPersons_ReturnsNotFoundResult_WhenPersonsAreNull()
        {
            // Arrange
            A.CallTo(() => _personService.GetAllPersons()).Returns(Task.FromResult<IEnumerable<Person>>(null));

            // Act
            var result = await _personsController.GetAllPersons();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PersonsController_GetAllPersons_ReturnsInternalServerError_WhenOnExpection()
        {
            // Arrange
            A.CallTo(() => _personService.GetAllPersons()).Throws<Exception>();

            // Act
            var result = await _personsController.GetAllPersons();

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult)result;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task PersonsController_GetPerson_ReturnsTaskIActionResult()
        {
            //Arrange
            var person = A.Fake<Person>();
            var personId = 1;
            A.CallTo(() => _personService.GetPersonById(personId)).Returns(person);

            //Act
            var result = await _personsController.GetPersonById(personId);

            //Assert
            result.Should().BeAssignableTo<IActionResult>();
        }

        [Fact]
        public async Task PersonController_GetPersonById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            //Arrange
            var person = A.Fake<Person>();
            var personId = 1;
            A.CallTo(() => _personService.GetPersonById(personId)).Returns(Task.FromResult<Person>(null));

            //Act
            var result = await _personsController.GetPersonById(personId);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PersonsController_GetPersonById_ReturnsInternalServerError_WhenOnExpection()
        {
            // Arrange
            var personId = 1;
            A.CallTo(() => _personService.GetPersonById(personId)).Throws<Exception>();

            // Act
            var result = await _personsController.GetPersonById(personId);

            // Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult)result;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task PersonsController_GetPersonsByColor_ReturnsTaskIActionResult()
        {
            //Arrange
            var persons = A.Fake<IEnumerable<Person>>();
            var color = "color";

            A.CallTo(() => _personService.GetPersonsByColor(color)).Returns(persons);

            //Act
            var result = await _personsController.GetPersonsByColor(color);

            //Assert
            result.Should().BeAssignableTo<IActionResult>();
        }





    }

}
