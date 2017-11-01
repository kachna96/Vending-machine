using System.Collections.Generic;
using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Machine
{
    /// <summary>
    /// Represents contract between vending machine and user
    /// </summary>
    public interface IVendingMachine
    {
        /// <summary>
        /// Vending machine control logic
        /// </summary>
        IControlUnit ControlUnit { get; }

        /// <summary>
        /// Gets all offered items with corresponding description
        /// </summary>
        /// <returns>Vending machine products description</returns>
        IEnumerable<string> GetCurrentOffer();

        /// <summary>
        /// Inserts coin with specified value into vending machine
        /// </summary>
        /// <param name="value">The value of the coin</param>
        void InsertCoin(decimal value);

        /// <summary>
        /// Perform selection of product according to given coordinates
        /// </summary>
        /// <param name="coordinates">Entered coordinates</param>
        void SelectCoordinates(Coordinates coordinates);

        /// <summary>
        /// Confirms user order
        /// </summary>
        void ConfirmOrder();
    }
}
