using System;
using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;
using PV178.Homeworks.HW02.Exceptions;

namespace PV178.Homeworks.HW02.Machine.States
{
    /// <summary>
    /// Parent class for other machine states, implementing IState interfacec
    /// </summary>
    public abstract class State : IState
    {
        public abstract IControlUnit ControlUnit { get; protected set; }
        public abstract decimal Credit { get; protected set; }
        public abstract Coordinates SelectedCoordinates { get; protected set; }

        public abstract void RaiseCredit(decimal value);
        public abstract void SelectProduct(Coordinates coordinates);
        public abstract void TryDeliverProduct();

        /// <summary>
        /// Checks whether control unit is null
        /// </summary>
        /// <param name="unit">Control unit to check</param>
        /// <exception cref="ArgumentNullException">If control unit is null</exception>
        protected void CheckControlUnitOnNull(IControlUnit unit)
        {
            if (Object.ReferenceEquals(unit, null))
            {
                throw new ArgumentNullException("Control unit you passed as a param is null");
            }
        }

        /// <summary>
        /// Checks whether machine state is null
        /// </summary>
        /// <param name="state">Machine state</param>
        /// <exception cref="ArgumentNullException">If state is null</exception>
        private void CheckStateOnNull(State state)
        {
            if (Object.ReferenceEquals(state, null))
            {
                throw new ArgumentNullException("State you passed as a param is null");
            }
        }

        /// <summary>
        /// Check whether credit is less than
        /// </summary>
        /// <param name="credit">Credit to check</param>
        /// <returns>True if credit is less than zero, false otherwise</returns>
        private bool CheckCreditOnZeroOrBelow(decimal credit)
        {
            return credit <= 0;
        }

        /// <summary>
        /// Raise credit in desired state
        /// </summary>
        /// <param name="state">Current machine state</param>
        /// <param name="value">Credit to add</param>
        /// <exception cref="ArgumentException">If credit is less or equal to zero</exception>
        /// <exception cref="ArgumentNullException">If state is null</exception>
        protected void RaiseCredit(State state, decimal value)
        {
            CheckStateOnNull(state);
            if (CheckCreditOnZeroOrBelow(value))
            {
                throw new ArgumentException("Value is lower or equal to zero.");
            }
            state.Credit += value;
            Console.WriteLine("Credit: {0},- CZK", state.Credit);
        }

        /// <summary>
        /// Select product in desired state
        /// </summary>
        /// <param name="state">Current machine state</param>
        /// <param name="coordinates">Desired coordinates</param>
        /// <exception cref="ArgumentNullException">If state is null</exception>
        /// <seealso cref="ControlUnit.GetStockFromCoordinates" for possible exceptions when handling with coordinates />
        /// <returns>True if operation was completed successfully, false otherwise</returns>
        protected bool SelectProduct(State state, Coordinates coordinates)
        {
            CheckStateOnNull(state);
            if (CheckCreditOnZeroOrBelow(state.Credit))
            {
                Console.WriteLine("Insert coin first.");
                return false;
            }
            if (Object.ReferenceEquals(state.ControlUnit.GetStockFromCoordinates(coordinates), null))
            {
                Console.WriteLine("There is no stock available at {0} {1}", coordinates.Col, coordinates.Row);
                return false;
            }
            state.SelectedCoordinates = coordinates;
            Console.WriteLine("Row: {0} and column: {1} are now selected.", coordinates.Row, coordinates.Col);
            return true;
        }

        /// <summary>
        /// Try deliver product for a customer
        /// </summary>
        /// <param name="state">Current machine state</param>
        /// <returns>True if product was delivered successfully, false otherwise</returns>
        /// <exception cref="ArgumentNullException">If state is null</exception>
        /// <exception cref="StockUnavailableException">If quantity of a desired product is less or equal to zero</exception>
        /// <remarks>If unsuccessful, set selected coordinates to Coordinates.Empty and switch to SelectCoordinatesState</remarks>
        protected bool TryDeliverProduct(State state)
        {
            CheckStateOnNull(state);
            if (CheckCreditOnZeroOrBelow(state.Credit))
            {
                Console.WriteLine("Insert coin first.");
                return false;
            }
            if (object.ReferenceEquals(state.SelectedCoordinates, null) || state.SelectedCoordinates.Col == '\0')
            {
                Console.WriteLine("No coordinates were given.");
                return false;
            }
            if (state.ControlUnit.GetStockFromCoordinates(state.SelectedCoordinates).Product.Price > state.Credit)
            {
                Console.WriteLine("Not enough credit.");
                return false;
            }
            try
            {
                state.ControlUnit.GetStockFromCoordinates(state.SelectedCoordinates).DispatchStock();
                Console.WriteLine("Delivered {0}, returned {1},- CZK.", state.ControlUnit.GetStockFromCoordinates(state.SelectedCoordinates).Product.ToString(),
                    state.Credit - state.ControlUnit.GetStockFromCoordinates(state.SelectedCoordinates).Product.Price);
            }
            catch (StockUnavailableException ex)
            {
                Console.WriteLine(ex.Message);
                state.SelectedCoordinates = Coordinates.Empty;
                state.ControlUnit.SwitchToState(new SelectCoordinatesState(state, state.ControlUnit));
                return false;
            }
            return true;
        }
    }
}
