
using DTO.Models.BaseModels;
using System.Text.Json.Serialization;

namespace DTO.Models.ConcreteModels.Payments
{
    public class PayPal : Payment
    {
        public string Email { get; set; }
        public override string Type => "PayPal";
        [JsonConstructor]
        public PayPal(string id, string name, decimal amount, string email)
            : base(id, name, amount)
        {
            Email = email;
        }

        public PayPal() : base(Guid.NewGuid().ToString("D"), "", 0)
        {

        }

        public PayPal(PayPal other) : base(other)
        {
            Email = other.Email;
        }

        public override Payment AutoFill()
        {
            return new PayPal(Guid.NewGuid().ToString("D"), "My PayPal Name", 8673, "someone@paypal.com");
        }

        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, Email: {Email}";
        }
    }

}
