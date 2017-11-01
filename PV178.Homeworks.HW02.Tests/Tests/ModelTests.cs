using Microsoft.VisualStudio.TestTools.UnitTesting;
using PV178.Homeworks.HW02.Exceptions;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Tests.Tests
{
    [TestClass]
    public class ModelTests
    {
        #region TestReadonlyFields

        private static readonly Product HubaBubba = new Product("Huba Bubba 15g", 15);

        private static readonly Stock StockWithGreaterThanZeroQuantity = new Stock(HubaBubba, 5);

        private static readonly Stock StockWithZeroQuantity = new Stock(HubaBubba, 0);

        private static readonly Stock StockWithNegativeQuantity = new Stock(HubaBubba, -1); 
        
        #endregion

        #region CoordinatesTests

        [TestMethod]
        public void Coordinates_EmptyCoordinates_HasCorrectValue()
        {
            Assert.AreEqual(new Coordinates(0, '\0'), Coordinates.Empty, "Coordinates - Coordinates.Empty - compared (expected) values are not correct.");
        }

        [TestMethod]
        public void Coordinates_ToString_ReturnCorrectOutput()
        {
            var coords = new Coordinates(1, 'C');
            Assert.AreEqual("[1;C]", coords.ToString(), "Coordinates - ToString() implementation is not correct.");
        }

        #endregion

        #region ProductTest

        [TestMethod]
        public void Product_ToString_ReturnCorrectOutput()
        {
            Assert.AreEqual("Huba Bubba 15g for 15,- CZK", HubaBubba.ToString(), "Product - ToString() implementation is not correct.");
        }

        #endregion

        #region StockTests

        [TestMethod]
        public void Stock_ToString_ReturnCorrectOutput()
        {
            var stock = new Stock(HubaBubba, 5);
            Assert.AreEqual("Huba Bubba 15g for 15,- CZK (available: 5x)", stock.ToString(), "Stock - ToString() implementation is not correct.");
        }

        [TestMethod]
        public void Stock_DispatchStockWithQuantityGreaterThanZero_DecreasesQuantity()
        {
            StockWithGreaterThanZeroQuantity.DispatchStock();
            Assert.AreEqual(4, StockWithGreaterThanZeroQuantity.Quantity, "DispatchStock - quantity was not decremented when DispatchStock was called.");
        }

        [TestMethod]
        [ExpectedException(typeof(StockUnavailableException))]
        public void Stock_DispatchStockWithNegativeQuantity_ThrowsStockUnavailableException()
        {
            StockWithNegativeQuantity.DispatchStock();
        } 

        #endregion
    }
}
