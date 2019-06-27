using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ChildPro.Models
{
    public class ProductDetailViewModel
    {
        public IEnumerable<Product_Norms> pro_no { get; set; }
        public IEnumerable<Product> tuijian { get; set; }
        public IEnumerable<Comment_Product> pro_com { get; set; }
        public IEnumerable<Adress> adresses { get; set; }
        public Product pro_detail {get;set;}
        public User users { get; set; }
    }
}