using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting
{
    public class SaleLineItem
    {
        public decimal ItemPrice { get; private set; }

        public decimal TotalPrice { get { return ItemPrice * Quantity; } } 

        public string Item { get; private set; }

        public int Quantity { get; private set; }


        public SaleLineItem(string item, decimal price, int quantity)
        {
            Item = item;
            Quantity = quantity;
            ItemPrice = price;
        }
    }
}
