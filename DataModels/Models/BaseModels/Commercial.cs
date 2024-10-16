﻿
using System.Text.Json.Serialization;

namespace DTO.Models.BaseModels
{
    public abstract class Commercial : Estate
    {
        public double SquareMeters { get; set; }

        [JsonConstructor]
        protected Commercial(string id, Address address, LegalForm legalForm, double squareMeters)
            : base(id, address, legalForm)
        {
            SquareMeters = squareMeters;
        }
        protected Commercial() : base(Guid.NewGuid().ToString("D"), new Address(), new LegalForm())
        {

        }

        public override string ToString()
        {
            return $"{base.ToString()}, Square Meters: {SquareMeters}";
        }
    }
}
