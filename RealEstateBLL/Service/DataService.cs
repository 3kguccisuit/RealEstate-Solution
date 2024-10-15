using DTO.Models;
using RealEstateDAL.Files;
using RealEstateDLL.Managers;

namespace RealEstateBLL.Service
{
    public class DataService
    {
        //private readonly AppState _appState;
        private readonly FileRepository _fileRepository;
        private readonly EstateManager _estateManager;
        private readonly PersonManager _personManager;
        private readonly PaymentManager _paymentManager;
        public DataService(EstateManager estateManager, PaymentManager paymentManager, PersonManager personManager)
        {
            _fileRepository = new FileRepository();
            _estateManager = estateManager;
            _paymentManager = paymentManager;
            _personManager = personManager;
        }

        public void SaveDataAsJson(string filePath)
        {
            var list = new RootObject();
            list.EstateList = _estateManager.GetAll();
            list.PersonList = _personManager.GetAll();
            list.PaymentList = _paymentManager.GetAll();
            _fileRepository.SaveDataAsJson(list, filePath);
        }

        public void LoadDataFromJson(string filePath)
        {
            ClearManagers();
            var lists = _fileRepository.LoadDataFromJson(filePath);
            LoadDataToManagers(lists);  

        }
        private void ClearManagers()
        {
            _estateManager.Clear();
            _personManager.Clear();
            _paymentManager.Clear();
        }
        private void LoadDataToManagers(RootObject rootObject)
        {
            //if user ex opens a json file when it expects xml file rootObject is null
            if (rootObject == null)
            {
                return;
            }
            foreach (var estate in rootObject.EstateList) _estateManager.Add(estate.ID, estate);
            foreach (var person in rootObject.PersonList) _personManager.Add(person.ID, person);
            foreach (var payment in rootObject.PaymentList) _paymentManager.Add(payment.ID, payment);
        }
    }
}
