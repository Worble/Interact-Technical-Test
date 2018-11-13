using System;
using InteractTechinicalTestInfrastructure.EntityFramework;
using InteractTechinicalTestInfrastructure.Repositories.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using InteractTechnicalTestDomain.Pocos;
using System.Linq;
using DeepEqual.Syntax;

namespace InteractTechnicalTestInfrastructureTests.RepositoryTests
{
    [TestClass]
    public class SqliteProductRepositoryTests
    {
        private List<Product> productList = new List<Product>
        {
            new Product { Id = 1, Name = "Red Shirt", Cost = 100 },
            new Product { Id = 2, Name = "Brown Shirt", Cost = 200 },
            new Product { Id = 3, Name = "Jeans", Cost = 300 }
        };

        private ProductContext SetupDb()
        {
            var options = new DbContextOptionsBuilder<ProductContext>()
                            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                            .Options;
            var context = new ProductContext(options);
            context.AddRange(productList);
            context.SaveChanges();
            return context;
        }

        [TestMethod]
        public void GetAllProducts_ReturnsAllProducts()
        {
            //Arrange
            var repo = new SqliteProductRepository(SetupDb());
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
            var repo = new SqliteProductRepository(SetupDb());
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
            var repo = new SqliteProductRepository(SetupDb());
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
            var repo = new SqliteProductRepository(SetupDb());

            //Act
            var actual = repo.GetProductsByName("NO PRODUCT").ToList();

            //Assert
            Assert.IsTrue(actual.Count == 0);
        }
    }
}