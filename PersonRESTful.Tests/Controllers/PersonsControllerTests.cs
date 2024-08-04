using FakeItEasy;
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
        public void PersonsController_GetAllPersons_ReturnsSuccess()
        {
            //Arrange
            var persons = A.CollectionOfFake<Person>(5);

            //Act

            //Assert
        }
    }   

}
