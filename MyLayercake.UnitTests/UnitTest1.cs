using NUnit.Framework;
using MyLayercake.BusinessObjects.Logic;
using MyLayercake.BusinessObjects;

namespace MyLayercake.UnitTests {
    public class TestObject : IEntity { }
    public class Tests {
        [SetUp]
        public void Setup() {
            Repository<TestObject> b = new Repository<TestObject>();
            b.Dispose();
        }

        private void B_Commited(object sender, System.EventArgs e) {
            throw new System.NotImplementedException();
        }

        [Test]
        public void Test1() {
            Assert.Pass();
        }
    }
}