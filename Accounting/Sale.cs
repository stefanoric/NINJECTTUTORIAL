using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting
{
    public class Sale
    {
        private readonly ITaxCalculator taxCalculator;

        public Sale(ITaxCalculator taxCalculator)
        {
            this.taxCalculator = taxCalculator;
        }

        private readonly List<SaleLineItem> lineItems = new List<SaleLineItem>();

        public List<SaleLineItem> Items { get { return lineItems; } }

        public void AddItem(SaleLineItem item)
        {
            lineItems.Add(item);
        }

        public decimal GetTotal()
        {
            decimal total = 0M;
            foreach (var item in lineItems)
            {
                total += taxCalculator.CalculateTax(item.TotalPrice) + item.TotalPrice;
            }

            return total;
        }
    }
}
