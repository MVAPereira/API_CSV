using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonRESTful.Models;
using PersonRESTful.Services;

namespace PersonRESTful.Controllers
{
    [Route("persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var Persons = await _personService.GetAllPersons();
            return Ok(Persons);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPerson(int personId)
        {
            var Person = await _personService.GetPersonById(personId);
            return Ok(Person);
        }

        [HttpGet("color/{color}")]
        public async Task<IActionResult> GetPersonsByColor(string color)
        {
            var Persons = await _personService.GetPersonsByColor(color);
            return Ok(Persons);
        }

    }
}
    