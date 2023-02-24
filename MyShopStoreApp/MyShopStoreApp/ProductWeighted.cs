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
                return Weight*this.UnitPrice;
            } 
        }

        public ProductWeighted(string name, float price, float weight) : base(name, price)
        {
            this.Weight = weight;
        }

    }
}
