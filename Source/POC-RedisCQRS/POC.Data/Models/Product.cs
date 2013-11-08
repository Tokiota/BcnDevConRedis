using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POC.Data.Models
{
    [DataContract]
    public class Product
    {
        public Product()
        {
            this.Order_Details = new List<Order_Detail>();
        }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public Nullable<int> SupplierID { get; set; }
        [DataMember]
        public Nullable<int> CategoryID { get; set; }
        [DataMember]
        public string QuantityPerUnit { get; set; }
        [DataMember]
        public Nullable<decimal> UnitPrice { get; set; }
        [DataMember]
        public Nullable<short> UnitsInStock { get; set; }
        [DataMember]
        public Nullable<short> UnitsOnOrder { get; set; }
        [DataMember]
        public Nullable<short> ReorderLevel { get; set; }
        [DataMember]
        public bool Discontinued { get; set; }
        [DataMember]
        public virtual Category Category { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
