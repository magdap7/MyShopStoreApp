// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using MyShopStoreApp;

PrintInstructions();
var file = new FileManager("Products.csv");
string line;
do
{
    Console.WriteLine($"Wpisz kolejny produkt w postaci danych rozdzielanych średnikami.");
    line = Console.ReadLine();
    if (line == "q")
        break;
    try
    {
        var result = TestInput(line);
        if (result == ";;;;")
            file.AddLineToFile(line);
        else if (result == "-n ")
            Console.WriteLine(line);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exeption catched: {ex.Message}");
    }
}
while (line != "q");

List<string> list = file.ReadLineListFromFile();
foreach (string item in list)
{
    Console.WriteLine(item);
    ////Console.WriteLine(item.Contains("sok"));
}


void PrintInstructions()
{
    Console.WriteLine("Dodaj produkt do magazynu lub modyfikuj cenę jednostkową i/lub ilość wybranego produktu.");
    Console.WriteLine("syntax1: -n [unikatowa nazwa produktu] -c [cena jednostkowa w zł] -i [ilość sztuk] -m [ilość masowa] -p [jednostka kg lub l]");
    Console.WriteLine("syntax2: [unikatowa nazwa produktu];[cena jednostkowa w zł];[ilość sztuk];[masa lub objętość];[jednostka w kg lub l]");
    Console.WriteLine("Ceny podawaj w postaci liczb całkowitych lub zmiennoprzecinkowych, np.: 29,99");
    Console.WriteLine("Masę lub objętość podawaj w postaci liczb całkowitych lub zmiennoprzecinkowych, np.: 3, czy 1,5");
    Console.WriteLine("Nazwę produktu podawaj bez spacji i bez polskich znaków.");
    Console.WriteLine("Jeśli produkt jest na wagę, jako ilość podawaj zawsze 1.");
    Console.WriteLine("Aby wyjść z programu, naciśnij q.");
}
string TestInput(string input)
{
    string result;
    var arr1 = input.Split(";");
    var arr2 = input.Split(" ");
    if (arr1.Length == 5)
    {
        result = ";;;;";
    }
    else if (input.Contains("-n ") && arr2.Length == 4)
    {//-n nazwa -c cena, -n nazwa -i ilosc
        result = "-n ";
    }
    else
    {
        result = "";
    }
    return result;
}