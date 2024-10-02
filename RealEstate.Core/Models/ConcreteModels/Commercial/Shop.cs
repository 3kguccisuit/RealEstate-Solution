﻿using RealEstate.Core.Enums;
using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Shop : Commercial
    {
        public bool HasOnlineStore { get; set; }
        public override string Type => "Shop";
        [JsonConstructor]
        public Shop(string id, Address address, LegalForm legalForm, double squareMeters, bool hasOnlineStore)
            : base(id, address, legalForm, squareMeters)
        {
            HasOnlineStore = hasOnlineStore;
        }

        public override Estate AutoFill()
        {
            return new Shop(Guid.NewGuid().ToString("D"), new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0, // SquareMeters
                    false // HasOnlineStore
                );
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Has Online Store: {HasOnlineStore}";
        }
    }
}
