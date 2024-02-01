using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class City
    {
        // Your City class properties and constructors go here
        // For example:
        public int Id { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }

        public City(int id, string name, string stateName)
        {
            Id = id;
            CityName = name;
            StateName = stateName;
        }

        public City(string name, string stateName)
        {
            // Set default Id or generate a new Id as needed
            Id = 0;
            CityName = name;
            StateName = stateName;
        }
    }
}
