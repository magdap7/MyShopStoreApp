using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopStoreApp
{
    public class ProductPacked : Product
    {
        public int Quantity { get;  set; }
        public float MassOrCapacity { get;  private set; }
        public string Unit { get; private set; }
        public float TotalPrice
        {
            get
            {
                return Quantity * this.UnitPrice;
            }
        }
        public ProductPacked(string name, float price, int quantity, float mass_cap, string unit) : base(name, price)
        {
            Quantity = quantity;
            MassOrCapacity = mass_cap;
            Unit = unit;
        }
        public override string ToString()
        {
            string objDescription = "";
            objDescription = objDescription + base.WriteMe();
            objDescription = objDescription + $"Ilość sztuk: \t {this.Quantity}\n";
            objDescription = objDescription + $"Masa lub pojemność: \t {this.MassOrCapacity}\n";
            objDescription = objDescription + $"Jednostka: \t {this.Unit}\n";
            objDescription = objDescription + $"Cena całkowita: \t {this.TotalPrice}\n";

            return objDescription;
        }
    }
}
