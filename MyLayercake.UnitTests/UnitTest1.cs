using MyLayercake.Core;
using MyLayercake.DataProvider;
using NUnit.Framework;
using System;

namespace MyLayercake.UnitTests {
    public class TestObject : IEntity<Guid> { 
        public Guid Oid { get; set; }
        public DateTime Created { get; set; }
    }

    public class Tests {
        [SetUp]
        public void Setup() {
            DataProvider<TestObject> b = new DBDataProvider<TestObject>(new DatabaseSettings(string.Empty, string.Empty, string.Empty));
   
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