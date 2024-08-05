using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using PersonRESTful.Dto;
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

        private async Task<IEnumerable<Person>> ReturnsValidPersons()
        {
            List<Person> persons = new List<Person>();

            using (var reader = new StreamReader(_csvPath))
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false
                };

                var fileContent = await reader.ReadToEndAsync();

                using (var stringReader = new StringReader(fileContent))
                using (var csv = new CsvReader(stringReader, csvConfig))
                {
                    csv.Context.RegisterClassMap(_classMap);

                    while (csv.Read())
                    {
                        try
                        {
                            var record = csv.GetRecord<Person>();

                            if 
                            (
                                string.IsNullOrEmpty(record.Name) ||
                                string.IsNullOrEmpty(record.LastName) ||
                                string.IsNullOrEmpty(record.Zipcode) ||
                                string.IsNullOrEmpty(record.City) ||
                                string.IsNullOrEmpty(record.Color)
                            )
                            {
                                throw new ArgumentException("A field is empty");
                            }

                            persons.Add(record);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }

            return persons;
        }

        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            var persons = await ReturnsValidPersons();
            return persons;
        }

        public async Task<Person> GetPersonById(int personId)
        {
            var persons = await ReturnsValidPersons();
            var person = persons.FirstOrDefault(p => p.Id == personId);
            return person;
        }

        public async Task<IEnumerable<Person>> GetPersonsByColor(string color)
        {
            var persons = await ReturnsValidPersons();
            return persons.Where(p => p.Color == color).ToList();
        }

        public async Task<IEnumerable<Person>> CreatePerson(PersonJSON personJSON)
        {
            PersonCreation personCreation = new PersonCreation()
            {
                LastName = personJSON.LastName,
                Name = personJSON.Name,
                ZipcodeCity = personJSON.Zipcode + " " + personJSON.City,
                Color = personJSON.Color
            };

            using (var writer = new StreamWriter(_csvPath, append: true))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            }))
            {
                csv.WriteRecord(personCreation);
                csv.NextRecord();
            }

            var persons = await ReturnsValidPersons();
            return persons;
        }
    }
}
