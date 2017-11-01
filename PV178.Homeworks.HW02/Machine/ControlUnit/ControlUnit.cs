using System;
using System.Collections.Generic;
using System.Linq;
using PV178.Homeworks.HW02.Machine.States;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Machine.ControlUnit
{
    /// <summary>
    /// Class for managing control unit for this machine
    /// </summary>
    public class ControlUnit : IControlUnit
    {
        public IDictionary<Coordinates, Stock> Goodies { get; private set; } = new Dictionary<Coordinates, Stock>();
        public int[] RowIdentifiers { get; private set; }
        public char[] ColIdentifiers { get; private set; }
        public IState State { get; private set; }

        /// <summary>
        /// Create new control unit with these params
        /// </summary>
        /// <param name="rowIdentifiers">Row range</param>
        /// <param name="columnIdentifiers">Column range</param>
        public ControlUnit(int[] rowIdentifiers, char[] columnIdentifiers)
        {
            RowIdentifiers = (int[])rowIdentifiers.Clone();
            ColIdentifiers = (char[])columnIdentifiers.Clone();
        }

        /// <summary>
        /// Check whether coordinates are in range of this control unit
        /// </summary>
        /// <param name="coordinates">Coordinates to check</param>
        /// <exception cref="ArgumentNullException">If coordinates are null</exception>
        /// <exception cref="ArgumentOutOfRangeException">If row or column coorinates are out of range</exception>
        private void CheckCoordinates(Coordinates coordinates)
        {
            if (Object.ReferenceEquals(coordinates, null))
            {
                throw new ArgumentNullException("Coordinates you passed as a param are null");
            }
            if (coordinates.Row > RowIdentifiers.Last() || coordinates.Row < 0)
            {
                throw new ArgumentOutOfRangeException("Row coordinates are out of range. Your param: {0}, max param: {1}", coordinates.Row, RowIdentifiers.Last().ToString());
            }
            if (coordinates.Col > ColIdentifiers.Last() || coordinates.Col < 'A')
            {
                throw new ArgumentOutOfRangeException("Col coordinates are out of range. Your param: {0}, max param: {1}", coordinates.Col, ColIdentifiers.Last().ToString());
            }
        }

        public Stock GetStockFromCoordinates(Coordinates coordinates)
        {
            CheckCoordinates(coordinates);
            return Goodies.FirstOrDefault(c => c.Key.Col == coordinates.Col && c.Key.Row == coordinates.Row).Value;
        }

        public IDictionary<Coordinates, Stock> GetStocksDictionary()
        {
            return Goodies;
        }

        public void SetStockOnCoordinates(Coordinates coordinates, Stock stock)
        {
            if (Object.ReferenceEquals(stock, null))
            {
                throw new ArgumentNullException("Stock you passed as a param is null");
            }
            if (Object.ReferenceEquals(coordinates, null))
            {
                throw new ArgumentNullException("Coordinates you passed as a param are null");
            }
            CheckCoordinates(coordinates);
            Goodies[coordinates] = stock;
        }

        public void SwitchToState(IState state)
        {
            if (Object.ReferenceEquals(state, null))
            {
                throw new ArgumentNullException("State you passed as a param is null");
            }
            State = state;
        }
    }
}
