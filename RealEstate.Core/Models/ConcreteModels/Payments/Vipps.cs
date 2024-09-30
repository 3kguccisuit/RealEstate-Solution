using RealEstate.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace RealEstate.Core.Models.ConcreteModels.Payments
{
    public class Vipps : Payment
    {
        public string PhoneNumber{ get; set; }
        public bool HasLoanApproval { get; set; }
        public override string Type => "Vipps";
        [JsonConstructor]
        public Vipps(string id, string name, decimal amount, string phoneNumber)
            : base(id, name, amount)
        {
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, IBAN: {PhoneNumber}";
        }
    }

}
