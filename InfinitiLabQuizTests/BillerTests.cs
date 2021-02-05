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
        public void Biller_MatchingSumAndIndividualBill_CustomerIdsReturned()
        {
            // Arrange
            Biller biller = new Biller();
            decimal amountToMatch = 280;

            // Act
            List<int> result = biller.GetPossibleCustomerIdsForOutstandingAmount(_bills, amountToMatch).ToList();

            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result.Contains(_customers[0].Id));
            Assert.IsTrue(result.Contains(_customers[1].Id));
            Assert.IsTrue(result.Contains(_customers[3].Id));
            Assert.IsTrue(result.Contains(_customers[5].Id));
        }

        [TestMethod]
        public void Biller_MatchingIndividualBill_CustomerIdsReturned()
        {
            // Arrange
            Biller biller = new Biller();
            decimal amountToMatch = 100;

            // Act
            List<int> result = biller.GetPossibleCustomerIdsForOutstandingAmount(_bills, amountToMatch).ToList();

            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.IsTrue(result.Contains(_customers[1].Id));
            Assert.IsTrue(result.Contains(_customers[3].Id));
            Assert.IsTrue(result.Contains(_customers[4].Id));
            Assert.IsTrue(result.Contains(_customers[5].Id));
            Assert.IsTrue(result.Contains(_customers[6].Id));
        }

        [TestMethod]
        public void Biller_MatchingInvidualAndOnlyOneBill_OneCustomerIdReturned()
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
        public void Biller_NonMatchingBill_EmptyListReturned()
        {
            // Arrange
            Biller biller = new Biller();
            decimal amountToMatch = 230; //invalid amount

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

            for (int i = 0; i < 7; i++)
            {
                _customers.Add(new Customer() { Id = i + 1, Name = "Customer " + i.ToString() });
            }

            //Customer 1
            //1 unpaid & 1 partially paid bills
            //Total billed: 400
            //Total outstanding: 280
            //Outstanding: <280>
            CreateBill(400, 120, _customers[0].Id);

            //Customer 2
            //2 unpaid bills
            //Total billed: 280
            //Total outstanding: 280
            //Outstanding: <100, 180>
            CreateBill(100, 0, _customers[1].Id);
            CreateBill(180, 0, _customers[1].Id);

            //Customer 3
            //1 unpaid bill
            //Total billed: 500
            //Total outstanding: 500
            //Outstanding: <500>
            CreateBill(500, 0, _customers[2].Id);

            //Customer 4
            //2 unpaid bills
            //Total billed: 400
            //Total outstanding: 380
            //Outstanding: <280,100>
            CreateBill(300, 20, _customers[3].Id);
            CreateBill(100, 0, _customers[3].Id);

            //Customer 5
            //2 unpaid bills
            //Total billed: 250
            //Total outstanding: 250
            //Outstanding: <100,150>
            CreateBill(100, 0, _customers[4].Id);
            CreateBill(150, 0, _customers[4].Id);

            //Customer 6
            //4 unpaid bills
            //Total billed: 380
            //Total outstanding: 380
            //Outstanding: <100,100,100,80>
            CreateBill(100, 0, _customers[5].Id);
            CreateBill(100, 0, _customers[5].Id);
            CreateBill(100, 0, _customers[5].Id);
            CreateBill(80, 0, _customers[5].Id);

            //Customer 7
            //4 unpaid bills
            //Total billed: 400
            //Total outstanding: 400
            //Outstanding: <100,100,100,100>
            CreateBill(100, 0, _customers[6].Id);
            CreateBill(100, 0, _customers[6].Id);
            CreateBill(100, 0, _customers[6].Id);
            CreateBill(100, 0, _customers[6].Id);
        }

        private void CreateBill(decimal billAmount, decimal paidAmount, int customerId)
        {
            _bills.Add(new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                BillAmount = billAmount,
                BillDate = DateTime.Today,
                PaidAmount = paidAmount,
                PaidDate = null
            });
            
        }
        #endregion
    }
}

    



