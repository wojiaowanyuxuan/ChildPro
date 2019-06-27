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
    public class CartManager
    {
        ICart icart = DataAccess.GetCart_Pro();
        public IEnumerable<Cart> GetCarts()
        {
            var cart = icart.GetCarts();
            return cart;
        }
        public Cart GetCartById(int? id)
        {
            Cart carts = icart.GetCartById(id);
            return carts;
        }
        public void RemoveProduct(Cart carts)
        {
            icart.RemoveProduct(carts);
        }
    }
}
