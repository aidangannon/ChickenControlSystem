﻿using BLL.HardwareModules.Light.Commands;
using Crosscutting.DateTime;
using Crosscutting.Sequencing.Contract;
using Crosscutting.Services.Contract.HAL.Interface;
using Crosscutting.Threading;
using Crosscutting.UnitTest;
using NSubstitute;

namespace Tests.Unit.BLL.HardwareModules.Light.Commands.CheckForLightCommandTests
{
    public class Given_CheckForLightCommand : GenericGivenWhenThenTests<CheckForLightCommand>
    {
        protected IValidateOperationService MockValidateOperationService;
        protected IAnalogOperations MockAnalogOperations;
        protected ILightSensor MockLightSensor;
        protected ITimeService MockTimeService;
        protected IThreadOperations MockThreadingOperations;

        public override void Given()
        {
            MockValidateOperationService = Substitute.For<IValidateOperationService>();
            MockAnalogOperations = Substitute.For<IAnalogOperations>();
            MockLightSensor = Substitute.For<ILightSensor>();
            MockThreadingOperations = Substitute.For<IThreadOperations>();

            SUT = new CheckForLightCommand(
                MockValidateOperationService,
                MockAnalogOperations,
                MockLightSensor,
                MockThreadingOperations
            );
        }
    }
}