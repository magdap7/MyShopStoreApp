using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopStoreApp
{
    public class ProductWeighted : Product
    {
        public float Weight { get; set; }
        public float TotalPrice
        {
            get
            {
                return Weight * this.UnitPrice;
            }
        }
        public ProductWeighted(string name, float price, float weight) : base(name, price)
        {
            this.Weight = weight;
        }
        public override string ToString()
        {
            string objDescription = "";
            objDescription = objDescription + base.WriteMe();
            objDescription = objDescription + $"Waga: \t {this.Weight}\n";
            objDescription = objDescription + $"Cena całkowita: \t {this.TotalPrice}\n";

            return objDescription;
        }
    }
}
