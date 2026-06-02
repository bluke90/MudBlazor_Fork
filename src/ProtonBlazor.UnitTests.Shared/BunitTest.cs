// Copyright (c) ProtonBlazor 2021
// ProtonBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ProtonBlazor.UnitTests.Shared.Extensions;
using NUnit.Framework;

namespace ProtonBlazor.UnitTests.Shared
{
    public abstract class BunitTest
    {
        protected Bunit.BunitContext Context { get; private set; } = null!;

        [SetUp]
        public virtual void Setup()
        {
            Context = new();
            Context.AddTestServices();
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                Context.Dispose();
            }
            catch (Exception)
            {
                /*ignore*/
            }
        }

    }
}
