// Copyright 2011, Stefano Ricciardi - www.stefanoricciardi.com
//
// This is free software; you can redistribute it and/or modify it
// under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation; either version 2.1 of
// the License, or (at your option) any later version.

// This software is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public
// License along with this software; if not, write to the Free
// Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
// 02110-1301 USA, or see the FSF site: http://www.fsf.org.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace Accounting
{
    public class Sale2
    {
        [Inject]
        public ITaxCalculator TaxCalculator { get; set; }

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
                total += TaxCalculator.CalculateTax(item.TotalPrice) + item.TotalPrice;
            }

            return total;
        }
    }
}

