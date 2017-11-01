using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Machine.States;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Tests.Tests
{
    [TestClass]
    public class StateTests
    {
        private IControlUnit controlUnit;

        [TestInitialize]
        public void SetUp()
        {
            // arrange
            controlUnit = new ControlUnit(
                            new[] { 0, 1, 2, 3, 4, 5, 6, 7 },
                            new[] { 'A', 'B', 'C', 'D', 'E', 'F' });
            ModelInitializer.Initialize(controlUnit);

            new InsertCoinState(this.controlUnit);
        }

        #region RaiseCreditMethodTest

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RaiseCredit_NegativeCreditPassed_RaisesException()
        {
            // arrange & act
            SetupRaiseCredit(-5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RaiseCredit_ZeroCreditPassed_RaisesException()
        {
            // arrange & act
            SetupRaiseCredit(0);
        }

        [TestMethod]
        public void RaiseCredit_GreaterThanZeroCreditPassed_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupRaiseCredit(10);

            // assert
            Assert.AreEqual("Credit: 10,- CZK", result,
                "RaiseCredit(...) - raise credit by value greater than zero - console log is not correct");
        }

        [TestMethod]
        public void RaiseCredit_MultipleAddition_ReturnsCorrectMessage()
        {
            // arrange & act
            SetupRaiseCredit(10);
            var result = SetupRaiseCredit(20);

            // assert
            Assert.AreEqual("Credit: 30,- CZK", result,
                "RaiseCredit(...) - multiple credit raise - console log is not correct");
        }

        #endregion

        #region SelectProductMethodTest

        [TestMethod]
        public void SelectProduct_ChooseValidCoordinatesWithNonEmptyStockAndCredit_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupSelectProduct(new Coordinates(3, 'D'), 50);

            // assert
            Assert.AreEqual("Row: 3 and column: D are now selected.", result,
                "SelectProduct(...) - valid coordinates selection with non-empty stock and credit - console log is not correct");
        }

        [TestMethod]
        public void SelectProduct_ChooseValidCoordinatesWithNonEmptyStockAndNoCredit_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupSelectProduct(new Coordinates(3, 'D'), null);

            // assert
            Assert.AreEqual("Insert coin first.", result,
                "SelectProduct(...) - valid coordinates selection with non-empty stock and no credit - console log is not correct");
        }

        #endregion

        #region TryDeliverStockMethodTest

        [TestMethod]
        public void TryDeliverStock_StockWithZeroUnits_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupTryDeliverStock(new Coordinates(4, 'C'), 50);
            
            // assert
            Assert.AreEqual("No units of Twix 40g available.", result,
                "TryDeliverStock(...) - order of unavailable product - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_StockWithNonZeroUnits_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupTryDeliverStock(new Coordinates(5, 'B'), 50);

            //assert
            Assert.AreEqual("Delivered Snickers 50g for 20,- CZK, returned 30,- CZK.", result,
                "TryDeliverStock(...) - order of available product - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_NoCoordinatesSelected_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupTryDeliverStock(null, 50);

            //assert
            Assert.AreEqual("No coordinates were given.", result,
                "TryDeliverStock(...) - order with empty coordinates - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_NoCoinInsertedAndNoCoordinatesGiven_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupTryDeliverStock(null, null);

            //assert
            Assert.AreEqual("Insert coin first.", result,
                "TryDeliverStock(...) - order with no coin inserted - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_NoCoinInsertedAndCoordinatesGiven_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupTryDeliverStock(new Coordinates(5, 'B'), null);

            //assert
            Assert.AreEqual("Insert coin first.", result,
                "TryDeliverStock(...) - order with no coin inserted and valid coordinates given - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_NotEnoughCredit_ReturnsCorrectMessage()
        {
            // arrange & act
            var result = SetupTryDeliverStock(new Coordinates(5, 'B'), 10);

            //assert
            Assert.AreEqual("Not enough credit.", result,
                "TryDeliverStock(...) - valid order with insufficient credit - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_CreditSetToZeroAfterSuccessfullConfirmation_ReturnsCorrectMessage()
        {
            // arrange & act
            SetupTryDeliverStock(new Coordinates(5, 'B'), 50);
            var result = SetupRaiseCredit(10);

            //assert
            Assert.AreEqual("Credit: 10,- CZK", result,
                "TryDeliverStock(...) - credit was not set to zero after successfull order confirmation - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_CreditRemainsAfterUnsuccessfulConfirmation_ReturnsCorrectMessage()
        {
            // arrange & act
            // unsuccessful confirmation due to low credit
            SetupTryDeliverStock(new Coordinates(5, 'B'), 10);
            var result = SetupRaiseCredit(10);

            //assert
            Assert.AreEqual("Credit: 20,- CZK", result,
                "TryDeliverStock(...) - credit was not increased after unsuccessful order confirmation - console log is not correct");
        }

        [TestMethod]
        public void TryDeliverStock_CoordinatesValueAfterSuccessfullConfirmation_CoordinatesAreCorrectlySet()
        {
            // arrange & act
            SetupTryDeliverStock(new Coordinates(5, 'B'), 50);

            //assert
            Assert.AreEqual(Coordinates.Empty, controlUnit.State.SelectedCoordinates,
                "TryDeliverStock(...) - coordinates were not set to Coordinates.Empty after successfull order confirmation");
        }

        #endregion

        #region HelperMethods

        private string SetupRaiseCredit(decimal credit)
        {
            // arrange
            Action action = () => controlUnit.State.RaiseCredit(credit);
            
            // act
            return TestHelperMethods.CaptureConsoleOutput(action);
        }

        private string SetupSelectProduct(Coordinates coords, decimal? credit)
        {
            // arrange         
            ConditionallyAssignCredit(credit);
            Action action = () => controlUnit.State.SelectProduct(coords);
            
            // act
            return TestHelperMethods.CaptureConsoleOutput(action);
        }

        private string SetupTryDeliverStock(Coordinates? coords, decimal? credit)
        {
            // arrange         
            ConditionallyAssignCredit(credit);
            if (coords.HasValue)
            {
                controlUnit.State.SelectProduct(coords.Value);
            }

            Action action = () => controlUnit.State.TryDeliverProduct();
           
            // act
            return TestHelperMethods.CaptureConsoleOutput(action);
        }

        private void ConditionallyAssignCredit(decimal? credit)
        {
            if (credit.HasValue)
            {
                controlUnit.State.RaiseCredit(credit.Value);
            }
        }

        #endregion
    }
}
