using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Models;

namespace DAL
{
    public class SqlCart:ICart
    {
        LessProEntities db = new LessProEntities();
        public IEnumerable<Cart> GetCarts()
        {
            var cart = db.Cart.ToList();
            return cart;
        }
        public Cart GetCartById(int? id)
        {
            Cart carts = db.Cart.Find(id);
            return carts;
        }
        public void RemoveProduct(Cart carts)
        {
            db.Cart.Remove(carts);
            db.SaveChanges();
        }
    }
}
