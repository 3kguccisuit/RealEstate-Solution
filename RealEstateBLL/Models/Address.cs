using RealEstateBLL.Enums;
using System.Text.Json.Serialization;

namespace RealEstateBLL.Models;

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

    public Address()
    {

    }

    // Copy constructor for deep cloning
    public Address(Address other)
    {
        Street = other.Street;
        City = other.City;
        ZipCode = other.ZipCode;
        Country = other.Country;
    }

    // Override ToString()
    public override string ToString()
    {
        return $"{Street}, {ZipCode} {City}, {Country}";
    }
}
