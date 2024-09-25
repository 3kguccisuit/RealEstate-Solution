using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.BaseModels
{ 
public abstract class Residential : Estate
{

    public int NumberOfRooms { get; set; }

        [JsonConstructor]
        protected Residential(string id, Address address, LegalForm legalForm, int numberOfRooms) : base(id, address, legalForm)
    {
        this.NumberOfRooms = numberOfRooms;
    }
    // Override ToString() to include residential-specific details
    public override string ToString()
    {
        return $"{base.ToString()}, Number of Rooms: {NumberOfRooms}";
    }
}
}