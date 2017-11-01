using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Machine.States
{
    /// <summary>
    /// Vending machine state
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Vending machine control unit
        /// </summary>
        IControlUnit ControlUnit { get; }

        /// <summary>
        /// Inserted credit
        /// </summary>
        decimal Credit { get; }

        /// <summary>
        /// Coordinates given by user
        /// </summary>
        Coordinates SelectedCoordinates { get; }

        /// <summary>
        /// Increases user credit according to given value (raises an exception when invalid parameter is given)
        /// </summary>
        /// <param name="value">The value of the coin inserted</param>
        void RaiseCredit(decimal value);

        /// <summary>
        /// Sets coordinates for desired product (raises an exception in case of invalid coordinates)
        /// </summary>
        /// <param name="coordinates"></param>
        void SelectProduct(Coordinates coordinates);

        /// <summary>
        /// Handles product delivery (catches StockUnavailableException)
        /// </summary>
        void TryDeliverProduct();
    }
}