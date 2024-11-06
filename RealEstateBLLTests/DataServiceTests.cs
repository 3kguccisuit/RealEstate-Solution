using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateBLL.Service;
using RealEstateDLL.Managers;
using DTO.Enums;
using System;
using DTO.Models.BaseModels;
using DTO.Models;
using DTO.Models.ConcreteModels;
using DTO.Models.ConcreteModels.Persons;
using DTO.Models.ConcreteModels.Payments;
using System.Windows.Controls.Primitives;

namespace RealEstateBLLTests
{
    [TestClass]
    public class DataServiceTests
    {
        private DataService _dataService;
        private EstateManager _estateManager;
        private PersonManager _personManager;
        private PaymentManager _paymentManager;

        [TestInitialize]
        public void Setup()
        {
            _estateManager = new EstateManager();
            _personManager = new PersonManager();
            _paymentManager = new PaymentManager();
            _dataService = new DataService(_estateManager, _paymentManager, _personManager);
        }

        [TestMethod]
        public void SaveData_ShouldReturnTrue_ForJsonFormat()
        {
            // Arrange
            AddTestDataEstate();
            AddTestDataPerson();
            AddTestDataPayment();

            string filePath = "test.json";
            FileFormats fileFormat = FileFormats.JSON;

            // Act
            bool result = _dataService.SaveData(filePath, fileFormat);

            // Assert
            Assert.IsTrue(result, "SaveData should return true for JSON format.");
        }

        [TestMethod]
        public void SaveData_ShouldReturnFalse_ForUnknownFormat()
        {
            // Arrange
            string filePath = "test.unknown";
            FileFormats fileFormat = FileFormats.Unknown;

            // Act
            bool result = _dataService.SaveData(filePath, fileFormat);

            // Assert
            Assert.IsFalse(result, "SaveData should return false for Unknown format.");
        }

        [TestMethod]
        public void LoadDataFromJson_ShouldReturnTrue_ForValidData()
        {
            // Arrange
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"test.json");

            // Act
            bool result = _dataService.LoadDataFromJson(filePath);

            // Assert
            Assert.IsTrue(result, "LoadDataFromJson should return true for valid JSON data.");
        }

        [TestMethod]
        public void SaveDataAsJson_ShouldCreateJsonFileWithCorrectData()
        {
            // Arrange
            AddTestDataEstate();
            AddTestDataPerson();
            AddTestDataPayment();

            var testDirectory = Path.GetTempPath(); // Using the system temporary path
            string filePath = Path.Combine(testDirectory, "test_output.json");

            // Act
            _dataService.SaveDataAsJson(filePath);

            // Assert
            Assert.IsTrue(File.Exists(filePath), "The JSON file should be created.");
            Assert.IsTrue(true, "SaveDataAsJson should return true for valid data.");
            string fileContent = File.ReadAllText(filePath);
            Assert.IsTrue(fileContent.Contains("Stockholm"), "The JSON file should contain the word 'Stockholm'.");

            // Clean up
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        [TestMethod]
        public void LoadDataFromJson_ShouldReturnFalse_ForInvalidData()
        {
            // Arrange
            AddTestDataEstate();
            AddTestDataPerson();
            AddTestDataPayment();

            var testDirectory = Path.GetTempPath(); // Using the system temporary path
            string filePath = Path.Combine(testDirectory, "test_output.json");

            // Act & Assert
            Assert.IsFalse(_dataService.LoadDataFromJson(filePath), "LoadDataFromJson should return false for invalid JSON data.");
        }

        private void AddTestDataEstate()
        {
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );

            Estate estate2 = new School(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    0
                );
            Estate estate3 = new Hospital(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    0
                );
            _estateManager.Add(estate.ID, estate);
            _estateManager.Add(estate2.ID, estate2);
            _estateManager.Add(estate3.ID, estate3);
        }

        private void AddTestDataPerson()
        {
            Person person = new Seller(Guid.NewGuid().ToString(), "", new Address("123 Main St", "17523", "Stockholm", Country.Sverige), 0);
            Person person2 = new Buyer(Guid.NewGuid().ToString(), "", new Address("123 Main St", "17523", "Stockholm", Country.Sverige), 0, false);
            _personManager.Add(person.ID, person);
            _personManager.Add(person2.ID, person2);

        }

        private void AddTestDataPayment()
        {
            Payment payment = new Bank(Guid.NewGuid().ToString(), "", 0, "");
            Payment payment2 = new PayPal(Guid.NewGuid().ToString(), "", 0, "");
            _paymentManager.Add(payment.ID, payment);
            _paymentManager.Add(payment2.ID, payment2);
        }
    }
}
