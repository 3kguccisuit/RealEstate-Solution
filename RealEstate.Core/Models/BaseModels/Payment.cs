using RealEstate.Core.Contracts.Services;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    public abstract class Payment : IPayment
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public virtual string Type => "Payment";
        public decimal Amount { get; set; }
        [JsonConstructor]
        public Payment(string id, string name, decimal amount)
        {
            ID = id;
            Name = name;
            Amount = amount;
        }

        // Copy constructor for deep cloning
        public Payment(Payment other)
        {
            ID = other.ID;
            Name = other.Name;
            Amount = other.Amount;
        }

        public abstract Payment AutoFill();
    }

}
