using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace MyShopStoreApp
{
    public class FileManager
    {
        const string header = "Nazwa;Cena jedn.;Ile sztuk;Masa lub obj.;Jednostka [kg, l]";
        string fileName;
        public FileManager(string name)
        {
            this.fileName = name;
        }

        public void AddLineToFile(List<string> list)
        {
            if (!File.Exists(fileName))
            {//dodaj wiersz nazw kolumn
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(header);
                }
            }
            using (var writer = File.AppendText(fileName))
            {
                string combinedString = string.Join(";", list.ToArray());
                writer.WriteLine(combinedString);
            }
        }
        public void AddLineToFile(string combinedString)
        {
            if (!File.Exists(fileName))
            {//dodaj wiersz nazw kolumn
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(header);
                }
            }
            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine(combinedString);
            }
        }
        public List<string> ReadLineListFromFile()
        {
            List<string> list = new List<string>();
            if (!File.Exists(fileName))
            {//rzuć wyjątkiem
                throw new Exception($"File {fileName} doesn't exist.");
            }
            else
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        list.Add(line);
                        line = reader.ReadLine();
                    }
                }
            }
            return list;
        }
        public void ClearFile()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }



    }
}
