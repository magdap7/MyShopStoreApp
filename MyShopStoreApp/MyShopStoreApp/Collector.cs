using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyShopStoreApp
{
    public class Collector : ICollector
    {
        //string Filename1, Filename2;
        List<ProductWeighted> productWeighteds = new List<ProductWeighted>();
        List<ProductPacked> productPackeds = new List<ProductPacked>();
        InputValidator inputValidator = new InputValidator();

        public delegate void ProductAddedDelegate(object sender, EventArgs args);
        public delegate void ProductDeletedDelegate(object sender, EventArgs args);
        public delegate void ProductUpdatedDelegate(object sender, EventArgs args);
        public event ProductAddedDelegate ProductAdded;
        public event ProductDeletedDelegate ProductDeleted;
        public event ProductUpdatedDelegate ProductUpdated;

        public int TotalCount
        {
            get
            {
                return productWeighteds.Count + productPackeds.Count;
            }
        }
        public float TotalCost 
        { get
            {
                float cost = 0;
                foreach(ProductWeighted item in productWeighteds)
                {
                    cost += item.TotalPrice;
                }
                foreach (ProductPacked item in productPackeds)
                {
                    cost += item.TotalPrice;
                }
                return cost;
            }
        }

        FileManager fileManagerW, fileManagerP;
        public Collector(string filenameW, string filenameP)
        {
            string filepath= "C:\\Users\\magda\\MyProjects\\MyShopStoreApp\\MyShopStoreApp\\MyShopStoreApp\\bin\\";
            string headerW = "Nazwa;Cena jedn.;Waga";
            string headerP = "Nazwa;Cena jedn.;Ile sztuk;Masa lub obj.;Jednostka [kg, l]";
            ProductAdded += PrintEventProductAdded;
            ProductDeleted += PrintEventProductDeleted;
            ProductUpdated += PrintEventProductUpdated;

            fileManagerW = new FileManager(filepath + filenameW, headerW);
            fileManagerP = new FileManager(filepath + filenameP, headerP);
        }

        public void LoadProductsFromFileToTmpList()
        {
            //throw new NotImplementedException();
            List<string> linesW = fileManagerW.ReadLineListFromFile();
            List<string> linesP = fileManagerP.ReadLineListFromFile();

            if (linesW != null)
            {
                foreach (string line in linesW)
                {
                    string[] parts = line.Split(new char[] { ';' });
                    productWeighteds.Add(new ProductWeighted(parts[0], float.Parse(parts[1]), float.Parse(parts[2])));
                }
            }
            else
            {
                throw new Exception("Empty file");
            }
            if (linesP!= null)
            {
                foreach (string line in linesP)
                {
                    string[] parts = line.Split(new char[] { ';' });
                    productPackeds.Add(new ProductPacked(parts[0], float.Parse(parts[1]), int.Parse(parts[2]), float.Parse(parts[3]), parts[4]));
                }
            }
            else
            {
                throw new Exception("Empty file");
            }
        }
        public void SaveProductsFromTmpListToFile()
        {
            //throw new NotImplementedException();
            //fileManagerW.ClearFile();
            //fileManagerP.ClearFile();

            if(productWeighteds.Count> 0)
                fileManagerW.ClearFile();
            if (productPackeds.Count > 0)
                fileManagerP.ClearFile();
            foreach (ProductWeighted item in productWeighteds)
            {
                string line = $"{item.Name};{item.UnitPrice};{item.Weight}";
                fileManagerW.AddLineToFile(line);
            }
            foreach (ProductPacked item in productPackeds)
            {
                string line = $"{item.Name};{item.UnitPrice};{item.Quantity};{item.MassOrCapacity};{item.Unit}";
                fileManagerP.AddLineToFile(line);
            }
        }

        public void AddProductToList(string productParams)
        {
            //throw new NotImplementedException();
            string[] result = inputValidator.IsValidForAddingProduct(productParams);
            if (result != null)
            {
                if (result.Length == 4)//produkty na wagę
                {
                    productWeighteds.Add(new ProductWeighted(result[1], float.Parse(result[2]), float.Parse(result[3])));
                    this.ProductAdded(this, new EventArgs());
                }
                if (result.Length == 6)//produkty na sztuki
                {
                    productPackeds.Add(new ProductPacked(result[1], float.Parse(result[2]), int.Parse(result[3]), float.Parse(result[4]), result[5]));
                    this.ProductAdded(this, new EventArgs());
                }
            }
        }
        public bool DeleteProductFromList(string productParams)
        {
            //throw new NotImplementedException();
            int[] foundIndex = FindProductInList(productParams, "strict");
            if (foundIndex[0] == 1 && foundIndex[1] >= 0)
            {//na wagę
                productWeighteds.RemoveAt(foundIndex[1]);
                this.ProductDeleted(this, new EventArgs());
                return true;
            }
            if (foundIndex[0] == 2 && foundIndex[1] >= 0)
            {//na sztuki
                productPackeds.RemoveAt(foundIndex[1]);
                this.ProductDeleted(this, new EventArgs());
                return true;
            } 
            return false;
        }
        public int[] FindProductInList(string productParams, string type)
        {
            //throw new NotImplementedException();
            var array = productParams.Split(' ');
            string name = array[1]; //nazwa

            if (type == "strict")
            {
                for (int index = 0; index < productWeighteds.Count; index++)
                {//szukamy pierwszego pasujacego
                    if (productWeighteds[index].Name == name)
                        return new int[2] { 1, index };
                }
                for (int index = 0; index < productPackeds.Count; index++)
                {
                    if (productPackeds[index].Name == name)
                        return new int[2] { 2, index };
                }
                return new int[2] { -1, -1 };
            }
            else //nonstrict
            {
                for (int index = 0; index < productWeighteds.Count; index++)
                {//szukamy pierwszego pasujacego
                    if (productWeighteds[index].Name.Contains(name))
                        return new int[2] { 1, index };
                }
                for (int index = 0; index < productPackeds.Count; index++)
                {
                    if (productPackeds[index].Name.Contains(name))
                        return new int[2] { 2, index };
                }
                return new int[2] { -1, -1 };
            }
        }
        public bool UpdateProductInList(string productParams)
        {
            //throw new NotImplementedException();
            string[] result = inputValidator.IsValidForUpdatingProduct(productParams);

            if (result != null)
            {
                int[] foundIndex = FindProductInList(productParams, "strict");
                if (foundIndex[0] == 1 && foundIndex[1] >= 0)
                {//na wagę
                    if (result[0] == "-ac")
                    {
                        productWeighteds[foundIndex[1]].UnitPrice = float.Parse(result[2]);
                        this.ProductUpdated(this, new EventArgs());
                    }   
                    else //if (result[0] == "-aiw")
                    {
                        productWeighteds[foundIndex[1]].Weight = float.Parse(result[2]);
                        this.ProductUpdated(this, new EventArgs());
                    }
                    return true;
                }
                if (foundIndex[0] == 2 && foundIndex[1] >= 0)
                {//na sztuki
                    if (result[0] == "-ac")
                    {
                        productPackeds[foundIndex[1]].UnitPrice = float.Parse(result[2]);
                        this.ProductUpdated(this, new EventArgs());
                    }    
                    else if (result[0] == "-aip")
                    {
                        productPackeds[foundIndex[1]].Quantity = int.Parse(result[2]);
                        this.ProductUpdated(this, new EventArgs());
                    }
                    else
                    {
                        throw new Exception("You can't change weight in packed products.");
                    }
                    return true;
                }
            }
            return false;
        }
        public string ReturnFound(int[] foundIndex)
        {

            if (foundIndex[0] == 1 && foundIndex[1] >= 0)
            {
                ProductWeighted itemW = productWeighteds[foundIndex[1]];
                return itemW.ToString();
            }
            else if (foundIndex[0] == 2 && foundIndex[1] >= 0)
            {
                ProductPacked itemP = productPackeds[foundIndex[1]];
                return itemP.ToString();
            }
            else
            {
                return "";
            }
        }
        void PrintEventProductAdded(object sender, EventArgs args)
        {
            Console.WriteLine($"Dodano nowy produkt do listy. //{args}");
        }
        void PrintEventProductDeleted(object sender, EventArgs args)
        {
            Console.WriteLine($"Usunięto wybrany produkt z listy. //{args}");
        }
        void PrintEventProductUpdated(object sender, EventArgs args)
        {
            Console.WriteLine($"Zaktualizowano wybrany produkt z listy. //{args}");
        }
    }
}
