using PersonRESTful.Models;

namespace PersonRESTful.Services
{
    public interface IPersonService
    {
        public Task<List<Person>> GetAllPersons();
        public Task<Person> GetPersonById(int personId);
        public Task<List<Person>> GetPersonsByColor(string color);
    }
}
