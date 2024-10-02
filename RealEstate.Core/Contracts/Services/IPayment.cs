using RealEstate.Core.Models;

namespace RealEstate.Core.Contracts.Services;

public interface IPayment
{
    string ID { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
}
