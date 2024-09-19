using RealEstate.Core.Enums;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models;

public class Address
{
    public string Street { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public Country Country { get; set; }


    [JsonConstructor]
    public Address(string street, string zipCode, string city, Country country)
    {
        Street = street;
        ZipCode = zipCode;
        City = city;
        Country = country;
    }

    // Override ToString()
    public override string ToString()
    {
        return $"{Street}, {ZipCode} {City}, {Country}";
    }
}
