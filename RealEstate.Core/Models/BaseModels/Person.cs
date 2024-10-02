using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    public abstract class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public virtual string Type => "Person";
        public Address Address { get; set; }
        public abstract Person AutoFill();
        [JsonConstructor]
        public Person(string id, string name, Address address)
        {
            ID = id;
            Name = name;
            Address = address;
        }
    }

}
