using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopStoreApp
{
    public abstract class Product 
    {
        public  string Name { get; private set; }
        public  float UnitPrice { get;  set; }

        public Product(string name, float price)
        {
            this.Name = name;
            this.UnitPrice = price;
        }
        public virtual string WriteMe()
        {
            string objDescription = "";
            objDescription = objDescription + $"Nazwa: \t {this.Name}\n";
            objDescription = objDescription + $"Cena jednostkowa: \t {this.UnitPrice}\n";
            return objDescription;
        }
    }
}
