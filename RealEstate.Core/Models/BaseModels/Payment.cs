﻿using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    public class Payment
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
    }

}
