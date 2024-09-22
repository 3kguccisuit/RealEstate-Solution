using RealEstate.Core.Models;

namespace RealEstate.Core.Contracts.Services;

public interface IEstate
{
    string ID { get; set; }
    Address Address { get; set; }
    string DisplayDetails();
}
