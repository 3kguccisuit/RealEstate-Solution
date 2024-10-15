using DTO.Models;
using RealEstateDAL.Files;

namespace RealEstateBLL.Service
{
    public class DataService
    {
        //private readonly AppState _appState;
        private readonly FileRepository _fileRepository;
        public DataService()
        {
            _fileRepository = new FileRepository();
        }

        public void SaveDataAsJson(RootObject lists, string filePath)
        {
            Console.WriteLine("Saving data as JSON");
            _fileRepository.SaveDataAsJson(lists, filePath);
            // Save data to the database
        }
    }
}
