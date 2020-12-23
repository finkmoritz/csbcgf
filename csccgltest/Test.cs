using csccgl;
using NUnit.Framework;
namespace csccgltest
{
    [TestFixture()]
    public class Test
    {
        [Test()]
        public void TestCase()
        {
            MyClass myClass = new MyClass();
            Assert.AreEqual("1", "1");
        }
    }
}
