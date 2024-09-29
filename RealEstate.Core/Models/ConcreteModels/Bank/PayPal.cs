using RealEstate.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace RealEstate.Core.Models.ConcreteModels.Bank
{
    public class PayPal : Payment
    {
        public string Email{ get; set; }
        public bool HasLoanApproval { get; set; }
        public override string Type => "PayPal";
        [JsonConstructor]
        public PayPal(string id, string name, decimal amount, string email)
            : base(id, name, amount)
        {
            Email = email;
        }

        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, Email: {Email}";
        }
    }

}
