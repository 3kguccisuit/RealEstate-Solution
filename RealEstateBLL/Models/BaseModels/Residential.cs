using System.Text.Json.Serialization;

namespace RealEstateBLL.Models.BaseModels
{
    public abstract class Residential : Estate
    {

        public int NumberOfRooms { get; set; }

        [JsonConstructor]
        protected Residential(string id, Address address, LegalForm legalForm, int numberOfRooms) : base(id, address, legalForm)
        {
            this.NumberOfRooms = numberOfRooms;
        }

        protected Residential() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm())
        {
            
        }
        public override string ToString()
        {
            return $"{base.ToString()}, Number of Rooms: {NumberOfRooms}";
        }
    }
}