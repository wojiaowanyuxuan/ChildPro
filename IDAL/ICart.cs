using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IDAL
{
    public interface ICart
    {
        IEnumerable<Cart> GetCarts();
        Cart GetCartById(int? id);

        void RemoveProduct(Cart cart);
    }
}
