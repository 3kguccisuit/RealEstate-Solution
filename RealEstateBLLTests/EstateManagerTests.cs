using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateDLL.Managers;
using DTO.Enums;
using System;
using DTO.Models.BaseModels;
using DTO.Models;
using DTO.Models.ConcreteModels;

namespace RealEstateBLLTests
{
    [TestClass]
    public class EstateManagerTests
    {
        private EstateManager _estateManager;

        [TestInitialize]
        public void Setup()
        {
            _estateManager = new EstateManager();
        }

        [TestMethod]
        public void Add_ShouldAddEstate()
        {
            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );

            // Act
            _estateManager.Add(estate.ID, estate);

            // Assert
            Assert.AreEqual(1, _estateManager.GetAll().Count, "The count of all estates should be 1.");
        }

        [TestMethod]
        public void Remove_ShouldRemoveEstate()
        {
            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );
            _estateManager.Add(estate.ID, estate);

            // Act
            _estateManager.Remove(estate.ID);

            // Assert
            Assert.AreEqual(0, _estateManager.GetAll().Count, "The count of all estates should be 0.");
        }

        [TestMethod]
        public void Update_ShouldUpdateEstate()
        {
            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );
            _estateManager.Add(estate.ID, estate);

            Estate updatedEstate = new Apartment(estate.ID,
                    new Address("123 Main St", "17523", "Malmo", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    0
                );

            // Act
            _estateManager.Update(estate.ID, updatedEstate);

            // Assert
            var estateFromManager = _estateManager.Get(estate.ID);
            Assert.AreEqual("Malmo", estateFromManager.Address.City, "The city of the estate should be Malmo.");
            Assert.AreEqual(LegalFormType.Ownership, estateFromManager.LegalForm.FormType, "The legal form of the estate should be Ownership.");
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllEstates()
        {
            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );

            Estate estate2 = new Hospital(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Malmo", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    0
                );
            _estateManager.Add(estate.ID, estate);
            _estateManager.Add(estate2.ID, estate2);
            // Act
            var estates = _estateManager.GetAll();

            // Assert
            Assert.AreEqual(2, estates.Count, "The count of all estates should be 2.");
        }

        [TestMethod]
        public void GetEstatesByCity_ShouldReturnCorrectEstates_ForGivenCity()
        {

            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );

            Estate estate2 = new Hospital(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Malmo", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    0
                );

            _estateManager.Add(estate.ID, estate);
            _estateManager.Add(estate2.ID, estate2);

            // Act
            var stockholmEstates = _estateManager.GetEstatesByCity("Stockholm");

            // Assert
            Assert.AreEqual(1, stockholmEstates.Count, "There should be 1 estates in Stockholm.");
        }

        [TestMethod]
        public void GetEstatesByCity_ShouldBeCaseInsensitive()
        {
            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );

            _estateManager.Add(estate.ID, estate);

            // Act
            var estates = _estateManager.GetEstatesByCity("stockholm");

            // Assert
            Assert.AreEqual(1, estates.Count, "The city search should be case-insensitive.");
        }

        [TestMethod]
        public void GetEstatesByCountry_ShouldReturnCorrectEstates_ForGivenCountry()
        {
            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );

            Estate estate2 = new Hospital(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Malmo", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    0
                );

            _estateManager.Add(estate.ID, estate);
            _estateManager.Add(estate2.ID, estate2);

            // Act
            var swedishEstates = _estateManager.GetEstatesByCountry(Country.Sverige);

            // Assert
            Assert.AreEqual(2, swedishEstates.Count, "There should be 2 estates in Sweden.");
        }

        [TestMethod]
        public void GetEstatesByType_ShouldReturnCorrectEstates_ForGivenType()
        {
            // Arrange
            Estate estate = new Apartment(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Stockholm", Country.Sverige),
                    new LegalForm(LegalFormType.Rental),
                    0,
                    0
                );

            Estate estate2 = new Hospital(Guid.NewGuid().ToString(),
                    new Address("123 Main St", "17523", "Malmo", Country.Sverige),
                    new LegalForm(LegalFormType.Ownership),
                    0,
                    0
                );

            _estateManager.Add(estate.ID, estate);
            _estateManager.Add(estate2.ID, estate2);

            // Act
            var apartmentEstates = _estateManager.GetEstatesByType("Apartment");

            // Assert
            Assert.AreEqual(1, apartmentEstates.Count, "There should be 1 apartment estate.");
        }
    }
}