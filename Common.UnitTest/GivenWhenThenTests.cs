﻿using NUnit.Framework;

namespace UnitTest
{
    public class GivenWhenThenTests
    {
        [SetUp]
        public void SetUpClass()
        {
            Given();
        }

        [SetUp]
        public void SetUpMethod()
        {
            When();
        }

        /// <summary>
        ///     set up for test fixture
        /// </summary>
        public virtual void Given()
        {
        }

        /// <summary>
        ///     set up for test method, SUT operation can be run (it will ignore exceptions)
        /// </summary>
        public virtual void When()
        {
        }
    }
}