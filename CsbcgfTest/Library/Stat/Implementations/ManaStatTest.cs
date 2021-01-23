using Csbcgf.Core;
using NUnit.Framework;

namespace Csbcgf.Coretest
{
    [TestFixture()]
    public class ManaStatTest
    {
        [Test()]
        public void TestValueAssignment()
        {
            ManaStat manaStat = new ManaStat(4, 10);
            Assert.AreEqual(4, manaStat.Value);
            Assert.AreEqual(10, manaStat.MaxValue);
            manaStat.Value = 7;
            Assert.AreEqual(7, manaStat.Value);
            manaStat.Value -= 10;
            Assert.AreEqual(0, manaStat.Value);
            manaStat.Value = 12;
            Assert.AreEqual(10, manaStat.Value);
            manaStat.MaxValue = 12;
            Assert.AreEqual(12, manaStat.MaxValue);
            manaStat.Value = 12;
            Assert.AreEqual(12, manaStat.Value);
            manaStat.MaxValue = ManaStat.GlobalMax + 10;
            Assert.AreEqual(ManaStat.GlobalMax, manaStat.MaxValue);
        }
    }
}
