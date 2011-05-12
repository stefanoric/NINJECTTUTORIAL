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
// 02110-1301 USA, or see the FSF site: http://www.fsf.org.using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Accounting;
using Ninject;
using Xunit;
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

        [Fact]
        public void it_should_be_possible_to_create_a_singleton()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>()
                      .To<TaxCalculator>()
                      .InSingletonScope()
                      .WithConstructorArgument("rate", .2M);

                var tc1 = kernel.Get<ITaxCalculator>();
                var tc2 = kernel.Get<ITaxCalculator>();

                Assert.Same(tc1, tc2);
            }
        }

        [Fact]
        public void it_should_be_possible_to_create_a_transient_calculator()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>()
                      .To<TaxCalculator>()
                      .InTransientScope() // this would be the default, so it's redundant
                      .WithConstructorArgument("rate", .2M);

                var tc1 = kernel.Get<ITaxCalculator>();
                var tc2 = kernel.Get<ITaxCalculator>();

                Assert.NotSame(tc1, tc2);
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


        [Fact]
        public void the_tax_calculator_can_be_injected_as_a_property()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>().To<TaxCalculator>().WithConstructorArgument("rate", .2M);

                var lineItem1 = new SaleLineItem("Gone with the wind", 10M, 1);
                var lineItem2 = new SaleLineItem("Casablanca", 5M, 2);

                var sale = kernel.Get<Sale2>(); // note that we do not mention tax calculator at all here!
                sale.AddItem(lineItem1);
                sale.AddItem(lineItem2);

                Assert.Equal(24M, sale.GetTotal());
            }
        }

        [Fact]
        public void the_tax_calculator_can_be_injected_in_a_marked_constructor()
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ITaxCalculator>().To<TaxCalculator>().WithConstructorArgument("rate", .2M);

                var lineItem1 = new SaleLineItem("Gone with the wind", 10M, 1);
                var lineItem2 = new SaleLineItem("Casablanca", 5M, 2);

                var sale = kernel.Get<Sale3>(); // note that we do not mention tax calculator at all here!
                sale.AddItem(lineItem1);
                sale.AddItem(lineItem2);

                Assert.Equal(24M, sale.GetTotal());
            }
        }
    }
}