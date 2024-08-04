using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PersonRESTful.Models;
using System;
using System.Formats.Asn1;
using System.Globalization;

namespace PersonRESTful.Services
{
    public class PersonCSVService : IPersonService
    {
        private readonly string _csvPath;
        private readonly ClassMap<Person> _classMap;
        public PersonCSVService(string csvPath, ClassMap<Person> classMap)
        {
            _csvPath = csvPath;
            _classMap = classMap;
        }

        private async Task<List<Person>> ReturnValidPersons()
        {
            var reader = new StreamReader(_csvPath);
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            var fileContent = await reader.ReadToEndAsync();
            var stringReader = new StringReader(fileContent);

            var csv = new CsvReader(stringReader, csvConfig);
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

        public async Task<List<Person>> GetAllPersons()
        {
            return await Task.Run(() => ReturnValidPersons());
        }

        public async Task<Person> GetPersonById(int personId)
        {
            List<Person> persons = await ReturnValidPersons();
            var person = persons.FirstOrDefault(p => p.Id == personId);
            return person;
        }

        public async Task<List<Person>> GetPersonsByColor(string color)
        {
            List<Person> persons = await ReturnValidPersons();
            return persons.Where(p => p.Color == color).ToList();
        }

    }
}
