using System;
using System.Collections.Generic;

namespace POC.Data.Models
{
    public class Order_Subtotal
    {
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}
