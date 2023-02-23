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
        public  float UnitPrice { get; private set; }


        public Product(string name, float price)
        {
            this.Name = name;
            this.UnitPrice = price;
        }

    }
}
