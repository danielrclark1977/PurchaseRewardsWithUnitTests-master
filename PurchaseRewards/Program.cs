using System;
using System.Collections.Generic;
using System.Linq;

namespace PurchaseRewards
{
    public class Program
    {
        //        A retailer offers a rewards program to its customers, awarding points based on each recorded purchase.
        //A customer receives 2 points for every dollar spent over $100 in each transaction, plus 1 point for every dollar spent over $50 in each transaction
        //(e.g.a $120 purchase = 2x$20 + 1x$50 = 90 points).
        //Given a record of every transaction during a three month period, calculate the reward points earned for each customer per month and total.
        //Make up a data set to best demonstrate your solution
        //Check solution into GitHub
        private static List<Transaction> transactionList = new List<Transaction>();
        private static List<string> errorList = new List<string>();
        private List<Customer> customerList = new List<Customer>();
        public static void Main(string[] args)
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory;
            var transactions = System.IO.File.ReadAllLines(dir + @"\transactionlist.txt");
            if (transactions != null)
            {
                foreach (string transactionLine in transactions)
                {
                    TransactionProcessor transactionProcessor = new TransactionProcessor();
                    transactionProcessor.processTransaction(transactionLine, transactionList, errorList);
                }
            }
            if (transactionList.Count > 0)
            {
                TransactionProcessor transactionProcessor = new TransactionProcessor();
                Console.WriteLine("Please enter a start date in the format of 01/01/1999 to see customer points for three months after this date");
                string startDateString = Console.ReadLine();
                DateTime startDate = DateTime.MinValue;
                if (!DateTime.TryParse(startDateString, out startDate))
                {
                    Console.WriteLine("Please enter a start date in the format of 01/01/1999");
                    startDateString = Console.ReadLine();
                }
                List<Transaction> threeMonthsOfTransactions = transactionProcessor.returnThreeMonthsOfTransactions(transactionList, startDate);
                if (threeMonthsOfTransactions.Count > 0)
                {
                    var transactionListCustomerGroups = threeMonthsOfTransactions.GroupBy(x => x.CustomerId).ToList();
                    if (transactionListCustomerGroups != null)
                    {
                        foreach (var group in transactionListCustomerGroups)
                        {
                            var months = group.GroupBy(x => x.TransactionDate.Month).ToList();
                            foreach (var month in months)
                            {
                                var totalPoints = month.Select(x => x.TransactionPoints).ToList().Sum();
                                Console.WriteLine("Reward points for Customer " + group.Key + " in " + month.Select(x => x.TransactionDate.ToString("MMMM")).First() + " "+ month.Select(x => x.TransactionDate.Year).First() + " is " + totalPoints + " points");
                            }
                        }

                    }
                }
            }
        }
    }
}
