using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PurchaseRewards
{
    public class TransactionProcessor
    {
        public void processTransaction(string transactionLine, List<Transaction> transactionList, List<string> errorList)
        {
            if (transactionLine != null && transactionLine.Length > 0 && transactionLine.Contains(','))
            {
                var transactionStringArray = transactionLine.Split(',');
                if (transactionStringArray != null && transactionStringArray.Length == 3)
                {
                    var transaction = new Transaction();
                    int parsedCustomerId = 0;
                    transaction.CustomerId = int.TryParse(transactionStringArray[0], out parsedCustomerId) ? parsedCustomerId : parsedCustomerId;
                    var transactionDate = DateTime.MinValue;
                    transaction.TransactionDate = DateTime.TryParse(transactionStringArray[1], out transactionDate) ? transactionDate : transactionDate;
                    transaction.TransactionAmount = transactionAmountProcessor(transactionStringArray[2]);
                    PointsProcessor pointsProcessor = new PointsProcessor();
                    transaction.TransactionPoints = pointsProcessor.ProcessPoints(transaction.TransactionAmount);
                    bool IsError = false;
                    if (parsedCustomerId == 0)
                    {
                        Console.WriteLine("There was an error processing the customer Id of " + transactionStringArray[0]);
                        IsError = true;
                    }
                    if (transaction.TransactionDate == DateTime.MinValue)
                    {
                        Console.WriteLine("There was an error processing the transaction date of " + transactionStringArray[1]);
                        IsError = true;
                    }
                    if (transaction.TransactionAmount == 0)
                    {
                        Console.WriteLine("There was an error processing the transaction amount of " + transactionStringArray[2]);
                        IsError = true;
                    }
                    if (!IsError)
                    {
                        transactionList.Add(transaction);
                    }
                    else
                    {
                        errorList.Add(transactionLine);
                    }
                }
                else
                {
                    //transaction line is null or empty and does not contains a csv
                    errorList.Add(transactionLine);
                }
            }
        }
        public List<Transaction> returnThreeMonthsOfTransactions(List<Transaction> transactionList,DateTime startDate) 
        {
            List<Transaction> threeMonthsOfTransactions = new List<Transaction>();
            int startMonth = startDate.Month;
            //what happens on year change
            if (startMonth > 10)
            {
                int endMonth = startDate.Month + 3 - 12;
                threeMonthsOfTransactions = transactionList.OrderBy(x => x.TransactionDate).Where(x => x.TransactionDate.Month >= startDate.Month && x.TransactionDate.Month <= 12).ToList();
                threeMonthsOfTransactions.AddRange(transactionList.OrderBy(x => x.TransactionDate).Where(x => (x.TransactionDate.Year == startDate.Year + 1) && (x.TransactionDate.Month < endMonth)).ToList());
            }
            else
            {
                threeMonthsOfTransactions = transactionList.Where(x => x.TransactionDate.Month >= startMonth && x.TransactionDate.Month < startMonth + 3).ToList();
            }
            return threeMonthsOfTransactions;
            
        }
        public decimal transactionAmountProcessor(string transactionAmountString)
        {
            string transactionAmountStringNoDollarSign = transactionAmountString.Contains('$') ? transactionAmountString.Replace('$', ' ') : transactionAmountString;
            decimal parsedTransactionAmount = decimal.TryParse(transactionAmountStringNoDollarSign, out parsedTransactionAmount) ? parsedTransactionAmount : 0;
            return parsedTransactionAmount;
        }
    }
}
