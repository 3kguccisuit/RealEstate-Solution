using NUnit.Framework;
using RealEstateDAL.Files;
using DTO.Models;
using System.IO;
using DTO.Models.BaseModels;
using DTO.Enums;
using DTO.Models.ConcreteModels.Persons;
using DTO.Models.ConcreteModels.Payments;
using DTO.Models.ConcreteModels;

namespace RealEstateDALTests
{
    [TestFixture]
    public class FileRepositoryTests
    {
        private FileRepository _fileRepository;
        private string _testFilePath;

        [SetUp]
        public void Setup()
        {
            _fileRepository = new FileRepository();
            _testFilePath = Path.Combine(Path.GetTempPath(), "test_output.json");
        }

        [TearDown]
        public void Cleanup()
        {
            // Delete the test file after each test to maintain a clean test environment
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void SaveDataAsJson_ShouldCreateJsonFileWithCorrectContent()
        {
            // Arrange
            var rootObject = new RootObject
            {
                EstateList = new List<Estate> { new Apartment { ID = Guid.NewGuid().ToString(), Address = new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                LegalForm = new LegalForm(LegalFormType.Ownership), NumberOfRooms=0, FloorLevel=0 } },
                PersonList = new List<Person> { new Seller { ID = Guid.NewGuid().ToString(), Name = "Alice", Address = new Address("123 Main St", "17523", "Stockholm", Country.Sverige) ,
                AskingPrice = 100000, } },
                PaymentList = new List<Payment> { new Bank { ID = Guid.NewGuid().ToString(), Name = "Payment1", Amount = 1000 } }
            };

            // Act
            _fileRepository.SaveDataAsJson(rootObject, _testFilePath);

            // Assert
            Assert.IsTrue(File.Exists(_testFilePath), "The JSON file should be created.");
            var fileContent = File.ReadAllText(_testFilePath);
            Assert.IsTrue(fileContent.Contains("Stockholm"), "The file content should contain the city 'Stockholm'.");
            Assert.IsTrue(fileContent.Contains("Alice"), "The file content should contain the person name 'Alice'.");
            Assert.IsTrue(fileContent.Contains("1000"), "The file content should contain the payment amount '1000'.");
        }

        [Test]
        public void LoadDataFromJson_ShouldReturnCorrectRootObject()
        {
            // Arrange
            var rootObject = new RootObject
            {
                EstateList = new List<Estate> { new Apartment { ID = Guid.NewGuid().ToString(), Address = new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                LegalForm = new LegalForm(LegalFormType.Ownership), NumberOfRooms=0, FloorLevel=0 } },
                PersonList = new List<Person> { new Seller { ID = Guid.NewGuid().ToString(), Name = "Alice", Address = new Address("123 Main St", "17523", "Stockholm", Country.Sverige) ,
                AskingPrice = 100000, } },
                PaymentList = new List<Payment> { new Bank { ID = Guid.NewGuid().ToString(), Name = "Payment1", Amount = 1000 } }
            };

            _fileRepository.SaveDataAsJson(rootObject, _testFilePath);

            // Act
            var loadedRootObject = _fileRepository.LoadDataFromJson(_testFilePath);

            // Assert
            Assert.IsNotNull(loadedRootObject, "The loaded RootObject should not be null.");
            Assert.AreEqual(rootObject.EstateList.Count, loadedRootObject.EstateList.Count, "The loaded RootObject should have the same number of estates.");
            Assert.AreEqual(rootObject.PersonList.Count, loadedRootObject.PersonList.Count, "The loaded RootObject should have the same number of persons.");
            Assert.AreEqual(rootObject.PaymentList.Count, loadedRootObject.PaymentList.Count, "The loaded RootObject should have the same number of payments.");
        }

        [Test]
        public void LoadDataFromJson_ShouldReturnNull_WhenFileDoesNotExist()
        {
            // Arrange
            var nonexistentFilePath = Path.Combine(Path.GetTempPath(), "non_existent.json");
            var fileRepository = new FileRepository();

            // Act
            var result = fileRepository.LoadDataFromJson(nonexistentFilePath);

            // Assert
            Assert.IsNull(result, "LoadDataFromJson should return null when the file does not exist.");
        }

    }
}
