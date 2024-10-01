using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels.Persons
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

        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Asking Price: {AskingPrice}, Address: {Address.Street}, {Address.City}";
        }
    }

}
