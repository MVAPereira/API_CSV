﻿using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
using PersonRESTful.Dto;
using PersonRESTful.Models;
using System;
using System.ComponentModel;
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

            using (var stream = new FileStream(_csvPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None, 4096, useAsync: true))
            using (var reader = new StreamReader(stream))
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

        private async Task<bool> IsNewLineNeeded()
        {
            using (var stream = new FileStream(_csvPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None, 4096, useAsync: true))
            using (var reader = new StreamReader(stream))
            {
                string fileContent = await reader.ReadToEndAsync();
                if (fileContent.Length > 0 && fileContent[fileContent.Length - 1] != '\n')
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private async Task WritePersonToCSV(PersonCreation personCreation)
        {
            bool isNewLineNeeded = await IsNewLineNeeded();

            using (var stream = new FileStream(_csvPath, FileMode.Append, FileAccess.Write, FileShare.None, 4096, useAsync: true))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            }))
            {
                if (isNewLineNeeded)
                {
                    await writer.WriteLineAsync();
                }
                csv.WriteRecord(personCreation);
                await writer.FlushAsync(); 
            }
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
            var filteteredPersons = persons.Where(p => p.Color == color);
            return filteteredPersons.Any() ? filteteredPersons : null;
        }

        public async Task<IEnumerable<Person>> CreatePerson(PersonJSON personJSON)
        {
            if (!personJSON.IsValidPersonJSON())
            {
                throw new InvalidOperationException();
            }

            PersonCreation personCreation = new PersonCreation()
            {
                LastName = personJSON.LastName,
                Name = personJSON.Name,
                ZipcodeCity = personJSON.Zipcode + " " + personJSON.City,
                Color = personJSON.Color
            };

            await WritePersonToCSV(personCreation);

            var persons = await ReturnsValidPersons();
            return persons;
        }
    }
}
