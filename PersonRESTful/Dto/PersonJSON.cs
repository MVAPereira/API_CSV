using PersonRESTful.Models;

namespace PersonRESTful.Dto
{
    public class PersonJSON
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Zipcode{ get; set; }
        public string City { get; set; }
        public string Color { get; set; }

        public bool IsValidPersonJSON()
        {

            if 
            (
                string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Zipcode) ||
                string.IsNullOrWhiteSpace(City)
            )
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(Colors), Convert.ToInt32(Color)))
            {
                return false;
            }

            return true;

        }
    }
}
