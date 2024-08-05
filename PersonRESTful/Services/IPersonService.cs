using PersonRESTful.Dto;
using PersonRESTful.Models;

namespace PersonRESTful.Services
{
    public interface IPersonService
    {
        public Task<IEnumerable<Person>> GetAllPersons();
        public Task<Person> GetPersonById(int personId);
        public Task<IEnumerable<Person>> GetPersonsByColor(string color);
        public Task<IEnumerable<Person>> CreatePerson(PersonJSON personJSON);
    }
}
