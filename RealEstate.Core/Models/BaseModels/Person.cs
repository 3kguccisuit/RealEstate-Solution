using RealEstate.Core.Models.ConcreteModels;
using RealEstate.Core.Models.ConcreteModels.Persons;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    [XmlInclude(typeof(Buyer))]
    [XmlInclude(typeof(Seller))]
    public abstract class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }

        public virtual string Type => "Person";
        public abstract Person AutoFill();

        [JsonConstructor]
        public Person(string id, string name, Address address)
        {
            ID = id;
            Name = name;
            Address = address;
        }

        // Copy constructor for deep cloning
        public Person(Person other)
        {
            ID = other.ID;
            Name = other.Name;
            Address = new Address(other.Address); // Deep copy of Address
        }
    }


}
