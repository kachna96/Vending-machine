using System;

namespace PV178.Homeworks.HW02.Exceptions
{
    /// <summary>
    /// Thrown when available amount of stock is not sufficient to satisfy user order
    /// </summary>
    public class StockUnavailableException : Exception
    {
        public StockUnavailableException()
        {
        }

        public StockUnavailableException(string message) : base(message)
        {
        }

        public StockUnavailableException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
