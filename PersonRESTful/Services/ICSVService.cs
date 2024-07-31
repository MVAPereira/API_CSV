﻿using PersonRESTful.Models;

namespace PersonRESTful.Services
{
    public interface ICSVService
    {
        public List<Person> GetAllPersons();
        public Person GetPersonById(int personId);
        public List<Person> GetPersonsByColor(string color);
    }
}
