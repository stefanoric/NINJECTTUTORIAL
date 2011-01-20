using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Accounting;
using Ninject;
using Xunit;
using Xunit.Extensions;
using Ninject.Parameters;

namespace UnitTest
{
    public class When_creating_a_tax_calculator
    {
        [Fact]
        public void it_should_be_possible_to_create_a_calculator_with_a_specified_rate()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>().To<TaxCalculator>().WithConstructorArgument("rate", .2M);

                var tc = kernel.Get<ITaxCalculator>();

                Assert.Equal(20M, tc.CalculateTax(100M));
            }
        }


        [Fact]
        public void it_should_not_possible_to_create_a_calculator_without_a_specified_rate()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>().To<TaxCalculator>();

                Assert.Throws<Ninject.ActivationException>(() => kernel.Get<ITaxCalculator>()); // this should throw
            }
        }
    }

    public class When_creating_a_sale
    {
        [Fact]
        public void the_tax_calculator_can_be_resolved()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>().To<TaxCalculator>().WithConstructorArgument("rate", .2M);

                var lineItem1 = new SaleLineItem("Gone with the wind", 10M, 1);
                var lineItem2 = new SaleLineItem("Casablanca", 5M, 2);

                var sale = new Sale(kernel.Get<ITaxCalculator>());
                sale.AddItem(lineItem1);
                sale.AddItem(lineItem2);

                Assert.Equal(24M, sale.GetTotal());
            }
        }

        [Fact]
        public void the_sale_can_be_automatically_created()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>().To<TaxCalculator>().WithConstructorArgument("rate", .2M);

                var lineItem1 = new SaleLineItem("Gone with the wind", 10M, 1);
                var lineItem2 = new SaleLineItem("Casablanca", 5M, 2);

                var sale = kernel.Get<Sale>(); // note that we do not mention tax calculator at all here!
                sale.AddItem(lineItem1);
                sale.AddItem(lineItem2);

                Assert.Equal(24M, sale.GetTotal());
            }
        }
    }
}
