//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cart()
        {
            this.Order = new HashSet<Order>();
        }
    
        public int Cart_ID { get; set; }
        public int UserID { get; set; }
        public int Product_ID { get; set; }
        public int Num { get; set; }
        public System.DateTime Time { get; set; }
        public int Product_Class_ID { get; set; }
        public int Order_Product_Detail_ID { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Product_Norms Product_Norms { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }
    }
}
