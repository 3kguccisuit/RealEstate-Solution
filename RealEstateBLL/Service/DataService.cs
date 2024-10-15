using DTO.Enums;
using DTO.Models;
using RealEstateDAL.Files;
using RealEstateDLL.Managers;
using Serilog;
using System.Text.Json;

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

        public void NewFile()
        {
            ClearManagers();
        }

        public Boolean SaveData(string filePath, FileFormats fileFormat)
        {
            switch (fileFormat)
            {
                case FileFormats.Unknown:
                    return false;
                case FileFormats.JSON:
                    SaveDataAsJson(filePath);
                    return true;
                case FileFormats.XML:
                    SaveDataAsXml(filePath);
                    return true;
                default:
                    return false;
            }
        }

        public void LoadDataFromXml(string filePath)
        {
            ClearManagers();
            var lists = _fileRepository.LoadDataFromXml(filePath);
            LoadDataToManagers(lists);
        }

        public void SaveDataAsXml(string filePath)
        {
            var list = GetAllObjects(new RootObject());
            _fileRepository.SaveDataAsXml(list, filePath);
        }

        public void SaveDataAsJson(string filePath)
        {
            var list = GetAllObjects(new RootObject());
            _fileRepository.SaveDataAsJson(list, filePath);
        }

        public Boolean LoadDataFromJson(string filePath)
        {
            ClearManagers();
            var lists = _fileRepository.LoadDataFromJson(filePath);
            return LoadDataToManagers(lists);

        }

        private RootObject GetAllObjects(RootObject rootObject)
        {
            rootObject.EstateList = _estateManager.GetAll();
            rootObject.PersonList = _personManager.GetAll();
            rootObject.PaymentList = _paymentManager.GetAll();
            return rootObject;

        }
        private void ClearManagers()
        {
            _estateManager.Clear();
            _personManager.Clear();
            _paymentManager.Clear();
        }
        private Boolean LoadDataToManagers(RootObject rootObject)
        {
            //if user ex opens a json file when it expects xml file rootObject is null
            if (rootObject == null)
            {
                return false;
            }
            foreach (var estate in rootObject.EstateList) _estateManager.Add(estate.ID, estate);
            foreach (var person in rootObject.PersonList) _personManager.Add(person.ID, person);
            foreach (var payment in rootObject.PaymentList) _paymentManager.Add(payment.ID, payment);
            return true;
        }
    }
}
