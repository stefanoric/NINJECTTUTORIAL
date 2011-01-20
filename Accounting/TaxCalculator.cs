using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace Accounting
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly decimal _rate;

        public TaxCalculator(decimal rate)              
        {
            _rate = rate;
        }

        public decimal CalculateTax(decimal gross)
        {
            return Math.Round(_rate * gross, 2);
        }
    }
}
