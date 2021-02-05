using System.Collections.Generic;
using System.Linq;
using System;
using InfinitiLabQuiz.Models;
using InfinitiLabQuiz.ErrorMessages;

namespace InfinitiLabQuiz
{
    public class Biller
    {
        public IEnumerable<int> GetPossibleCustomerIdsForOutstandingAmount(List<Bill> outstandingBills, decimal amountToMatch)
        {
            Validate(outstandingBills, amountToMatch);
            return Process(outstandingBills, amountToMatch);
        }
        
        private IEnumerable<int> Process(List<Bill> outstandingBills, decimal amountToMatch)
        {
            List<int> result = new List<int>();
            IEnumerable<int> customerIds = outstandingBills.Select(x => x.CustomerId).Distinct();            

            foreach (int customerId in customerIds)
            {
                List<decimal> outstandingAmounts = outstandingBills.Where(x => x.CustomerId == customerId)
                                                            .Select(x => x.BillAmount - x.PaidAmount)
                                                            .ToList();

                if (HasPossibleMatchingSumOrIndividualAmount(outstandingAmounts, amountToMatch))
                    result.Add(customerId);
            }

            return result;
        }

        //main logic
        private bool HasPossibleMatchingSumOrIndividualAmount(List<decimal> outstandingAmounts, decimal amountToMatch)
        {
            for (int i=0; i < outstandingAmounts.Count; i++)
            {
                decimal left = amountToMatch - outstandingAmounts[i];
                
                if(left == 0)
                {
                    return true;
                }
                else
                {
                    List<decimal> possible = outstandingAmounts.Skip(i+1).Where(x => x <= left).ToList();
                    if (possible.Count > 0)
                    {
                        return HasPossibleMatchingSumOrIndividualAmount(possible, left);
                    }
                }
            }
            return false;
        }


        private void Validate(List<Bill> outstandingBills, decimal amountToMatch)
        {
            if (amountToMatch <= 0)
                throw new Exception(ErrorMessageBiller.AmountToMatch_Invalid);

            if(outstandingBills == null || outstandingBills.Count == 0)
                throw new Exception(ErrorMessageBiller.OutstandingBills_Empty);

            if (outstandingBills.Any(x => x.PaidAmount == x.BillAmount))
                throw new Exception(ErrorMessageBiller.OutstandingBills_FullyPaid);

            if (outstandingBills.Any(x => x.PaidDate != null && x.BillAmount - x.PaidAmount != 0))
                throw new Exception(ErrorMessageBiller.OutstandingBills_HasPaidDate);
        }
    }
}
