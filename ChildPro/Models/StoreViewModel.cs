using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ChildPro.Models
{
    public class StoreViewModel
    {
        public IEnumerable<Product> better_products1 { get; set; }
        public IEnumerable<Product> better_products2 { get; set; }
        public IEnumerable<Product> better_products3 { get; set; }
        public IEnumerable<Product> products_center{ get; set; }
        public IEnumerable<Product> products_center_wenju { get; set; }
        public IEnumerable<Product> products_center_shuji { get; set; }
        public IEnumerable<Product> products_center_wanju { get; set; }
        public Order_Detail Order_Details { get; set; }
    }
}