using RealEstate.Core.Enums;
using RealEstate.Core.Libs;
using RealEstate.Core.Models;
using RealEstate.Core.Services;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using RealEstate.Contracts.Services;
using RealEstate.ViewModels;

namespace RealEstate.Helpers
{
    public class FileDataHandler
    {
        private readonly EstateManager _estateManager;
        private readonly PersonManager _personManager;
        private readonly PaymentManager _paymentManager;
        private readonly AppState _appState;
        private readonly INavigationService _navigationService;

        public FileDataHandler(AppState appState, EstateManager estateManager, PersonManager personManager, PaymentManager paymentManager, INavigationService navigationService)
        {
            _appState = appState;
            _estateManager = estateManager;
            _personManager = personManager;
            _paymentManager = paymentManager;
            _navigationService = navigationService;
        }

        public void OpenJsonFile()
        {
            OpenFile("json", "*.json", FileFormats.JSON, (content) =>
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new EstateJsonConverter(), new PersonJsonConverter(), new PaymentJsonConverter(), new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<RootObject>(content, options);
            });
        }

        public void SaveAsJsonFile()
        {
            SaveFile("json", "*.json", () =>
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new EstateJsonConverter(), new PersonJsonConverter(), new PaymentJsonConverter() }
                };
                return JsonSerializer.Serialize(new RootObject
                {
                    EstateList = _estateManager.GetAll(),
                    PersonList = _personManager.GetAll(),
                    PaymentList = _paymentManager.GetAll()
                }, options);
            });
        }

        public void OpenXmlFile()
        {
            OpenFile("xml", "*.xml", FileFormats.XML, (content) => XmlHelper.DeserializeFromXml<RootObject>(content));
        }

        public void SaveAsXmlFile()
        {
            SaveFile("xml", "*.xml", () =>
            {
                return XmlHelper.SerializeToXml(new RootObject
                {
                    EstateList = _estateManager.GetAll(),
                    PersonList = _personManager.GetAll(),
                    PaymentList = _paymentManager.GetAll()
                });
            });
        }

        private void OpenFile(string defaultExt, string filter, FileFormats format, Func<string, RootObject> deserialize)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog { FileName = "Document", DefaultExt = $".{defaultExt}", Filter = $"{defaultExt.ToUpper()}|{filter}|All Files|*.*" };
            if (dialog.ShowDialog() == true)
            {
                _appState.FileName = dialog.FileName;
                var content = File.ReadAllText(_appState.FileName);
                var rootObject = deserialize(content);

                ClearManagers();
                LoadDataToManagers(rootObject);

                _appState.IsDirty = false;
                _appState.Format = format;
                _navigationService.NavigateTo(typeof(MainViewModel).FullName);
            }
        }

        private void SaveFile(string defaultExt, string filter, Func<string> serialize)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog { FileName = "Document", DefaultExt = $".{defaultExt}", Filter = $"{defaultExt.ToUpper()}|{filter}|All Files|*.*" };
            if (dialog.ShowDialog() == true)
            {
                _appState.FileName = dialog.FileName;
                var content = serialize();
                File.WriteAllText(_appState.FileName, content);
                _appState.IsDirty = false;
            }
        }
        public void Save()
        {
            switch (_appState.Format)
            {
                case FileFormats.Unknown:
                    SaveAsJsonFile();
                    break;
                case FileFormats.JSON:
                    SerializeAsJsonAndSaveToFile();
                    break;
                case FileFormats.XML:
                    SerializeAsXmlAndSaveToFile();
                    break;
                default:
                    break;
            }
        }
        private void SerializeAsJsonAndSaveToFile()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,  // Pretty-print JSON
                Converters = { new EstateJsonConverter(), new PersonJsonConverter(), new PaymentJsonConverter() }  // Ensure the custom converter is used
            };

            var json = JsonSerializer.Serialize(new RootObject
            {
                EstateList = _estateManager.GetAll(),
                PersonList = _personManager.GetAll(),
                PaymentList = _paymentManager.GetAll()
            },
                options);

            File.WriteAllText(_appState.FileName, json);

            // Set AppState
            _appState.IsDirty = false;
            _appState.Format = FileFormats.JSON;
        }

        private void SerializeAsXmlAndSaveToFile()
        {
            // Serialize the list to XML
            string xml = XmlHelper.SerializeToXml(new RootObject { EstateList = _estateManager.GetAll(), PersonList = _personManager.GetAll(), PaymentList = _paymentManager.GetAll() });
            // string xml = XmlHelper.SerializeToXml(_paymentManager.GetAll());
            File.WriteAllText(_appState.FileName, xml);
        }

        private void ClearManagers()
        {
            _estateManager.Clear();
            _personManager.Clear();
            _paymentManager.Clear();
        }

        private void LoadDataToManagers(RootObject rootObject)
        {
            foreach (var estate in rootObject.EstateList) _estateManager.Add(estate.ID, estate);
            foreach (var person in rootObject.PersonList) _personManager.Add(person.ID, person);
            foreach (var payment in rootObject.PaymentList) _paymentManager.Add(payment.ID, payment);
        }
    }
}
