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
        public int Cart_ID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Time { get; set; }
        public int Product_ID { get; set; }
        public int Pro_Num { get; set; }
        public int Pro_Norm_id { get; set; }
        public Nullable<int> Flag { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Product_Norms Product_Norms { get; set; }
        public virtual User User { get; set; }
    }
}
