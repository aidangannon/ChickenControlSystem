﻿using System;
using System.Linq;
using ControlLine.Dto;
using ControlLine.Exception;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace ControlLineUnitTests.ControlLineSocketsTests.SendOperation.Scenarios
{
    [TestFixture]
    [Description("Given ControlLineSockets.SendOperation Is Called, When Payload Cannot Be Sent")]
    public class ControlLineTimesOutTests : SendOperationTests
    {
        private readonly byte[] _payload = {115, 121, 2, 255, 255};

        private readonly OperationDto _operation = new OperationDto()
        {
            Operation = 115,
            Device = 121,
            Params = new[] {65535},
            Timeout = Timeout
        };

        private const int RecievePeriod = 100;

        [SetUp]
        protected new void Init()
        {
            base.Init();

            //arrange
            MockThreadOperations
                .WaitUntilFuncTimeout(Arg.Any<Func<byte[]>>(), Arg.Any<int>())
                .Throws<ThreadTimeout>();
        }

        private void When()
        {
            try
            {
                Sut.SendOperation(_operation);
            }
            catch (ControlLineTimeOut)
            {
            }
        }

        private void WhenWithErrors()
        {
            Sut.SendOperation(_operation);
        }

        [Test]
        [Description("Then Connection Was Opened")]
        public void ConnectionOpenTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received(1)
                .Connect();
        }

        //TODO: change to 1 method call
        [Test]
        [Description("Then Payload Was Sent")]
        public void PayloadSendTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received()
                .Send(Arg.Is<byte[]>(payload => payload.SequenceEqual(_payload)));
            MockSocketClient
                .Received(1)
                .Send(Arg.Any<byte[]>());
        }

        [Test]
        [Description("Then Data Was Attempted To Be Received")]
        public void DataRecievedTest()
        {
            //act
            When();

            //assert
            MockThreadOperations
                .Received(1)
                .WaitUntilFuncTimeout(Arg.Any<Func<byte[]>>(), Arg.Any<int>());
            MockThreadOperations
                .Received()
                .WaitUntilFuncTimeout(
                    Arg.Any<Func<byte[]>>(),
                    Arg.Is(Timeout)
                );
        }

        [Test]
        [Description("Then Connection Was Closed")]
        public void ConnectionCloseTest()
        {
            //act
            When();

            //assert
            MockSocketClient
                .Received(1)
                .Close();
        }

        [Test]
        [Description("Then Response Status Was Not Validated")]
        public void IsErrorTest()
        {
            //act
            When();

            //assert
            MockStatusValidator
                .DidNotReceive()
                .IsError(Arg.Any<byte>());
        }

        [Test]
        [Description("Then Response Error Was Not Validated")]
        public void ValidateErrorTest()
        {
            //act
            When();

            //assert
            MockStatusValidator
                .DidNotReceive()
                .ValidateError(Arg.Any<byte>());
        }

        public void ControlLineTimeOutTest()
        {
            //assert
            Assert.Throws<ControlLineTimeOut>(WhenWithErrors);
        }
    }
}