using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopStoreApp
{
    public class Collector : ICollector
    {
        //string Filename1, Filename2;
        List<ProductWeighted> productWeighteds = new List<ProductWeighted>();
        List<ProductPacked> productPackeds = new List<ProductPacked>();
        InputValidator inputValidator = new InputValidator();

        FileManager fileManagerW, fileManagerP;
        public Collector(string filenameW, string filenameP)
        {
            fileManagerW = new FileManager(filenameW);
            fileManagerP = new FileManager(filenameP);
        }

        public void LoadProductsFromFileToTmpList()
        {
            //throw new NotImplementedException();
            List<string> linesW = fileManagerW.ReadLineListFromFile();
            List<string> linesP = fileManagerP.ReadLineListFromFile();

            if(linesW!= null) 
            {
                foreach (string line in linesW)
                {
                    string[] parts = line.Split(new char[] { ';' });
                    productWeighteds.Add(new ProductWeighted(parts[0], float.Parse(parts[1]), float.Parse(parts[2])));
                }
            }
            if (linesP!= null)
            {
                foreach (string line in linesP)
                {
                    string[] parts = line.Split(new char[] { ';' });
                    productPackeds.Add(new ProductPacked(parts[0], float.Parse(parts[1]), int.Parse(parts[2]), float.Parse(parts[3]), parts[4]));
                }
            }
            
        }
        public void SaveProductsFromTmpListToFile()
        {
            //throw new NotImplementedException();
            foreach (ProductWeighted item in productWeighteds)
            {
                string line = $"{item.Name};{item.UnitPrice};{item.Weight}";
                fileManagerW.AddLineToFile(line);
            }
            foreach (ProductPacked item in productPackeds)
            {
                string line = $"{item.Name};{item.UnitPrice};{item.Quantity};{item.MassOrCapacity},{item.Unit}";
                fileManagerW.AddLineToFile(line);
            }

        }



        public void AddProductToList(string productParams)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProductFromList(string productParams)
        {
            throw new NotImplementedException();
        }

        public int FindProductInList(string productParams)
        {
            throw new NotImplementedException();
        }


        public bool UpdateProductInList(string productParams)
        {
            throw new NotImplementedException();
        }
    }
}
