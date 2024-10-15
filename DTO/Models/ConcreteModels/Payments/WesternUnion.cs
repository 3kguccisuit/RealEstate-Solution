
using DTO.Models.BaseModels;
using System.Text.Json.Serialization;

namespace DTO.Models.ConcreteModels.Payments
{
    public class WesternUnion : Payment
    {
        public string Email { get; set; }

        public override string Type => "WesternUnion";
        [JsonConstructor]
        public WesternUnion(string id, string name, decimal amount, string email)
            : base(id, name, amount)
        {
            Email = email;
        }

        public WesternUnion() : base(Guid.NewGuid().ToString("D"), "", 0)
        {

        }


        public WesternUnion(WesternUnion other) : base(other)
        {
            Email = other.Email;
        }
        public override Payment AutoFill()
        {
            return new WesternUnion(Guid.NewGuid().ToString("D"), "My WesternUnion Name", 2345, "someone@domain.com");
        }


        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, Email: {Email}";
        }
    }

}
