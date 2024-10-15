using DTO.Models;

namespace DTO.Interfaces
{ 

public interface IEstate
{
    string ID { get; set; }
    Address Address { get; set; }
    string DisplayDetails();
}
}