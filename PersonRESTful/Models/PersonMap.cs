using CsvHelper.Configuration;

namespace PersonRESTful.Models
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Id).Convert(args => args.Row.Context.Parser.Row); ;
            Map(m => m.Name).Index(0);
            Map(m => m.LastName).Index(1);
            Map(m => m.Zipcode).Index(2);
            Map(m => m.City).Index(3);
            Map(m => m.Color).Index(4);
        }

    }
}
