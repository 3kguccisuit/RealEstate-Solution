using DTO.Enums;
using DTO.Models.BaseModels;
using System.Text.Json.Serialization;

namespace DTO.Models.ConcreteModels.Persons
{
    public class Seller : Person
    {
        public decimal AskingPrice { get; set; }
        public override string Type => "Seller";
        [JsonConstructor]
        public Seller(string id, string name, Address address, decimal askingPrice)
            : base(id, name, address)
        {
            AskingPrice = askingPrice;
        }


        // Copy constructor for deep cloning
        public Seller(Seller other) : base(other)
        {
            AskingPrice = other.AskingPrice;
        }

        public Seller() : base(Guid.NewGuid().ToString("D"), "", new Address())
        {

        }

        public override Person AutoFill()
        {
            return new Seller(Guid.NewGuid().ToString("D"), "John Doe", new Address("123 main st", "165523", "Stockholm", Country.Sverige), 0);
        }
        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Asking Price: {AskingPrice}, Address: {Address.Street}, {Address.City}";
        }
    }

}
