using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PersonRESTful.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace PersonRESTful.Services
{
    public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>()
        {
            var reader = new StreamReader("Data/sample-input.csv");
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            var csv = new CsvReader(reader, csvConfig);
            csv.Context.RegisterClassMap<PersonMap>();

            var records = new List<T>();

            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<T>();

                    Console.WriteLine(record);

                    if(record is Person person)
                    {
                        if (string.IsNullOrEmpty(person.Name))
                        {
                            throw new ArgumentException("The field is empty!");
                        }

                        if (string.IsNullOrEmpty(person.LastName))
                        {
                            throw new ArgumentException("The field is empty!");
                        }

                        if (string.IsNullOrEmpty(person.Zipcode))
                        {
                            throw new ArgumentException("The field is empty!");
                        }

                        if (string.IsNullOrEmpty(person.City))
                        {
                            throw new ArgumentException("The field is empty!");
                        }

                        if (string.IsNullOrEmpty(person.Color))
                        {
                            throw new ArgumentException("The field is empty!");
                        }
                    }

                    records.Add(record);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Validation error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return records;
        }
    }
}
