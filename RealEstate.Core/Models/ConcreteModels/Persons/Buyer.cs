﻿using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels.Persons
{
    public class Buyer : Person
    {
        public decimal Budget { get; set; }
        public bool HasLoanApproval { get; set; }
        public override string Type => "Buyer";
        [JsonConstructor]
        public Buyer(string id, string name, Address address, decimal budget, bool hasLoanApproval)
            : base(id, name, address)
        {
            Budget = budget;
            HasLoanApproval = hasLoanApproval;
        }

        public override Person AutoFill()
        {
            return new Buyer(Guid.NewGuid().ToString("D"), "Alice Johnsson", new Address("321 Main st", "175575", "New York", Country.United_States_of_America), 0, true);
        }

        public override string ToString()
        {
            return $"{Name}, ID: {ID}, Budget: {Budget}, Loan Approval: {HasLoanApproval}, Address: {Address.Street}, {Address.City}";
        }
    }

}
