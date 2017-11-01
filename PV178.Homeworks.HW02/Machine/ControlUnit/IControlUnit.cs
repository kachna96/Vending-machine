using System.Collections.Generic;
using PV178.Homeworks.HW02.Machine.States;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Machine.ControlUnit
{
    /// <summary>
    /// Represents vending machine control logic
    /// </summary>
    public interface IControlUnit
    {
        /// <summary>
        /// Current vending machine state
        /// </summary>
        IState State { get; }

        /// <summary>
        /// Performs switch to other vending machine state
        /// </summary>
        /// <param name="state">State to switch to</param>
        void SwitchToState(IState state);

        /// <summary>
        /// Gets all available stocks in the form of dictionary, where:
        /// key is represented by coordinates of the respective stock
        /// value is simply the stock itself
        /// </summary>
        /// <returns>Dictionary of all vending machine stocks</returns>
        IDictionary<Coordinates, Stock> GetStocksDictionary();

        /// <summary>
        /// Gets stock at given coordinates
        /// </summary>
        /// <param name="coordinates">Stock coordinates</param>
        /// <returns>Stock at given coordinates or null if there is none</returns>
        Stock GetStockFromCoordinates(Coordinates coordinates);

        /// <summary>
        /// Sets stock at given coordinates
        /// </summary>
        /// <param name="coordinates">Stock coordinates</param>
        /// <param name="stock">Stock to set</param>
        void SetStockOnCoordinates(Coordinates coordinates, Stock stock);
    }
}
