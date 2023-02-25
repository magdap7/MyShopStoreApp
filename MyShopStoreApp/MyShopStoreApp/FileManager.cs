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
        string FileName;
        string Header;

        public FileManager(string filename, string header)
        {
            this.FileName = filename;
            this.Header = header;
        }
   

        public void AddLineToFile(List<string> list)
        {
            if (!File.Exists(FileName))
            {//dodaj wiersz nazw kolumn
                using (var writer = File.AppendText(FileName))
                {
                    writer.WriteLine(Header);
                }
            }
            using (var writer = File.AppendText(FileName))
            {
                string combinedString = string.Join(";", list.ToArray());
                writer.WriteLine(combinedString);
            }
        }
        public void AddLineToFile(string combinedString)
        {
            if (!File.Exists(FileName))
            {//dodaj wiersz nazw kolumn
                using (var writer = File.AppendText(FileName))
                {
                    writer.WriteLine(Header);
                }
            }
            using (var writer = File.AppendText(FileName))
            {
                writer.WriteLine(combinedString);
            }
        }
        public List<string> ReadLineListFromFile()
        {
            List<string> list = new List<string>();
            if (!File.Exists(FileName))
            {//rzuć wyjątkiem
                throw new Exception($"File {FileName} doesn't exist.");
            }
            else
            {
                using (var reader = File.OpenText(FileName))
                {
                    reader.ReadLine();//wiersz kolumn
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
            if (File.Exists(FileName))
            {
                //File.Delete(FileName);
                using (StreamWriter writer = new StreamWriter(FileName))
                {
                    writer.WriteLine(Header);
                }
            }
        }
    }
}
