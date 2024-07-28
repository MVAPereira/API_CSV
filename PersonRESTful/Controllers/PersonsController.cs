using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonRESTful.Models;
using PersonRESTful.Services;

namespace PersonRESTful.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ICSVService _csvService;
        public PersonsController(ICSVService csvService)
        {
            _csvService = csvService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeCSV()
        {
            var Persons = _csvService.ReadCSV<Person>();

            return Ok(Persons);
        }
    }
}
