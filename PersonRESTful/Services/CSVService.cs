using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PersonRESTful.Models;
using System;
using System.Formats.Asn1;
using System.Globalization;

namespace PersonRESTful.Services
{
    public class CSVService : ICSVService
    {
        private readonly string _csvPath;
        private readonly ClassMap<Person> _classMap;
        public CSVService(string csvPath, ClassMap<Person> classMap)
        {
            _csvPath = csvPath;
            _classMap = classMap;
        }

        private List<Person> ReturnValidPersons()
        {
            var reader = new StreamReader(_csvPath);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            var csv = new CsvReader(reader, csvConfig);
            csv.Context.RegisterClassMap(_classMap);

            List<Person> persons = new List<Person>();

            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<Person>();

                    if (string.IsNullOrEmpty(record.Name))
                    {
                        throw new ArgumentException("The field is empty!");
                    }

                    if (string.IsNullOrEmpty(record.LastName))
                    {
                        throw new ArgumentException("The field is empty!");
                    }

                    if (string.IsNullOrEmpty(record.Zipcode))
                    {
                        throw new ArgumentException("The field is empty!");
                    }

                    if (string.IsNullOrEmpty(record.City))
                    {
                        throw new ArgumentException("The field is empty!");
                    }

                    if (string.IsNullOrEmpty(record.Color))
                    {
                        throw new ArgumentException("The field is empty!");
                    }

                    persons.Add(record);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return persons;
        }

        public List<Person> GetAllPersons()
        {
            List<Person> persons = ReturnValidPersons();
            return persons;
        }

        public Person GetPersonById(int personId)
        {
            List<Person> persons = ReturnValidPersons();
            var person = persons.FirstOrDefault(p => p.Id == personId);
            return person;
        }

        public List<Person> GetPersonsByColor(string color)
        {
            List<Person> persons = ReturnValidPersons().Where(p => p.Color == color).ToList();
            return persons;

        }
    }
}
