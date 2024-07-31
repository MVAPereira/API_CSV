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
        public List<Person> GetAllPersons()
        {
            var reader = new StreamReader("Data/sample-input.csv");
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            var csv = new CsvReader(reader, csvConfig);
            csv.Context.RegisterClassMap<PersonMap>();

            var records = new List<Person>();

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

        public Person GetPersonById(int personId)
        {
            var reader = new StreamReader("Data/sample-input.csv");
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            var csv = new CsvReader(reader, csvConfig);
            csv.Context.RegisterClassMap<PersonMap>();

            var person = new Person();
            while (csv.Read())
            {
                try
                {
                    var record = csv.GetRecord<Person>();
                    if (record.Id == personId)
                    { 
                        person = record;
                        break;
                    }
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

            return person;
        }
    }
}
