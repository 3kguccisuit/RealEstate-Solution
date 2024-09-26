﻿using RealEstate.Core.Models.BaseModels;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models.ConcreteModels
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

        public override string ToString()
        {
            return $"{base.ToString()}, Capacity: {ParkingSpaces}";
        }
    }
}