using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{ 
public abstract class Residential : Estate
{
    public LegalForm LegalForm { get; set; }
    public int NumberOfRooms { get; set; }

        [JsonConstructor]
        protected Residential(string id, Address address, LegalForm legalForm, int numberOfRooms) : base(id, address)
    {
        this.LegalForm = legalForm;
        this.NumberOfRooms = numberOfRooms;
    }
    // Override ToString() to include residential-specific details
    public override string ToString()
    {
        return $"{base.ToString()}, Legal Form: {LegalForm}, Number of Rooms: {NumberOfRooms}";
    }
}
}