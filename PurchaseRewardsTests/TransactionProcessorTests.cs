using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseRewards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRewards.Tests
{
    [TestClass()]
    public class TransactionProcessorTests
    {
        [TestMethod()]
        public void processTransactionTest()
        {
            string stringTransactionList = "12346,04/11/2012,$112.00\n2122,01-21-2012,200.00\n345,02/11/2012,$19.00\n345,13/11/2012,$19.00\nv,02/11/2012,$19.00\n119,02/11/2012,k\n";
            List<Transaction> goodTransactions = new List<Transaction>() 
            {
                new Transaction(){CustomerId = 12346, TransactionDate = DateTime.Parse("04/11/2012"), TransactionPoints = 74,TransactionAmount = decimal.Parse("112.00") },
                new Transaction(){CustomerId = 2122, TransactionDate = DateTime.Parse("01-21-2012"), TransactionPoints = 250,TransactionAmount =  decimal.Parse("200.00")},
                new Transaction(){CustomerId = 345, TransactionDate = DateTime.Parse("02/11/2012"), TransactionPoints = 0,TransactionAmount =  decimal.Parse("19.00") }

            };
            List<string> errorTransactions = new List<string>() { "345,13/11/2012,$19.00", "v,02/11/2012,$19.00", "119,02/11/2012,k" };
            List<Transaction> goodProcessedTransactions = new List<Transaction>();
            List<string> errorProcessedTransactions = new List<string>();
            TransactionProcessor transactionProcessor = new TransactionProcessor();
            foreach (string transaction in stringTransactionList.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None))
            {
                transactionProcessor.processTransaction(transaction, goodProcessedTransactions, errorProcessedTransactions);
            }
            CollectionAssert.Equals(goodTransactions, goodProcessedTransactions);
            CollectionAssert.AreEqual(errorTransactions, errorProcessedTransactions);
       }

        [TestMethod()]
        public void processThreeMonthsOfTransactionsTest()
        {
            List<Transaction> goodProcessedTransactions = new List<Transaction>();
            TransactionProcessor transactionProcessor = new TransactionProcessor();
            transactionProcessor.returnThreeMonthsOfTransactions(goodProcessedTransactions, DateTime.Now);
        }

        [TestMethod()]
        public void transactionAmountProcessorTest()
        {
            TransactionProcessor transactionProcessor = new TransactionProcessor();
            Assert.AreEqual((decimal)2.00,transactionProcessor.transactionAmountProcessor("$2.00"));
            Assert.AreEqual((decimal)2.00, transactionProcessor.transactionAmountProcessor("2.00"));
            Assert.AreEqual((decimal)0, transactionProcessor.transactionAmountProcessor("0"));
            Assert.AreEqual((decimal)0, transactionProcessor.transactionAmountProcessor("k"));
            Assert.AreEqual((decimal)0, transactionProcessor.transactionAmountProcessor(" "));
        }
    }
}