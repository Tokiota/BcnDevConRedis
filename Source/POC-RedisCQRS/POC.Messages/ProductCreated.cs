using System;
using System.Runtime.Serialization;

using POC.Data.Models;

namespace POC.Messages
{
    [Serializable]
    public class ProductCreated:IEventMessage
    {
        public ProductCreated(int id, string name, decimal? unitPrice, short? stock, string categoryName)
        {
            Stock = stock;
            UnitPrice = unitPrice;
            Id = id;
            Name = name;
            CategoryName = categoryName;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal? UnitPrice { get; private set; }
        public short? Stock { get; private set; }
        public string CategoryName { get; private set; }

    }
}
