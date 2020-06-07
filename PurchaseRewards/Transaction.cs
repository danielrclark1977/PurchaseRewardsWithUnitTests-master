using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRewards
{
    public class Transaction
    {
        private DateTime transactionDate;
        private int customerId;
        private decimal transactionAmount;
        private int transactionPoints;

        public DateTime TransactionDate { get => transactionDate; set => transactionDate = value; }
        public int CustomerId { get => customerId; set => customerId = value; }
        public decimal TransactionAmount { get => transactionAmount; set => transactionAmount = value; }
        public int TransactionPoints { get => transactionPoints; set => transactionPoints = value; }
    }
}
