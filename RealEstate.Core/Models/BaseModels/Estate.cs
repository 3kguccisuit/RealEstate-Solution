﻿using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.ConcreteModels.Persons;
using System.Reflection;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    public abstract class Estate : IEstate
    {
        public string ID { get; set; }
        public Address Address { get; set; }
        public LegalForm LegalForm { get; set; }
        public Buyer LinkedBuyer { get; set; }
        public Seller LinkedSeller { get; set; }
        public Payment LinkedPayment { get; set; }
        public string ImagePath { get; set; }
        public virtual string Type => "Estate";
        public abstract string DisplayDetails();
        public abstract Estate AutoFill();

        [JsonConstructor]
        protected Estate(string id, Address address, LegalForm legalForm)
        {
            this.ID = id;
            this.Address = address;
            LegalForm = legalForm;
        }

        public Estate Clone()
        {
            var clonedEstate = (Estate)this.MemberwiseClone();

            // Deep clone reference types
            clonedEstate.Address = new Address(this.Address);
            clonedEstate.LegalForm = new LegalForm(this.LegalForm);

            // Deep clone Buyer, Seller, and Payment if they exist
            if (LinkedBuyer != null) clonedEstate.LinkedBuyer = new Buyer(LinkedBuyer);
            if (LinkedSeller != null) clonedEstate.LinkedSeller = new Seller(LinkedSeller);
            if (LinkedPayment != null) clonedEstate.LinkedPayment = (Payment)Activator.CreateInstance(LinkedPayment.GetType(), LinkedPayment);

            return clonedEstate;
        }

        public override string ToString()
        {
            return $"Estate ID: {ID}, Address: {Address}, {LegalForm}";
        }
    }
}
