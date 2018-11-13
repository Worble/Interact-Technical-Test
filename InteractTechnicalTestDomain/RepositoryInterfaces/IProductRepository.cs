using InteractTechnicalTestDomain.Pocos;
using System.Collections.Generic;

namespace InteractTechnicalTestDomain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetProductsByName(string name);
    }
}