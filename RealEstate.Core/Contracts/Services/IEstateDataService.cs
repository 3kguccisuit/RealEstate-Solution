using RealEstate.Core.Models.BaseModels;
using System.Threading.Tasks;

namespace RealEstate.Core.Contracts.Services
{
    public interface IEstateDataService
    {
        Task<IEnumerable<Estate>> GetEstatesAsync();
        Task AddEstateAsync(Estate estate);
        Task RemoveEstateAsync(string estateId);
        Task UpdateEstateAsync(Estate updatedEstate);
    }
}
