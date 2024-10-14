
using System.Text.Json.Serialization;

namespace RealEstateBLL.Models.BaseModels
{
    public abstract class Institutional : Estate
    {
        public int ParkingSpaces { get; set; }

        [JsonConstructor]
        protected Institutional(string id, Address address, LegalForm legalForm, int parkingSpaces)
            : base(id, address, legalForm)
        {
            ParkingSpaces = parkingSpaces;
        }
        protected Institutional() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm())
        {
            
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Capacity: {ParkingSpaces}";
        }
    }
}
