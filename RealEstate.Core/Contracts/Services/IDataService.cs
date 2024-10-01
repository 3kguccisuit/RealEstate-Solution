namespace RealEstate.Core.Contracts.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task AddAsync(T data);
        Task RemoveAsync(string Id);
        Task UpdateAsync(T updated);

    }
}
