﻿using System;
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

        FileManager fileManagerW, fileManagerP;
        public Collector(string filenameW, string filenameP)
        {
            string filepath= "C:\\Users\\magda\\MyProjects\\MyShopStoreApp\\MyShopStoreApp\\MyShopStoreApp\\bin\\";
            string headerW = "Nazwa;Cena jedn.;Waga";
            string headerP = "Nazwa;Cena jedn.;Ile sztuk;Masa lub obj.;Jednostka [kg, l]";
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
                }
                if (result.Length == 6)//produkty na sztuki
                {
                    productPackeds.Add(new ProductPacked(result[1], float.Parse(result[2]), int.Parse(result[3]), float.Parse(result[4]), result[5]));
                }
            }
        }
        public bool DeleteProductFromList(string productParams)
        {
            //throw new NotImplementedException();
            string result = inputValidator.IsValidForDeletingProduct(productParams);
            if (result != null)
            {
                int[] foundIndex = FindProductInList(result, "strict");
                if (foundIndex[0] == 1 && foundIndex[1] >= 0)
                {//na wagę
                    productWeighteds.RemoveAt(foundIndex[1]);
                    return true;
                }
                if (foundIndex[0] == 2 && foundIndex[1] >= 0)
                {//na sztuki
                    productPackeds.RemoveAt(foundIndex[1]);
                    return true;
                } 
            }
            return false;
        }
        public int[] FindProductInList(string productParams, string type)
        {
            //throw new NotImplementedException();
            string result = inputValidator.IsValidForFindingProduct(productParams);
            if (result != null)
            {
                for (int index = 0; index < productWeighteds.Count; index++)
                {//szukamy pierwszego
                    if(type=="strict")
                        if (productWeighteds[index].Name == result)
                            return new int[2] { 1, index };
                    if (type == "nonstrict")
                        if (productWeighteds[index].Name.Contains(result))
                            return new int[2] { 1, index };
                }
                for (int index = 0; index < productPackeds.Count; index++)
                {
                    if (type == "strict")
                        if (productPackeds[index].Name == result)
                            return new int[2] { 2, index };
                    if (type == "nonstrict")
                        if (productPackeds[index].Name.Contains(result))
                            return new int[2] { 2, index };
                }
            }
            return new int[2] { -1, -1 };
        }
        public bool UpdateProductInList(string productParams)
        {
            //throw new NotImplementedException();
            string[] result = inputValidator.IsValidForUpdatingProduct(productParams);
            if (result != null)
            {
                int[] foundIndex = FindProductInList(result[1],"strict");
                if (foundIndex[0] == 1 && foundIndex[1] >= 0)
                {//na wagę
                    if (result[0] == "-ac")
                        productWeighteds[foundIndex[1]].UnitPrice = float.Parse(result[2]);
                    if (result[0] == "-ai")
                        productWeighteds[foundIndex[1]].Weight = float.Parse(result[2]);
                    return true;
                }
                if (foundIndex[0] == 2 && foundIndex[1] >= 0)
                {//na sztuki
                    if (result[0] == "-ac")
                        productPackeds[foundIndex[1]].UnitPrice = float.Parse(result[2]);
                    if (result[0] == "-ai")
                        productPackeds[foundIndex[1]].Quantity = int.Parse(result[2]);
                    return true;
                }
            }
            return false;
        }
        public string ReturnFound(int[] foundIndex)
        {
            ProductWeighted itemW = null;
            ProductPacked itemP = null;

            if (foundIndex[0] == 1 && foundIndex[1] >= 0)
            {
                itemW = productWeighteds[foundIndex[1]];
                //return $"{itemW.Name}\t{itemW.UnitPrice}\t{itemW.Weight}\t";
                return itemW.ToString();
            }
            else if (foundIndex[0] == 2 && foundIndex[1] >= 0)
            {
                itemP = productPackeds[foundIndex[1]];
                //return $"{itemP.Name}\t{itemP.UnitPrice}\t{itemP.Quantity}\t{itemP.MassOrCapacity}\t{itemP.Unit}\t";
                return itemP.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
