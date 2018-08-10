using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParkingLotManager;
using ParkingLotManager.Model;

namespace ParkingLotManagerTest
{
    [TestClass]
    public class LotManagerTest
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetTotalMoneyCollected_WhenPassingInNull_ShouldThrowArgumentException()
        {
            ParkingLot lot = null;

            lot.GetTotalMoneyCollected();
        }

        [TestMethod]
        public void GetTotalMoneyCollected_WhenPassingEmptyParkingLot_ShouldReturnZero()
        {
            var lot = new ParkingLot(5);

            var cost=lot.GetTotalMoneyCollected();

            Assert.AreEqual(0, cost);
        }

        [TestMethod]
        public void GetTotalMoneyCollected_WhenPassingParkingLotOneCompactVehicle_ShouldReturnDollor20()
        {
            var lot = new ParkingLot(5);

            lot.ParkVehicle(new CompactVehicle());

            var cost = lot.GetTotalMoneyCollected();

            Assert.AreEqual(20, cost);
        }
    }
}
