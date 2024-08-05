using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonRESTful.Dto;
using PersonRESTful.Models;
using PersonRESTful.Services;
using System;
using System.Drawing;

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
            try
            {
                var persons = await _personService.GetPersonsByColor(color);
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

        [HttpPost("create")]
        public async Task<IActionResult> CreatePerson(PersonJSON personJSON)
        {
            try
            {
                if (personJSON == null)
                {
                    return BadRequest();
                }
                var persons = await _personService.CreatePerson(personJSON);
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
    