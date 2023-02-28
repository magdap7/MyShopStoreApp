namespace MyShopStoreApp
{
    public interface ICollector
    {
        int TotalCount { get; }
        float TotalCost { get; }
        void LoadProductsFromFileToTmpList();
        void SaveProductsFromTmpListToFile();
        void AddProductToList(string productParams);
        int[] FindProductInList(string productParams, string type);
        bool UpdateProductInList(string productParams);
        bool DeleteProductFromList(string productParams);
    }
}
