using System;
using System.Collections.Generic;
using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;
using PV178.Homeworks.HW02.Machine.States;

namespace PV178.Homeworks.HW02.Machine
{
    /// <summary>
    /// Class for managing the actual vending machine
    /// </summary>
    public class VendingMachine : IVendingMachine
    {
        public IControlUnit ControlUnit { get; private set; }

        /// <summary>
        /// Create new vending machine
        /// </summary>
        /// <param name="controlUnit">Control unit</param>
        /// <exception cref="ArgumentNullException">If control unit is null</exception>
        public VendingMachine(IControlUnit controlUnit)
        {
            if (controlUnit == null)
            {
                throw new ArgumentNullException("Control unit you passed as a param is null.");
            }
            ControlUnit = controlUnit;
            controlUnit.SwitchToState(new InsertCoinState(controlUnit));
        }

        public void ConfirmOrder()
        {
            ControlUnit.State.TryDeliverProduct();
        }

        public IEnumerable<string> GetCurrentOffer()
        {
            List<string> result = new List<string>();
            foreach (var item in ControlUnit.GetStocksDictionary())
            {
                result.Add(item.Key.ToString() + " " + item.Value.ToString());
            }
            return result;
        }

        public void InsertCoin(decimal value)
        {
            ControlUnit.State.RaiseCredit(value);
        }

        public void SelectCoordinates(Coordinates coordinates)
        {
            ControlUnit.State.SelectProduct(coordinates);
        }
    }
}
