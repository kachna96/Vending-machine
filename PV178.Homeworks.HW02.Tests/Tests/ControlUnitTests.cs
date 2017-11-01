using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Machine.States;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Tests.Tests
{
    [TestClass]
    public class ControlUnitTests
    {
        private IControlUnit controlUnit;

        private readonly Stock hubaBubba = new Stock(new Product("Huba Bubba 15g", 10), 2);

        [TestInitialize]
        public void SetUp()
        {
            // arrange
            controlUnit = new ControlUnit(
                            new[] { 0, 1, 2, 3, 4, 5, 6, 7 },
                            new[] { 'A', 'B', 'C', 'D', 'E', 'F' });

            ModelInitializer.Initialize(controlUnit);
        }

        /// <summary>
        /// Switch state method tests depends on proper implementation of control unit states
        /// </summary>
        #region SwitchToStateMethodTests

        [TestMethod]
        public void SwitchToState_SwitchFromInsertCoinToSelectCoordinatesState_HasCorrectStateType()
        {
            // arrange
            new InsertCoinState(controlUnit);

            // act
            controlUnit.State.RaiseCredit(50);

            Assert.IsInstanceOfType(controlUnit.State, typeof(SelectCoordinatesState), "SwitchToState(...) - switch from insert coin to select coordinates - the resulted state of the control unit does not correspond to expected type (SelectCoordinatesState)");
        }

        /// <summary>
        /// Switch state test methods depends on proper implementation of control unit states
        /// </summary>
        [TestMethod]
        public void SwitchToState_SwitchFromInsertCoinToConfirmOrderState_HasCorrectStateType()
        {
            // arrange
            new InsertCoinState(controlUnit);

            // act
            controlUnit.State.RaiseCredit(50);
            controlUnit.State.SelectProduct(new Coordinates(3, 'D'));

            Assert.IsInstanceOfType(controlUnit.State, typeof(ConfirmOrderState), "SwitchToState(...) - switch from insert coin to confirm order - the resulted state of the control unit does not correspond to expected type (ConfirmOrderState)");
        }

        /// <summary>
        /// Switch state test methods depends on proper implementation of control unit states
        /// </summary>
        [TestMethod]
        public void SwitchToState_SwitchFromInsertCoinBackToSameState_HasCorrectStateType()
        {
            // arrange
            new InsertCoinState(controlUnit);

            // act
            controlUnit.State.RaiseCredit(50);
            controlUnit.State.SelectProduct(new Coordinates(3, 'D'));
            controlUnit.State.TryDeliverProduct();

            Assert.IsInstanceOfType(controlUnit.State, typeof(InsertCoinState), "SwitchToState(...) - switch from insert coin back to insert coin state - the resulted state of the control unit does not correspond to expected type (InsertCoinState)");
        }

        #endregion

        #region GetStockFromCoordinatesMethodTests

        [TestMethod]
        public void GetStockFromCoordinates_ValidCoordinatesWithNonEmptyStock_ReturnsCorrespondingStock()
        {
            // arrange
            var coords = new Coordinates(7, 'F');

            // act
            var stock = controlUnit.GetStockFromCoordinates(coords);
            var a = new Stock(new Product("Mattoni 250ml", 25), 5);
            Assert.AreEqual(new Stock(new Product("Mattoni 250ml", 25), 5), stock, "GetStockFromCoordinates(...) - for given valid coordinates the resulted stock does not equal the expected one");
        }

        [TestMethod]
        public void GetStockFromCoordinates_ValidCoordinatesWithEmptyStock_ReturnsNull()
        {
            // arrange
            var coords = new Coordinates(0, 'F');

            // act
            var stock = controlUnit.GetStockFromCoordinates(coords);

            Assert.AreEqual(null, stock, "GetStockFromCoordinates(...) - for given valid coordinates the resulted stock is not null");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void GetStockFromCoordinates_EmptyCoordinates_RaisesException()
        {
            // arrange
            var coords = Coordinates.Empty;

            // act
            controlUnit.GetStockFromCoordinates(coords);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void GetStockFromCoordinates_CoordinatesWithInvalidRowIndex_RaisesException()
        {
            // arrange
            var coords = new Coordinates(-9, 'B');

            // act
            controlUnit.GetStockFromCoordinates(coords);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void GetStockFromCoordinates_CoordinatesWithInvalidColumnIndex_RaisesException()
        {
            // arrange
            var coords = new Coordinates(4, 'G');

            // act
            controlUnit.GetStockFromCoordinates(coords);
        }

        #endregion

        #region SetStockOnCoordinatesMethodTests

        [TestMethod]
        public void SetStockOnCoordinates_ValidCoordinates_AssignsGivenStock()
        {
            // arrange
            var coords = new Coordinates(5, 'F');

            // act
            controlUnit.SetStockOnCoordinates(coords, hubaBubba);

            Assert.AreEqual(hubaBubba, controlUnit.GetStockFromCoordinates(coords), "GetStockFromCoordinates(...) - for given valid coordinates the resulted stock does not equal the expected one");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SetStockOnCoordinates_EmptyCoordinates_RaisesException()
        {
            // arrange
            var coords = Coordinates.Empty;

            // act
            controlUnit.SetStockOnCoordinates(coords, hubaBubba);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SetStockOnCoordinates_CoordinatesWithInvalidRowIndex_RaisesException()
        {
            // arrange
            var coords = new Coordinates(-9, 'B');

            // act
            controlUnit.SetStockOnCoordinates(coords, hubaBubba);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SetStockOnCoordinates_CoordinatesWithInvalidColumnIndex_RaisesException()
        {
            // arrange
            var coords = new Coordinates(4, 'G');

            // act
            controlUnit.SetStockOnCoordinates(coords, hubaBubba);
        }

        #endregion

        #region GetStocksDictionaryMethodTest

        [TestMethod]
        public void GetStocksDictionary_AllInitializedStocks_VerifyEqualityByExpectedOutput()
        {
            var expected = new Dictionary<Coordinates, Stock>
            {
                {new Coordinates(3, 'D'), new Stock(new Product("Coca Cola 250ml", 30), 8)},
                {new Coordinates(2, 'D'), new Stock(new Product("Sprite 250ml", 25), 6)},
                {new Coordinates(0, 'A'), new Stock(new Product("Bake Rolls 150g", 35), 4)},
                {new Coordinates(5, 'B'), new Stock(new Product("Snickers 50g", 20), 9)},
                {new Coordinates(2, 'F'), new Stock(new Product("Evian 500ml", 30), 1)},
                {new Coordinates(7, 'E'), new Stock(new Product("Bohemia Chips 25g", 25), 4)},
                {new Coordinates(4, 'C'), new Stock(new Product("Twix 40g", 15), 0)},
                {new Coordinates(7, 'F'), new Stock(new Product("Mattoni 250ml", 25), 5)}
            };

            var dictionaryToCheck = controlUnit.GetStocksDictionary();

            if (dictionaryToCheck == null || dictionaryToCheck.Count != expected.Count)
            {
                Assert.Fail("Dictionary to check is null or does not contain same amount of key value pairs as the expected one.");
            }
            foreach (var keyValuePair in expected)
            {
                if (!dictionaryToCheck.ContainsKey(keyValuePair.Key) || !dictionaryToCheck[keyValuePair.Key].Equals(keyValuePair.Value))
                {
                    var a = dictionaryToCheck[keyValuePair.Key];
                    var b = keyValuePair.Value;
                    Assert.Fail($"Dictionary to check does not contain key {keyValuePair.Key} or value within this key ({keyValuePair.Value}) does not equal the expected one.");
                }
            }
        } 

        #endregion
    }
}
