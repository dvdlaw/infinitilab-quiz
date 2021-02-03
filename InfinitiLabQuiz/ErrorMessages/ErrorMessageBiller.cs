namespace InfinitiLabQuiz.ErrorMessages
{
    public static class ErrorMessageBiller
    {
        public const string AmountToMatch_Invalid = "amountToMatch must be above 0.00";
        public const string OutstandingBills_Empty = "outstandingBills cannot be null or empty";
        public const string OutstandingBills_FullyPaid = "outstandingBills cannot contain fully paid bill(s)";
        public const string OutstandingBills_HasPaidDate = "outstandingBills cannot contain PaidDate with outstanding amount";
    }
}