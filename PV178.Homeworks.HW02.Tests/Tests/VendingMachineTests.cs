using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Tests.Tests
{
    /// <summary>
    /// Methods: InsertCoin, SelectCoordinates and ConfirmOrder are 
    /// just proxies therefore only GetCurrentOffer has unit tests
    /// </summary>
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void GetCurrentOffer_AllInitializedItemsDictionary_ReturnCorrectResult()
        {
            // arrange
            var expected = new List<string>
            {
                "[3;D] Coca Cola 250ml for 30,- CZK (available: 8x)",
                "[2;D] Sprite 250ml for 25,- CZK (available: 6x)",
                "[0;A] Bake Rolls 150g for 35,- CZK (available: 4x)",
                "[5;B] Snickers 50g for 20,- CZK (available: 9x)",
                "[7;E] Bohemia Chips 25g for 25,- CZK (available: 4x)",
                "[4;C] Twix 40g for 15,- CZK (available: 0x)",
                "[7;F] Mattoni 250ml for 25,- CZK (available: 5x)"
            };
            var input = new Dictionary<Coordinates, Stock>
            {
                {new Coordinates(3, 'D'), new Stock(new Product("Coca Cola 250ml", 30), 8)},
                {new Coordinates(2, 'D'), new Stock(new Product("Sprite 250ml", 25), 6)},
                {new Coordinates(0, 'A'), new Stock(new Product("Bake Rolls 150g", 35), 4)},
                {new Coordinates(5, 'B'), new Stock(new Product("Snickers 50g", 20), 9)},
                {new Coordinates(7, 'E'), new Stock(new Product("Bohemia Chips 25g", 25), 4)},
                {new Coordinates(4, 'C'), new Stock(new Product("Twix 40g", 15), 0)},
                {new Coordinates(7, 'F'), new Stock(new Product("Mattoni 250ml", 25), 5)}
            };
            var controlUnitMock = new Mock<IControlUnit>();
            controlUnitMock.Setup(unit => unit.GetStocksDictionary()).Returns(input);
            var vendingMachine = new Machine.VendingMachine(controlUnitMock.Object);

            // act
            var res = vendingMachine.GetCurrentOffer().ToList();

            // assert
            if (res == null || res.Count != expected.Count)
            {
                Assert.Fail("Resulted collection is null or does not contain same amount of items as the expected one.");
            }

            foreach (var expectedItemDescription in expected)
            {
                if(!res.Contains(expectedItemDescription))
                {
                    Assert.Fail($"Resulted collection does not contain expected item: {expectedItemDescription}.");
                }
            }
        }
    }
}
