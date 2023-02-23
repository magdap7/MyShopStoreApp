// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using MyShopStoreApp;


PrintInstructions();
var collector = new Collector("ProductsWeighted.csv", "ProductsPackedd");
try
{
    collector.LoadProductsFromFileToTmpList();
}
catch (Exception ex)
{
    Console.WriteLine($"Exeption catched: {ex.Message}");
}

string line;
do
{
    line = Console.ReadLine();
    if (line == "q" || line.Length==0)
        break;
    try
    {
        bool cond1 = line.StartsWith("-dw ");
        bool cond2 = line.StartsWith("-dp ");
        bool cond3 = line.StartsWith("-z ");
        bool cond4 = line.StartsWith("-ac ");
        bool cond5 = line.StartsWith("-ai ");
        bool cond6 = line.StartsWith("-u ");

        if (cond1 || cond2 || cond3 || cond4 || cond5 || cond6)
        {
            var array = line.Split(' '); 
            if (array[1].Length>4 && !array[1].Contains(","))
            {
                switch (array[0])
                {
                    case "-dw" or "-dp":
                        collector.AddProductToList(line);//na wagę albo na paczki lub kartony
                        break;
                    case "-z":
                        collector.FindProductInList(line);
                        break;
                    case "-ac" or "-ai":
                        collector.UpdateProductInList(line);//cena jednostkowa labo ilość (sztuk lub wagowa)
                        break;
                    case "-u":
                        collector.DeleteProductFromList(line);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                throw new Exception("First param too short or contains characters not allowed.");
            } 
        }
        else 
        {
            throw new Exception("Ivalid input start");
        }

        Console.WriteLine(line);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exeption catched: {ex.Message}");
    }
}
while (line != "q" || line.Length == 0);

try 
{
    collector.SaveProductsFromTmpListToFile();
}
catch (Exception ex)
{
    Console.WriteLine($"Exeption catched: {ex.Message}");
}



//////////////////////Metody//////////////////////

void PrintInstructions()
{
    Console.WriteLine("PROGRAM DO OBSŁUGI MAGAZYNU DLA SKLEPU XYZ");
    Console.WriteLine("Wybierz jedną z opcji:");
    Console.WriteLine("-dw nazwa cena waga  - dodanie produktu na wagę");
    Console.WriteLine("-dp nazwa cena ilosc_sztuk waga/pojemnosc/brak jednostka (kg lub l lub x)    - dodanie produktu na sztuki");
    Console.WriteLine("-z fragnent_tekstu   - znalezienie produktu po słowie");
    Console.WriteLine("-ac nazwa cena   - modyfikacja ceny jednostkowej danego produktu");
    Console.WriteLine("-ai nazwa ilosc  - modyfikacja wagi lub ilości sztuk danego produktu");
    Console.WriteLine("-u nazwa    - usunięcie z bazy danego produktu");

    Console.WriteLine("Uwagi pomocznicze:");
    Console.WriteLine("Ceny podawaj w postaci liczb całkowitych lub zmiennoprzecinkowych, np.: 29,99");
    Console.WriteLine("Masę lub objętość podawaj w postaci liczb całkowitych lub zmiennoprzecinkowych, np.: 3, czy 1,5, czy 0,25");
    Console.WriteLine("Nazwę produktu podawaj bez spacji, przecinków i bez polskich znaków.");
    Console.WriteLine("Jeśli produkt jest na wagę, nie muszisz pdawać jednostki, jest ona traktowana jak kg.");
    Console.WriteLine("Jeśli produkt jest na sztuki, jako jednostkę podawaj jedną z: kg, l.");
    Console.WriteLine("Jeśli produkt jest na sztuki, ale nie ma podanej wagi czy pojemności, wstaw x.");
    Console.WriteLine("Aby wyjść z programu, naciśnij q lub zostaw pustą linię.");

    //Console.WriteLine("syntax1: -n [unikatowa nazwa produktu] -c [cena jednostkowa w zł] -i [ilość sztuk] -m [ilość masowa] -p [jednostka kg lub l]");
    //Console.WriteLine("syntax2: [unikatowa nazwa produktu];[cena jednostkowa w zł];[ilość sztuk];[masa lub objętość];[jednostka w kg lub l]");
}
