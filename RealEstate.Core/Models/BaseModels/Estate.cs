using RealEstate.Core.Contracts.Services;
using RealEstate.Core.Models.ConcreteModels.Persons;
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
        public string ImagePath { get; set; }
        public virtual string Type => "Estate";
        public abstract string DisplayDetails();

        [JsonConstructor]
        protected Estate(string id, Address address, LegalForm legalForm)
        {
            this.ID = id;
            this.Address = address;
            LegalForm = legalForm;
        }

        public override string ToString()
        {
            return $"Estate ID: {ID}, Address: {Address}, {LegalForm}";
        }
    }
}
