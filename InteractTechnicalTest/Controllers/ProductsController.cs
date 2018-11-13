using InteractTechnicalTestDomain.Pocos;
using InteractTechnicalTestDomain.RepositoryInterfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace InteractTechnicalTest.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public JsonResult<IEnumerable<Product>> Get(string name = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(_productRepository.GetAllProducts());
            }
            return Json(_productRepository.GetProductsByName(name));
        }
    }
}