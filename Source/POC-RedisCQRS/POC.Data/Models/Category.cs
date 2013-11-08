using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POC.Data.Models
{
    [DataContract]
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [DataMember]
        public int CategoryID { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [IgnoreDataMember]
        public byte[] Picture { get; set; }
        [IgnoreDataMember]
        public virtual ICollection<Product> Products { get; set; }
    }
}
