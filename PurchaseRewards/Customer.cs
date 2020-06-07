using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRewards
{
    public class Customer
    {
        private int customerID;
        private int customerPoints;
        public int CustomerID { get => customerID; set => customerID = value; }
        public int CustomerPoints { get => customerPoints; set => customerPoints = value; }
    }
}
