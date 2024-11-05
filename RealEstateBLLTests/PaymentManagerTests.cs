using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealEstateDLL.Managers;
using DTO.Enums;
using System;
using DTO.Models.BaseModels;
using DTO.Models;
using DTO.Models.ConcreteModels;
using DTO.Models.ConcreteModels.Payments;

namespace RealEstateBLLTests
{
    [TestClass]
    public class PaymentManagerTests
    {
        private PaymentManager _paymentManager;

        [TestInitialize]
        public void Setup()
        {
            _paymentManager = new PaymentManager();
        }

        [TestMethod]
        public void GetPaymentsByType_ShouldReturnCorrectEstates_ForGivenCity()
        {
            // Arrange
            Bank bank = new Bank(Guid.NewGuid().ToString(), "Bank of America", 100, "ibannumber");
            PayPal payPal = new PayPal(Guid.NewGuid().ToString(), "PayPal", 200, "email");
            _paymentManager.Add(bank.ID, bank);
            _paymentManager.Add(payPal.ID, payPal);

            // Act
            var payments = _paymentManager.GetPaymentsByType("Bank");

            // Assert
            Assert.AreEqual(1, payments.Count, "The count of payments should be 1.");
        }


    }
}