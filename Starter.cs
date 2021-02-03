using InfinitiLabQuiz.Models;
using System;
using System.Collections.Generic;

namespace InfinitiLabQuiz
{
    public class Starter
    {
        public static void Main(string[] args)
        {
            Biller biller = new Biller();
            Customer customer = new Customer() { Id = 1, Name = "David" };
            Bill bill = new Bill()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                BillAmount = 100,
                BillDate = DateTime.Today,
                PaidAmount = 0,
                PaidDate = null
            };

            IEnumerable<int> result = biller.GetPossibleCustomerIdsForOutstandingAmount(new List<Bill>() { bill }, 100);

            Console.WriteLine("Matching customer IDs: " + string.Join(",", result));
        }
    }
}
