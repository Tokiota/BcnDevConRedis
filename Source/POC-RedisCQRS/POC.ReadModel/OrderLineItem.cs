using System;

namespace POC.ReadModel
{
    [Serializable]
    public class OrderLineItem
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int CantidadItems { get; set; }
        public decimal SubTotal { get; set; }
    }
}