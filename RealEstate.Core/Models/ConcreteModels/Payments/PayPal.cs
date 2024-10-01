﻿using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels.Payments
{
    public class PayPal : Payment
    {
        public string Email { get; set; }
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
