using RealEstate.Core.Contracts.Services;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{
    public abstract class Estate : IEstate
    {
        public int ID { get; set; }
        public Address Address { get; set; }

        // Abstract method to be implemented by derived classes
        public virtual string Type => "Estate";
        public abstract string DisplayDetails();


        [JsonConstructor]
        protected Estate(int id, Address address)
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
