﻿using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels.Payments
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

        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, Email: {Email}";
        }
    }

}
