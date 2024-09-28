using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    public class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public virtual string Type => "Person";
        public Address Address { get; set; }
        [JsonConstructor]
        public Person(string id, string name, Address address)
        {
            ID = id;
            Name = name;
            Address = address;
        }
    }

}
