using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateDLL.Managers;
using DTO.Enums;
using System;
using DTO.Models.BaseModels;
using DTO.Models;
using DTO.Models.ConcreteModels;
using DTO.Models.ConcreteModels.Payments;
using DTO.Models.ConcreteModels.Persons;

namespace RealEstateBLLTests
{
    [TestClass]
    public class PersonManagerTests
    {
        private PersonManager _PersonManager;

        [TestInitialize]
        public void Setup()
        {
            _PersonManager = new PersonManager();

        }

        [TestMethod]
        public void GetPersonsByCity_ShouldReturnCorrectPersons_ForGivenCity()
        {
            // Arrange
            Person person1 = new Buyer(Guid.NewGuid().ToString(), "John", new Address("123 Main St", "17523", "Stockholm", Country.Sverige), 1000, false);
            Person person2 = new Seller(Guid.NewGuid().ToString(), "Jane", new Address("123 Main St", "17523", "Malmo", Country.Sverige), 10000);
            _PersonManager.Add(person1.ID, person1);
            _PersonManager.Add(person2.ID, person2);

            // Act
            var persons = _PersonManager.GetPersonsByCity("Stockholm");

            // Assert
            Assert.AreEqual(1, persons.Count, "The count of persons should be 3.");
        }

    }
}