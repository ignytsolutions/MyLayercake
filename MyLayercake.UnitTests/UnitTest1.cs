using MyLayercake.Core;
using MyLayercake.DataProvider;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.UnitTests {
    public class TestObject : DBEntity { 
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
        public double? Salary { get; set; }
    }

    public class Tests {
        private const string _providerName = "System.Data.SqlClient";
        private const string _connectionString = "Data Source=DESKTOP-ER9017R;Initial Catalog=MyLayercake;Integrated Security=true;";
        private const string _database = "MyLayercake";

        [Test]
        public void TestInsert() {
            using DataProvider<TestObject> provider = new DBDataProvider<TestObject>(new DatabaseSettings(_database, _connectionString, _providerName, true));

            provider.InsertOne(new TestObject() { Oid = Guid.NewGuid(), Created = DateTime.Now, Name = "Leanda", Surname = "Alexander" });
        }

        [Test]
        public void TestUpdate() {
            using DataProvider<TestObject> provider = new DBDataProvider<TestObject>(new DatabaseSettings(_database, _connectionString, _providerName, true));

            provider.UpdateOne(new TestObject() { Oid = Guid.Parse("484CEC1B-8516-455C-A117-0F97E54F6266"), Created = DateTime.Now, Name = "Gavin", Surname = "Smellexander", Age = 40, Salary = 45.85 });
        }

        [Test]
        public void TestDelete() {
            using DataProvider<TestObject> provider = new DBDataProvider<TestObject>(new DatabaseSettings(_database, _connectionString, _providerName, true));

            provider.DeleteById(new TestObject() { Oid = Guid.Parse("484CEC1B-8516-455C-A117-0F97E54F6266"), Created = DateTime.Now, Name = "Gavin", Surname = "Smellexander", Age = 40, Salary = 45.85 });
        }

        [Test]
        public void TestSelect() {
            using DataProvider<TestObject> provider = new DBDataProvider<TestObject>(new DatabaseSettings(_database, _connectionString, _providerName, true));
            
            provider.SelectAll();
        }

        [Test]
        public void TestSelectByID() {
            using DataProvider<TestObject> provider = new DBDataProvider<TestObject>(new DatabaseSettings(_database, _connectionString, _providerName, true));

            provider.FindById(Guid.Parse("CCD4390B-9A31-4441-BA7E-12F695EAB772"));
        }
    }
}