using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    class SqlProduct:IProduct
    {
        LessProEntities db = new LessProEntities();

        public Product GetProductById(int? id)
        {
            Product product = db.Product.Find(id);
            return product;
        }

        public IEnumerable<Product> GetProducts()
        {
            var product = db.Product.ToList();
            return product;
        }

    }
}
