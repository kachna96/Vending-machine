using PV178.Homeworks.HW02.Machine.ControlUnit;

namespace PV178.Homeworks.HW02.Model
{
    /// <summary>
    /// Helper class for vending machine data initialization
    /// </summary>
    public static class ModelInitializer
    {
        /// <summary>
        /// Initializes vending machine with several stocks
        /// </summary>
        /// <param name="controlUnit"></param>
        public static void Initialize(IControlUnit controlUnit)
        {
            var cocaCola = CreateStock("Coca Cola 250ml", 30, 8);
            var sprite = CreateStock("Sprite 250ml", 25, 6);
            var bakeRolls = CreateStock("Bake Rolls 150g", 35, 4);
            var snickers = CreateStock("Snickers 50g", 20, 9);
            var evian = CreateStock("Evian 500ml", 30, 1);
            var bohemiaChips = CreateStock("Bohemia Chips 25g", 25, 4);
            var twix = CreateStock("Twix 40g", 15, 0);
            var mattoni = CreateStock("Mattoni 250ml", 25, 5);

            controlUnit.SetStockOnCoordinates(new Coordinates(3, 'D'), cocaCola);
            controlUnit.SetStockOnCoordinates(new Coordinates(2, 'D'), sprite);
            controlUnit.SetStockOnCoordinates(new Coordinates(0, 'A'), bakeRolls);
            controlUnit.SetStockOnCoordinates(new Coordinates(5, 'B'), snickers);
            controlUnit.SetStockOnCoordinates(new Coordinates(2, 'F'), evian);
            controlUnit.SetStockOnCoordinates(new Coordinates(7, 'E'), bohemiaChips);
            controlUnit.SetStockOnCoordinates(new Coordinates(4, 'C'), twix);
            controlUnit.SetStockOnCoordinates(new Coordinates(7, 'F'), mattoni);
        }

        /// <summary>
        /// Creates stock according to given arguments
        /// </summary>
        /// <param name="name">Name of the product within the stock</param>
        /// <param name="price">Price of the product within the stock</param>
        /// <param name="quantity">Stock quantity</param>
        /// <returns>Created stock</returns>
        private static Stock CreateStock(string name, decimal price, int quantity)
        {
            return new Stock(new Product(name, price), quantity);
        }
    }
}
