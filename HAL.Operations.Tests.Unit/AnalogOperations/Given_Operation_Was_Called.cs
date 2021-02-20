﻿using ControlLine.Contract;
using HAL.Operations.Contract;
using NSubstitute;
using UnitTest;

namespace HAL.Operations.Tests.Unit.AnalogOperations
{
    public abstract class Given_Operation_Was_Called : GenericGivenWhenThenTests<Operations.AnalogOperations>
    {
        protected IControlLine MockControlLine;
        protected IErrorService MockErrorService;

        protected override void Given()
        {
            MockControlLine = Substitute.For<IControlLine>();
            MockErrorService = Substitute.For<IErrorService>();

            SUT = new Operations.AnalogOperations(MockErrorService, MockControlLine);
        }
    }
}