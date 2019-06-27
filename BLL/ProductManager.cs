using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DALFactory;
using IDAL;

namespace BLL
{
    public class ProductManager
    {
        IProduct iproduct = DataAccess.SearchProduct();
        public IEnumerable<Product> GetProduct()
        {
            var product = iproduct.GetProducts();
            return product;
        }
        public Product GetProductById(int? id)
        {
            Product products = iproduct.GetProductById(id);
            return products;
        }

    }
}
