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
            try
            {
                var persons = await _personService.GetAllPersons();
                if (persons == null)
                {
                    return NotFound();
                }
                return Ok(persons);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPersonById(int personId)
        {
            try
            {
                var person = await _personService.GetPersonById(personId);
                if (person == null)
                {
                    return NotFound();
                }
                return Ok(person);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("color/{color}")]
        public async Task<IActionResult> GetPersonsByColor(string color)
        {
            var Persons = await _personService.GetPersonsByColor(color);
            return Ok(Persons);
        }

    }
}
    