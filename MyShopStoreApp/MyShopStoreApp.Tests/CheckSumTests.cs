namespace MyShopStoreApp.Tests
{
    public class CheckSumTests
    {

        [Test]
        public void TestCheckSum()
        {
            Collector testingCollector = new Collector("ProductsWeighted.csv", "ProductsPacked.csv");
            testingCollector.LoadProductsFromFileToTmpList();
            int startTotalCount = testingCollector.TotalCount;
            float startTotalCost = testingCollector.TotalCost;
            int nowTotalCount;
            float nowTotalCost;

            //adding products
            testingCollector.AddProductToList("-dw produktW 3,99 2,5");//na wagê
            testingCollector.AddProductToList("-dp produktP 5,99 3 0,5 l");//na sztuki
            float addedCost=3.99f*2.5f+5.99f*3;
            nowTotalCount = testingCollector.TotalCount;
            nowTotalCost = testingCollector.TotalCost;
            Assert.AreEqual(startTotalCount+2, nowTotalCount);
            Assert.AreEqual(startTotalCost+addedCost, nowTotalCost);

            //deleting
            testingCollector.DeleteProductFromList("-u produktW");
            testingCollector.DeleteProductFromList("-u produktP");
            nowTotalCount = testingCollector.TotalCount;
            nowTotalCost = testingCollector.TotalCost;
            Assert.AreEqual(startTotalCount, nowTotalCount);
            Assert.AreEqual(startTotalCost, nowTotalCost);
        }

        [Test]
        public void TestUpdate()
        {
            Collector testingCollector = new Collector("ProductsWeighted.csv", "ProductsPacked.csv");
            testingCollector.LoadProductsFromFileToTmpList();
            float nowTotalCost;
            testingCollector.AddProductToList("-dw produktW 3,99 2,5");//na wagê
            testingCollector.AddProductToList("-dp produktP 5,99 3 0,5 l");//na sztuki
            //updating
            float startTotalCost = testingCollector.TotalCost;
            testingCollector.UpdateProductInList("-ac produktW 4,99");
            testingCollector.UpdateProductInList("-aiw produktW 1,5");
            testingCollector.UpdateProductInList("-ac produktP 6,99");
            testingCollector.UpdateProductInList("-aip produktP 2");
            float changedCost = 4.99f*1.5f + 6.99f * 2 - (3.99f*2.5f + 5.99f * 3);
            nowTotalCost = testingCollector.TotalCost;
            Assert.AreEqual(startTotalCost + changedCost, nowTotalCost);
        }
    }
}