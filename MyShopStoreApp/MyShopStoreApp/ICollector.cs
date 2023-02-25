using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyShopStoreApp.Product;

namespace MyShopStoreApp
{
    public interface ICollector
    {
        
        void LoadProductsFromFileToTmpList();
        void SaveProductsFromTmpListToFile();
        void AddProductToList(string productParams);
        int[] FindProductInList(string productParams, string type);
        bool UpdateProductInList(string productParams);
        bool DeleteProductFromList(string productParams);

        //event ProductAddedDelegate ProducAdded;
    }
}
