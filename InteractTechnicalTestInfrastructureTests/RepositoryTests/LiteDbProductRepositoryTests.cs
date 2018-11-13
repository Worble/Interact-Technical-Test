using DeepEqual.Syntax;
using InteractTechinicalTestInfrastructure.Repositories.Litedb;
using InteractTechnicalTestDomain.Pocos;
using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractTechnicalTestInfrastructureTests.RepositoryTests
{
    [TestClass]
    public class LiteDbProductRepositoryTests
    {
        private readonly string TestDatabaseString = System.IO.Path.GetTempFileName();

        private List<Product> productList = new List<Product>
        {
            new Product { Id = 1, Name = "Red Shirt", Cost = 100 },
            new Product { Id = 2, Name = "Brown Shirt", Cost = 200 },
            new Product { Id = 3, Name = "Jeans", Cost = 300 }
        };

        [TestInitialize]
        public void Setup()
        {
            using (var db = new LiteDatabase(TestDatabaseString))
            {
                db.DropCollection("products");
                var products = db.GetCollection<Product>("products");
                products.InsertBulk(productList);
                products.EnsureIndex(x => x.Name, "LOWER($.Name)", false);
            }
        }

        [TestMethod]
        public void GetAllProducts_ReturnsAllProducts()
        {
            //Arrange
            var repo = new LiteDbProductRepository(TestDatabaseString);
            var expected = productList.OrderBy(e => e.Id).ToList();

            //Act
            var actual = repo.GetAllProducts().OrderBy(e => e.Id).ToList();

            //Assert
            Assert.IsTrue(productList.IsDeepEqual(actual));
        }

        [TestMethod]
        public void GetProductsByName_ReturnsOneMatchingProduct_WhenOneProductWithTheGivenNameExists()
        {
            //Arrange
            var repo = new LiteDbProductRepository(TestDatabaseString);
            var expected = productList.First(e => e.Name == "Red Shirt");

            //Act
            var actual = repo.GetProductsByName("Red Shirt").ToList();

            //Assert
            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(expected.IsDeepEqual(actual.First()));
        }

        [TestMethod]
        public void GetProductsByName_ReturnsTwoMatchingProducts_WhenOneProductWithTheGivenNameExists()
        {
            //Arrange
            var repo = new LiteDbProductRepository(TestDatabaseString);
            var expected = productList.Where(e => e.Name.Contains("Shirt")).OrderBy(e => e.Id).ToList();

            //Act
            var actual = repo.GetProductsByName("Shirt").OrderBy(e => e.Id).ToList();

            //Assert
            Assert.IsTrue(actual.Count == 2);
            Assert.IsTrue(expected.IsDeepEqual(actual));
        }

        [TestMethod]
        public void GetProductsByName_ReturnsNoMatchingProducts_WhenNoProductWithTheGivenNameExists()
        {
            //Arrange
            var repo = new LiteDbProductRepository(TestDatabaseString);

            //Act
            var actual = repo.GetProductsByName("NO PRODUCT").ToList();

            //Assert
            Assert.IsTrue(actual.Count == 0);
        }
    }
}