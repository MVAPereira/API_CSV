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


    }   

}
