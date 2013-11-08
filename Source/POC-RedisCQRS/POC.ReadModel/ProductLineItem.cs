using System;

namespace POC.ReadModel
{
    [Serializable]
    public class ProductLineItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public decimal? UnitPrice { get; set; }
        public short?  Stock { get; set; }
    }
}
