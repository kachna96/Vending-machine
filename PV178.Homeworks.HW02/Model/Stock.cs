using PV178.Homeworks.HW02.Exceptions;
using System;
using System.Diagnostics;

namespace PV178.Homeworks.HW02.Model
{
    /// <summary>
    /// Class for managing a whole bunch of products
    /// </summary>
    public class Stock
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }

        /// <summary>
        /// Create a new Stock
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="quantity">Quantity. If it's less than zero, set it manually to zero (duh!)</param>
        /// <exception cref="ArgumentNullException">If product is null</exception>
        public Stock(Product product, int quantity)
        {
            if (Object.ReferenceEquals(product, null))
            {
                throw new ArgumentNullException("Product you are trying to set is null.");
            }
            if (quantity < 0)
            {
                Debug.WriteLine("Quantity of a product {0} is {1}, setting quantity to zero.", product.Name, quantity);
                quantity = 0;
            }
            Product = product;
            Quantity = quantity;
        }

        /// <summary>
        /// Dispatch one Product
        /// <exception cref="StockUnavailableException">If quantity of this product is zero or less than zero</exception>
        /// </summary>
        public void DispatchStock()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }
            else
            {
                throw new StockUnavailableException(String.Format("No units of {0} available.", Product.Name));
            }
        }

        /// <summary>
        /// Determine if two objects of this instance are equal based on Product equality and quantity
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>True if they are equal, false otherwise</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Stock s = obj as Stock;
            return s.Product.Equals(Product) && s.Quantity == Quantity;
        }

        /// <summary>
        /// Get hash code based on product and quantity
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return String.Format("{0} + {1}", this.ToString(), Quantity).GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0} (available: {1}x)", Product.ToString(), Quantity);
        }
    }
}
