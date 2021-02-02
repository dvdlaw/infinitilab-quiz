using InfinitiLabQuiz.Models;
using System.Collections.Generic;
using System.Linq;

namespace InfinitiLabQuiz
{
    public class Biller
    {
        public IEnumerable<int> GetPossibleCustomerIdsForOutstandingAmount(List<Bill> outstandingBills, decimal amountToMatch)
        {
            IEnumerable<int> customerIds = outstandingBills.Select(x => x.CustomerId).Distinct();
            List<int> result = new List<int>();

            foreach (int customerId in customerIds)
            {
                decimal totalBillAmount = outstandingBills.Where(x => x.CustomerId == customerId)
                                                .Select(x => x.BillAmount)
                                                .Sum();

                decimal totalPaidAmount = outstandingBills.Where(x => x.CustomerId == customerId)
                                                .Select(x => x.PaidAmount)
                                                .Sum();

                if (totalBillAmount - totalPaidAmount == amountToMatch)
                    result.Add(customerId);
            }

            return result;
        }
    }
}
