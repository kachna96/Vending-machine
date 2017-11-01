using System;

namespace PV178.Homeworks.HW02.Model
{
    /// <summary>
    /// Class for handling a product
    /// </summary>
    public class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="name">Name of a product</param>
        /// <param name="price">Price of a product</param>
        /// <exception cref="ArgumentException">If name is null/empty/whitespace(s)</exception>
        /// <exception cref="ArgumentOutOfRangeException">If price is less than zero</exception>
        public Product(string name, decimal price)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name of a product is null or white space.");
            }
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("Price for a product {0} is less than zaro.", name);
            }
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return String.Format("{0} for {1},- CZK", Name, Price);
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
            Product p = obj as Product;
            return p.Name == Name && p.Price == Price;
        }

        /// <summary>
        /// Get hash code based on Name and Price
        /// </summary>
        /// <returns>Hashcode</returns>
        public override int GetHashCode()
        {
            return String.Format("{0} + {1}", Name, Price).GetHashCode();
        }
    }
}
