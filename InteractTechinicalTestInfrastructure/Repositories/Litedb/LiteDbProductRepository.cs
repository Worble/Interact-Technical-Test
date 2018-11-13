using InteractTechnicalTestDomain.Pocos;
using InteractTechnicalTestDomain.RepositoryInterfaces;
using LiteDB;
using System.Collections.Generic;

namespace InteractTechinicalTestInfrastructure.Repositories.Litedb
{
    public class LiteDbProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public LiteDbProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                return db.GetCollection<Product>("products").FindAll();
            }
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            using (var db = new LiteDatabase(_connectionString))
            {
                return db.GetCollection<Product>("products").Find(Query.Contains("Name", name.ToLower()));
            }
        }
    }
}