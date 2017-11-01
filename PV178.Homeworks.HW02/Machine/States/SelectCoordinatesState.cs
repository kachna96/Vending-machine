using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Machine.States
{
    /// <summary>
    /// Class for managing the machine when user is selecting coordinates of desired product
    /// </summary>
    public class SelectCoordinatesState : State
    {
        public override IControlUnit ControlUnit { get; protected set; }

        public override decimal Credit { get; protected set; }

        public override Coordinates SelectedCoordinates { get; protected set; }

        /// <summary>
        /// Create new 'blank' SelectCoordinatesState (but in fact this will just create new 'blank' InsertCoinState(IControlUnit)
        /// </summary>
        /// <param name="unit">Control unit</param>
        public SelectCoordinatesState(IControlUnit unit) : this(null, unit)
        {
        }

        /// <summary>
        /// Create new SelectCoordinatesState, respect previous state properties
        /// </summary>
        /// <param name="state">Previous state of this machine</param>
        /// <param name="unit">Control unit</param>
        /// <exception cref="ArgumentNullException">If control unit is null</exception>
        public SelectCoordinatesState(IState state, IControlUnit unit)
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
            if (base.SelectProduct(this, coordinates))
            {
                ControlUnit.SwitchToState(new ConfirmOrderState(this, ControlUnit));
            }
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
