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
        private readonly ICSVService _csvService;
        public PersonsController(ICSVService csvService)
        {
            _csvService = csvService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var Persons = _csvService.GetAllPersons();
            return Ok(Persons);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPerson(int personId)
        {
            var Person = _csvService.GetPersonById(personId);
            return Ok(Person);
        }
    }
}
    