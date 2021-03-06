﻿using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Assent.Tests.EndToEnd
{
    public class AssentNonInterativeVariablePreventsReporterFromRunningScenario : BaseScenario
    {
        private Action _action;
        private readonly string _recievedPath = $@"{GetTestDirectory()}\EndToEnd\{nameof(AssentNonInterativeVariablePreventsReporterFromRunningScenario)}.{nameof(WhenTheTestIsRun)}.received.txt";

        public void AndGivenTheAssentNonInteractiveEnvironmentVariableIsSetToTrue()
        {
            Environment.SetEnvironmentVariable("AssentNonInteractive", "TrUe");
        }

        public void WhenTheTestIsRun()
        {
            _action = () => this.Assent("Foo", Configuration);
        }

        public void ThenAnAssentExceptionIsThrown()
        {
            _action.ShouldThrow<AssentException>().WithMessage("Strings differ");
        }

        public void AndThenTheRecievedFileIsNoWritten()
        {
            ReaderWriter.Files.Keys.Should().NotContain(_recievedPath);
        }

        [TearDown]
        public void TearDown()
        {
            Environment.SetEnvironmentVariable("AssentNonInteractive", "");
        }
    }
}
