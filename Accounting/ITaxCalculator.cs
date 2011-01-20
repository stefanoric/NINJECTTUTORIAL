using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accounting
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(decimal gross);
    }
}
