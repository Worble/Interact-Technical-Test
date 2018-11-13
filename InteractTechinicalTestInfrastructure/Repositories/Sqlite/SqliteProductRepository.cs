using InteractTechinicalTestInfrastructure.EntityFramework;
using InteractTechnicalTestDomain.Pocos;
using InteractTechnicalTestDomain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractTechinicalTestInfrastructure.Repositories.Sqlite
{
    public class SqliteProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public SqliteProductRepository(ProductContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetProductsByName(string name)
        {
            var query = from p in _context.Products
                        where EF.Functions.Like(p.Name, $"%{name}%")
                        select p;
            return query.ToList();
        }
    }
}