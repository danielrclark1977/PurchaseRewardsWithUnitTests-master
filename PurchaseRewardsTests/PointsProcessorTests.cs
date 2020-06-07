using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseRewards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchaseRewards.Tests
{
    [TestClass()]
    public class PointsProcessorTests
    {
        [TestMethod()]
        public void ProcessPointsTest()
        {
            PointsProcessor pointsProcessor = new PointsProcessor();
            Assert.AreEqual<int>(0, pointsProcessor.ProcessPoints(0));
            Assert.AreEqual<int>(25, pointsProcessor.ProcessPoints(75));
            Assert.AreEqual<int>(150, pointsProcessor.ProcessPoints(150));
            Assert.AreEqual<int>(750, pointsProcessor.ProcessPoints(450));
        }
    }
}