using CsvHelper;
using CsvHelper.Configuration;
using PersonRESTful.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PersonRESTful.Services
{
    public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>()
        {
            var reader = new StreamReader("Data/Persons.csv");
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            var csv = new CsvReader(reader, csvConfig);
            csv.Context.RegisterClassMap<PersonMap>();

            var records = csv.GetRecords<T>();
            return records;
        }
    }
}
