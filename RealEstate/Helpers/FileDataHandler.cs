using RealEstate.Core.Enums;
using RealEstate.Core.Libs;
using RealEstate.Core.Models;
using RealEstate.Core.Services;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using RealEstate.Contracts.Services;
using RealEstate.ViewModels;
using Serilog;
using System.Windows;
using RealEstate.Models;

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
                try
                {
                    return JsonSerializer.Deserialize<RootObject>(content, options);
                }
                catch (JsonException ex)
                {
                    Log.Information("The selected file is not a valid JSON file.", ex);

                    // Show a message box to the user
                    MessageBox.Show("The selected file is not a valid JSON file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    // Return null or handle it gracefully to prevent crashing
                    return null;
                }
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
            OpenFile("xml", "*.xml", FileFormats.XML, (content) =>
            {
                try
                {
                    // Try to deserialize the XML content
                    var ret = XmlHelper.DeserializeFromXml<RootObject>(content);
                    return ret;
                }
                catch (InvalidDataException ex)
                {
                    // Handle the InvalidDataException
                    Log.Information("The selected file is not a valid XML file.", ex);
                    MessageBox.Show("The selected file is not a valid XML file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Return null or handle the error as needed
                    return null;
                }
                catch (Exception ex)
                {
                    // Handle any other unexpected exceptions
                    Log.Information("An unexpected error occurred.", ex);
                    MessageBox.Show("An unexpected error occurred while processing the XML file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            });
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
                try
                {
                    using (var reader = new StreamReader(_appState.FileName))
                    {
                        var content = reader.ReadToEnd();
                        var rootObject = deserialize(content);

                        ClearManagers();
                        LoadDataToManagers(rootObject);

                        _appState.IsDirty = false;
                        _appState.Format = format;
                        _navigationService.NavigateTo(typeof(MainViewModel).FullName);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Log.Information("The selected file could not be found.", ex);
                    MessageBox.Show("The selected file could not be found.");
                    //throw new FileNotFoundException("The selected file could not be found.", ex);
                }
                catch (IOException ex)
                {
                    Log.Information("An error occurred while accessing the file.", ex);
                    MessageBox.Show("An error occurred while accessing the file.");
                   // throw new IOException("An error occurred while accessing the file.", ex);
                }
            }
        }

        private void SaveFile(string defaultExt, string filter, Func<string> serialize)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog { FileName = "Document", DefaultExt = $".{defaultExt}", Filter = $"{defaultExt.ToUpper()}|{filter}|All Files|*.*" };
            if (dialog.ShowDialog() == true)
            {
                _appState.FileName = dialog.FileName;
                try
                {
                    using (var writer = new StreamWriter(_appState.FileName))
                    {
                        var content = serialize();
                        writer.Write(content);
                        _appState.IsDirty = false;
                    }
                }
                catch (IOException ex)
                {
                    Log.Information("An error occurred while saving the file.", ex);
                    throw new IOException("An error occurred while saving the file.", ex);
                }
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
                WriteIndented = true,
                Converters = { new EstateJsonConverter(), new PersonJsonConverter(), new PaymentJsonConverter() }
            };

            try
            {
                var json = JsonSerializer.Serialize(new RootObject
                {
                    EstateList = _estateManager.GetAll(),
                    PersonList = _personManager.GetAll(),
                    PaymentList = _paymentManager.GetAll()
                }, options);

                File.WriteAllText(_appState.FileName, json);
                _appState.IsDirty = false;
            }
            catch (IOException ex)
            {
                Log.Information("An error occurred while saving the JSON file.", ex);
                throw new IOException("An error occurred while saving the JSON file.", ex);
            }
        }

        private void SerializeAsXmlAndSaveToFile()
        {
            try
            {
                var xml = XmlHelper.SerializeToXml(new RootObject
                {
                    EstateList = _estateManager.GetAll(),
                    PersonList = _personManager.GetAll(),
                    PaymentList = _paymentManager.GetAll()
                });

                File.WriteAllText(_appState.FileName, xml);
                _appState.IsDirty = false;
            }
            catch (IOException ex)
            {
                Log.Information("An error occurred while saving the XML file.", ex);
                throw new IOException("An error occurred while saving the XML file.", ex);
            }
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
            if(rootObject == null)
            {
                return;
            }
            foreach (var estate in rootObject.EstateList) _estateManager.Add(estate.ID, estate);
            foreach (var person in rootObject.PersonList) _personManager.Add(person.ID, person);
            foreach (var payment in rootObject.PaymentList) _paymentManager.Add(payment.ID, payment);
        }
    }
}
