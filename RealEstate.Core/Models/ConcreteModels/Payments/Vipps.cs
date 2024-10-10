using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels.Payments
{
    public class Vipps : Payment
    {
        public string PhoneNumber { get; set; }
        public override string Type => "Vipps";
        [JsonConstructor]
        public Vipps(string id, string name, decimal amount, string phoneNumber)
            : base(id, name, amount)
        {
            PhoneNumber = phoneNumber;
        }

        public Vipps() : base(Guid.NewGuid().ToString("D"), "", 0)
        {

        }


        public Vipps(Vipps other) : base(other)
        {
            PhoneNumber = other.PhoneNumber;
        }
        public override Payment AutoFill()
        {
            return new Vipps(Guid.NewGuid().ToString("D"), "My Vipps Name", 3459, "+4434689834");
        }


        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, IBAN: {PhoneNumber}";
        }
    }

}
