using CsvHelper;
using CsvHelper.Configuration;
using System.Formats.Asn1;
using System.Globalization;

namespace PersonRESTful.Services
{
    public class CSVService
    {
        public IEnumerable<T> ReadCSV<T>()
        {
            var reader = new StreamReader("Data/Persons.csv");
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            var csv = new CsvReader(reader, csvConfig);
            var records = csv.GetRecords<T>();
            return records;
        }
    }
}
