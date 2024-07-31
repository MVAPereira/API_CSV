using PersonRESTful.Models;

namespace PersonRESTful.Services
{
    public interface ICSVService
    {
        public List<Person> GetAllPersons();
    }
}
