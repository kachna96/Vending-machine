using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Machine.States
{
    /// <summary>
    /// Class for managing delivery of a product
    /// </summary>
    public class ConfirmOrderState​ : State
    {
        public override IControlUnit ControlUnit { get; protected set; }

        public override decimal Credit { get; protected set; }

        public override Coordinates SelectedCoordinates { get; protected set; }

        /// <summary>
        /// Create new 'blank' ConfirmOrderState​ (but in fact this will just create new 'blank' InsertCoinState(IControlUnit)
        /// </summary>
        /// <param name="unit">Control unit</param>
        public ConfirmOrderState​(IControlUnit unit) : this(null, unit)
        {
        }

        /// <summary>
        /// Create new ConfirmOrderState​, respect previous state properties
        /// </summary>
        /// <param name="state">Previous state of this machine</param>
        /// <param name="unit">Control unit</param>
        /// <exception cref="ArgumentNullException">If control unit is null</exception>
        public ConfirmOrderState​(IState state, IControlUnit unit)
        {
            if (state == null)
            {
                unit.SwitchToState(new InsertCoinState(unit));
            }
            base.CheckControlUnitOnNull(unit);
            ControlUnit = unit;
            Credit = state.Credit;
            SelectedCoordinates = state.SelectedCoordinates;
        }

        public override void RaiseCredit(decimal value)
        {
            base.RaiseCredit(this, value);
        }

        public override void SelectProduct(Coordinates coordinates)
        {
            base.SelectProduct(this, coordinates);
        }

        public override void TryDeliverProduct()
        {
            if (base.TryDeliverProduct(this))
            {
                ControlUnit.SwitchToState(new InsertCoinState(ControlUnit));
            }
        }
    }
}
