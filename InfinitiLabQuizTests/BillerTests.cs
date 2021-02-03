using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfinitiLabQuiz.Models;
using InfinitiLabQuiz;

namespace InfinitiLabQuizTests
{
    [TestClass]
    public class BillerTests
    {
        private List<Customer> _customers;
        private List<Bill> _bills;

        [TestInitialize]
        public void Initialize()
        {
            CreateSampleData();
        }        

        [TestMethod]
        public void Biller_MultipleMatchingBillsAndAmount_CustomerIdsReturned()
        {
            // Arrange
            Biller biller = new Biller();
            decimal amountToMatch = 280;

            // Act
            List<int> result = biller.GetPossibleCustomerIdsForOutstandingAmount(_bills, amountToMatch).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(_customers[0].Id));
            Assert.IsTrue(result.Contains(_customers[1].Id));
        }

        [TestMethod]
        public void Biller_SingleMatchingBillsAndAmount_OneCustomerIdReturned()
        {
            // Arrange
            Biller biller = new Biller();
            decimal amountToMatch = 500;

            // Act
            List<int> result = biller.GetPossibleCustomerIdsForOutstandingAmount(_bills, amountToMatch).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(_customers[2].Id, result[0]);
        }

        [TestMethod]
        public void Biller_NonMatchingBillsAndAmount_EmptyListReturned()
        {
            // Arrange
            Biller biller = new Biller();
            decimal amountToMatch = 100; //invalid amount

            // Act
            List<int> result = biller.GetPossibleCustomerIdsForOutstandingAmount(_bills, amountToMatch).ToList();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        #region Helper methods
        private void CreateSampleData()
        {
            _customers = new List<Customer>();
            _bills = new List<Bill>();

            for (int i = 0; i < 5; i++)
            {
                _customers.Add(new Customer() { Id = i + 1, Name = "Customer " + i.ToString() });
            }

            //Customer 1
            //1 unpaid & 1 partially paid bills
            //Total billed: 400
            //Total outstanding: 280
            Bill bill1 = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = _customers[0].Id,
                BillAmount = 400,
                BillDate = DateTime.Today,
                PaidAmount = 120,
                PaidDate = null
            };
            _bills.Add(bill1);

            //Customer 2
            //2 unpaid bills
            //Total billed: 280
            //Total outstanding: 280
            Bill bill2 = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = _customers[1].Id,
                BillAmount = 100,
                BillDate = DateTime.Today,
                PaidAmount = 0,
                PaidDate = null
            };
            _bills.Add(bill2);

            Bill bill3 = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = _customers[1].Id,
                BillAmount = 180,
                BillDate = DateTime.Today,
                PaidAmount = 0, 
                PaidDate = null
            };
            _bills.Add(bill3);

            //Customer 3
            //1 unpaid bill
            //Total billed: 500
            //Total outstanding: 500
            Bill bill4 = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = _customers[2].Id,
                BillAmount = 500,
                BillDate = DateTime.Today,
                PaidAmount = 0,
                PaidDate = null
            };
            _bills.Add(bill4);            
        }
        #endregion
    }
}

    



