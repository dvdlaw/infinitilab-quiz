using System;
using System.Collections.Generic;
using InfinitiLabQuiz;
using InfinitiLabQuiz.ErrorMessages;
using InfinitiLabQuiz.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfinitiLabQuizTests
{
    [TestClass]
    public class BillerValidatorTests
    {        
        private Biller _biller;

        [TestInitialize]
        public void Initialize()
        {
            _biller = new Biller();
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), ErrorMessageBiller.AmountToMatch_Invalid)]
        public void BillerValidator_AmountToMatchZero_ThrowsAmountZeroException()
        {
            // Arrange            
            decimal amountToMatch = -10; //invalid amount
            List<Bill> bills = new List<Bill>();
            Bill bill1 = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = 1,
                BillAmount = 100,
                BillDate = DateTime.Today,
                PaidAmount = 0,
                PaidDate = null
            };
            bills.Add(bill1);             

          // Act
          _biller.GetPossibleCustomerIdsForOutstandingAmount(bills, amountToMatch);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), ErrorMessageBiller.OutstandingBills_Empty)]
        public void BillerValidator_EmptyBills_ThrowsEmptyBillsException()
        {
            // Arrange
            List<Bill> bills = new List<Bill>();

            // Act
            _biller.GetPossibleCustomerIdsForOutstandingAmount(bills, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), ErrorMessageBiller.OutstandingBills_FullyPaid)]
        public void BillerValidator_HasPaidBills_ThrowsHasFullyPaidException()
        {
            // Arrange
            List<Bill> bills = new List<Bill>();
            Bill bill1 = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = 1,
                BillAmount = 100,
                BillDate = DateTime.Today,
                PaidAmount = 100,
                PaidDate = null
            };
            bills.Add(bill1);

            // Act
            _biller.GetPossibleCustomerIdsForOutstandingAmount(bills, 100);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), ErrorMessageBiller.OutstandingBills_HasPaidDate)]
        public void BillerValidator_HasPaidDate_ThrowsHasPaidDateException()
        {
            // Arrange
            List<Bill> bills = new List<Bill>();
            Bill bill1 = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = 1,
                BillAmount = 100,
                BillDate = DateTime.Today,
                PaidAmount = 0,
                PaidDate = DateTime.Today
            };
            bills.Add(bill1);

            // Act
            _biller.GetPossibleCustomerIdsForOutstandingAmount(bills, 100);
        }
    }
}
