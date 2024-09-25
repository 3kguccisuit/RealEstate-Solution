using RealEstate.Core.Contracts.Services;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    public abstract class Estate : IEstate
    {
        public string ID { get; set; }
        public Address Address { get; set; }

        public string ImagePath { get; set; }
        public virtual string Type => "Estate";
        public abstract string DisplayDetails();



        [JsonConstructor]
        protected Estate(string id, Address address)
        {
            this.ID = id;
            this.Address = address;
        }
        // Override ToString() to return a summary of the estate
        public override string ToString()
        {
            return $"Estate ID: {ID}, Address: {Address}";
        }
    }
}
