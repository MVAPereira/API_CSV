using CsvHelper;
using CsvHelper.Configuration;
using PersonRESTful.Services;

namespace PersonRESTful.Models
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(m => m.Id).Convert(args => GetId(args));

            Map(m => m.Name).Index(0).Validate(args => !string.IsNullOrEmpty(args.Field));
            Map(m => m.LastName).Index(1);
            Map(m => m.Zipcode).Convert(args => ExtractZipCode(args));
            Map(m => m.City).Convert(args => ExtractCity(args));
            Map(m => m.Color).Index(3);
        }

        private int GetId(ConvertFromStringArgs args)
        {
            return Convert.ToInt32(args.Row.Context.Parser.RawRow);
        }

        private string ExtractZipCode(ConvertFromStringArgs args)
        {
            if(!string.IsNullOrEmpty(args.Row.GetField<string>(2)))
            {
                string value = args.Row.GetField<string>(2);
                string valueCut = value.Substring(0, 5);
                return valueCut;
            }
            return string.Empty;     
        }

        private string ExtractCity(ConvertFromStringArgs args)
        {
            if (!string.IsNullOrEmpty(args.Row.GetField<string>(2)))
            {
                string value = args.Row.GetField<string>(2);
                string city = value.Substring(5).Trim();
                return city;
            }
            return string.Empty;
        }
    }
}
