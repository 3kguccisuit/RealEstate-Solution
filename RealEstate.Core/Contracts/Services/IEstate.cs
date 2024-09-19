using RealEstate.Core.Models;

namespace RealEstate.Core.Contracts.Services;

public interface IEstate
{
    int ID { get; set; }
    Address Address { get; set; }
    string DisplayDetails();
}
