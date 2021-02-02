using System;
using System.Collections.Generic;
using System.Text;

namespace InfinitiLabQuiz.Models
{
    public class Bill
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
       
        private int _customerId;
        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private DateTime _billDate;
        public DateTime BillDate
        {
            get { return _billDate; }
            set { _billDate = value; }
        }

        private decimal _billAmount;
        public decimal BillAmount
        {
            get { return _billAmount; }
            set { _billAmount = value; }
        }

        private decimal _paidAmount;
        public decimal PaidAmount
        {
            get { return _paidAmount; }
            set { _paidAmount = value; }
        }

        private DateTime? _paidDate;
        public DateTime? PaidDate
        {
            get { return _paidDate; }
            set { _paidDate = value; }
        }

        //public Bill(Guid customerId, DateTime billDate, decimal billAmount)
        //{
        //    this.Id = Guid.NewGuid();
        //    this.CustomerId = customerId;
        //    this.BillDate = billDate;
        //    this.BillAmount = billAmount;
        //    this.PaidAmount = 0;
        //    this.PaidDate = null;
        //}       

    }
}
