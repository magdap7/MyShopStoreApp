using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopStoreApp
{
    public class ProductPacked : Product
    {
        public int Quantity { get; private set; }
        public float MassOrCapacity { get; private set; }
        public string Unit { get; private set; }
        public ProductPacked(string name, float price, int quantity, float mass_cap, string unit) : base(name, price)
        {
            Quantity = quantity;
            MassOrCapacity = mass_cap;
            Unit = unit;
        }
    }
}
