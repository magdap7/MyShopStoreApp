// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

/*
                                O PROGRAMIE
 
Program obsługuje magazyn do sklepu, gdzie produktami są  artykuły na wagę lub na sztuki. Wszystkie produkty posiadają nazwę, która jednoznacznie 
okresla cechy produktu, czyli np masę opakowania czy pojemność kartonu. Poza tym mają cenę jednostkową (na sztukę lub na wagę).
Co rozróżnia dwa typy produktów: te ważone mają dodatkowo atrybut w postaci wagi (kg), natomiast te na sztuki mają określoną: ilość sztuk
i masę w kilogramach  na sztukę lub pojemność w litrach. Te, które nie mają miary wagi (np jajka, papierosy) ani objętości mają jednostkę x.
Program umożliwia dodawanie do pliku nowych produktów, wyszukiwania po nazwie lub jej fragmencie. 
Można aktualizować cenę jednostkową (na szt lub kg) wybranego po pełnej nazwie produktu. Można też usuwać wybrane pozycje z listy i z pliku.
 
*/


using MyShopStoreApp;


PrintInstructions();
var collector = new Collector("ProductsWeighted.csv", "ProductsPacked.csv");

try
{
    collector.LoadProductsFromFileToTmpList();
}
catch (Exception ex)
{
    Console.WriteLine($"Exeption catched: {ex.Message}");
}

string line="";
do
{
    Console.Write(">");
    line = Console.ReadLine();
    if (line == "q" || line==null)
        break;
    try
    {
        bool cond1 = line.StartsWith("-dw ");
        bool cond2 = line.StartsWith("-dp ");
        bool cond3 = line.StartsWith("-z ");
        bool cond4 = line.StartsWith("-ac ");
        bool cond5 = line.StartsWith("-aiw ");//wagowo
        bool cond6 = line.StartsWith("-aip ");//paczki, pojemniki, kartony
        bool cond7 = line.StartsWith("-u ");

        if (cond1 || cond2 || cond3 || cond4 || cond5 || cond6 || cond7)
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
                        int[] foundIndex = collector.FindProductInList(line, "nonstrict");
                        string found = collector.ReturnFound(foundIndex);
                        Console.WriteLine(found);
                        break;
                    case "-ac" or "-aiw" or "-aip":
                        collector.UpdateProductInList(line);//cena jednostkowa albo ilość (sztuk lub wagowa)
                        break;
                    case "-u":
                        bool res = collector.DeleteProductFromList(line);
                        break;
                    default:
                        break;
                        //throw new Exception("Inwalid command line."); 
                }
            }
            else
            {
                throw new Exception("Name of product too short (less then 5 characters) or contains characters not allowed.");
            } 
        }
        else 
        {
            PrintInstructions();
            throw new Exception("Ivalid input start");
        }
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
    Console.WriteLine("-aiw nazwa waga  - modyfikacja wagi danego produktu");
    Console.WriteLine("-aip nazwa ilosc  - modyfikacja ilości sztuk danego produktu");
    Console.WriteLine("-u nazwa    - usunięcie z bazy danego produktu");

    Console.WriteLine("Uwagi pomocznicze:");
    Console.WriteLine("Ceny podawaj w postaci liczb całkowitych lub zmiennoprzecinkowych, np.: 29,99");
    Console.WriteLine("Masę lub objętość podawaj w postaci liczb całkowitych lub zmiennoprzecinkowych, np.: 3, czy 1,5, czy 0,25");
    Console.WriteLine("Nazwę produktu podawaj bez spacji, przecinków i bez polskich znaków.");
    Console.WriteLine("Nazwa produktu musi posiadac conajmniej 5 znaków.");
    Console.WriteLine("Jeśli produkt jest na wagę, nie muszisz pdawać jednostki, jest ona traktowana jak kg.");
    Console.WriteLine("Jeśli produkt jest na sztuki, jako jednostkę podawaj jedną z: kg, l.");
    Console.WriteLine("Jeśli produkt jest na sztuki, ale nie ma podanej wagi czy pojemności, wstaw x.");
    Console.WriteLine("Aby wyjść z programu, naciśnij q lub zostaw pustą linię.");

    //Console.WriteLine("syntax1: -n [unikatowa nazwa produktu] -c [cena jednostkowa w zł] -i [ilość sztuk] -m [ilość masowa] -p [jednostka kg lub l]");
    //Console.WriteLine("syntax2: [unikatowa nazwa produktu];[cena jednostkowa w zł];[ilość sztuk];[masa lub objętość];[jednostka w kg lub l]");
}


