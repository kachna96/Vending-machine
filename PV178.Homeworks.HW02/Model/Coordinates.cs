using System;

namespace PV178.Homeworks.HW02.Model
{
    /// <summary>
    /// Struct for handling with coordinates for each product
    /// </summary>
    public struct Coordinates
    {
        public int Row { get; private set; }
        public char Col { get; private set; }
        public static readonly Coordinates Empty = new Coordinates(0, '\0');

        /// <summary>
        /// Create new coordinates for a product
        /// </summary>
        /// <param name="row">Row of a product</param>
        /// <param name="col">Column of a product</param>
        public Coordinates(int row, char col)
        {
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return String.Format("[{0};{1}]", Row, Col);
        }

        /// <summary>
        /// Determine if two objects of this instance are equal based on Row and Column coordinates
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>True if they are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Coordinates c = (Coordinates) obj;
            return c.Row == Row && c.Col == Col;
        }

        /// <summary>
        /// Get hash code based on Row and Column
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return String.Format("{0} + {1}", Row, Col).GetHashCode();
        }
    }
}
