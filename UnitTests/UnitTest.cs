using IngenicoPOS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests {

    [TestClass]
    public class UnitTest {
        private const string PORT = "COM9";

        [TestMethod]
        public void Test01Connectivity() {
            POS pos = new POS(PORT);
            Assert.IsTrue(pos.Connect());
            pos.Disonnect();
        }

        [TestMethod]
        public void Test02Sale() {
            POS pos = new POS(PORT);
            pos.POSPrints = true;

            if (pos.Connect()) {
                SaleResult saleResult;
                saleResult = pos.Sale(12345);
                Assert.IsTrue(saleResult.Success);
            } else {
                Assert.Fail();
            }
            pos.Disonnect();
        }
    }
}