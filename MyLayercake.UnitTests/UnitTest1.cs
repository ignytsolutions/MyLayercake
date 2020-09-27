using MyLayercake.Core;
using MyLayercake.DataProvider;
using MyLayercake.DataProvider.Factory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyLayercake.UnitTests {
    public class TestObject : DBEntity { 
        [Required]
        public string Name { get; set; }
        [Required]
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
            TestObject testObject = new TestObject() { Name = "John", Surname = "Smith" };

            using IDBContext context = new DBContext(new DatabaseSettings(_database, _connectionString, _providerName, true));
            context.Instance.InsertOne<TestObject>(testObject);
        }

        [Test]
        public void TestUpdate() {
            using IDBContext context = new DBContext(new DatabaseSettings(_database, _connectionString, _providerName, true));
            context.Instance.UpdateOne(new TestObject() { Oid = Guid.Parse("484CEC1B-8516-455C-A117-0F97E54F6266"), Created = DateTime.Now, Name = "Gavin", Surname = "Smellexander", Age = 40, Salary = 45.85 });
        }

        [Test]
        public void TestDelete() {
            using IDBContext context = new DBContext(new DatabaseSettings(_database, _connectionString, _providerName, true));
            context.Instance.DeleteById<TestObject>(new TestObject() { Oid = Guid.Parse("484CEC1B-8516-455C-A117-0F97E54F6266"), Created = DateTime.Now, Name = "Gavin", Surname = "Smellexander", Age = 40, Salary = 45.85 });
        }

        [Test]
        public IEnumerable<TestObject> TestSelect() {
            using IDBContext context = new DBContext(new DatabaseSettings(_database, _connectionString, _providerName, true));
            return context.Instance.SelectAll<TestObject>();
        }

        [Test]
        public TestObject TestSelectByID() {
            using IDBContext context = new DBContext(new DatabaseSettings(_database, _connectionString, _providerName, true));
            return context.Instance.FindById<TestObject>("CCD4390B-9A31-4441-BA7E-12F695EAB772");
        }
    }
}