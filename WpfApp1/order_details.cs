//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class order_details
    {
        public long order_id { get; set; }
        public long product_id { get; set; }
        public Nullable<decimal> unit_price { get; set; }
        public Nullable<long> quantity { get; set; }
        public Nullable<decimal> total_price { get; set; }
    
        public virtual orders orders { get; set; }
        public virtual products products { get; set; }
    }
}
