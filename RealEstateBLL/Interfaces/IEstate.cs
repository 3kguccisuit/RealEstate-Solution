using RealEstateBLL.Models;

namespace RealEstateBLL.Interfaces
{ 

public interface IEstate
{
    string ID { get; set; }
    Address Address { get; set; }
    string DisplayDetails();
}
}