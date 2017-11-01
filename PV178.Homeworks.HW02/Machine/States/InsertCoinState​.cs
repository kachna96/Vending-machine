using PV178.Homeworks.HW02.Machine.ControlUnit;
using PV178.Homeworks.HW02.Model;

namespace PV178.Homeworks.HW02.Machine.States
{
    /// <summary>
    /// Class for managing the machine before user throws in coins
    /// </summary>
    public class InsertCoinState​ : State
    {
        public override IControlUnit ControlUnit { get; protected set; }

        public override decimal Credit { get; protected set; } = 0;

        public override Coordinates SelectedCoordinates { get; protected set; }

        /// <summary>
        /// Create new 'blank' InsertCoinState
        /// </summary>
        /// <param name="unit">Control unit</param>
        public InsertCoinState(IControlUnit unit) : this(null, unit)
        {
        }

        /// <summary>
        /// Create new InsertCoinState, respect previous state properties
        /// </summary>
        /// <param name="state">Previous state of this machine</param>
        /// <param name="unit">Control unit</param>
        /// <exception cref="ArgumentNullException">If control unit is null</exception>
        public InsertCoinState(IState state, IControlUnit unit)
        {
            base.CheckControlUnitOnNull(unit);
            ControlUnit = unit;
            ControlUnit.SwitchToState(this);
            if (state != null)
            {
                Credit = state.Credit;
                SelectedCoordinates = state.SelectedCoordinates;
            }
        }

        public override void RaiseCredit(decimal value)
        {
            base.RaiseCredit(this, value);
            ControlUnit.SwitchToState(new SelectCoordinatesState(this, ControlUnit));
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
