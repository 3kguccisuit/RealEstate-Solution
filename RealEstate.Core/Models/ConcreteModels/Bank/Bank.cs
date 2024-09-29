using RealEstate.Core.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace RealEstate.Core.Models.ConcreteModels.Bank
{
    public class Bank : Payment
    {
        public string IbanNumber{ get; set; }
        public bool HasLoanApproval { get; set; }
        public override string Type => "Bank";
        [JsonConstructor]
        public Bank(string id, string name, decimal amount, string ibanNumber)
            : base(id, name, amount)
        {
            IbanNumber = ibanNumber;
        }

        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Amount: {Amount}, IBAN: {IbanNumber}";
        }
    }

}
