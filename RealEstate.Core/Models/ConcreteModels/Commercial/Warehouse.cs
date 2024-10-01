using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
{
    public class Warehouse : Commercial
    {
        public int LoadingDocks { get; set; }
        public override string Type => "Warehouse";
        [JsonConstructor]
        public Warehouse(string id, Address address, LegalForm legalForm, double squareMeters, int loadingDocks)
            : base(id, address, legalForm, squareMeters)
        {
            LoadingDocks = loadingDocks;
        }

        public override string DisplayDetails()
        {
            return $"{base.ToString()}, Loading Docks: {LoadingDocks}";
        }
    }
}
