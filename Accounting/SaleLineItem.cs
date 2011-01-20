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
