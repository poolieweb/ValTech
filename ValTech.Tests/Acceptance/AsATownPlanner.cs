namespace ValTech.Tests.Acceptance
{
    using System.IO;

    using Moq;

    using NUnit.Framework;

    using SpecsFor;

    public class AsATownPlanner
    {
        public class WhenProcessingAFile : SpecsFor<DoorNumberService>
        {
            private Mock<IConsoleProxy> consoleMock;

            protected override void InitializeClassUnderTest()
            {
                this.consoleMock = new Mock<IConsoleProxy>();

                this.SUT = new DoorNumberService(
                    new StreetNumberFileParser(this.consoleMock.Object),
                    this.consoleMock.Object);
            }

            protected override void When()
            {
                this.SUT.ProcessForTownPlanner(Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\valid.txt"));
            }

            [Test]
            public void ThenShouldPrintValidation()
            {
                this.consoleMock.Verify(x => x.PrintLine("File is valid."));
            }

            [Test]
            public void ThenShouldPrintHousesOnTheStreet()
            {
                this.consoleMock.Verify(x => x.PrintLine($"Houses on the street:5"));
            }

            [Test]
            public void ThenShouldPrintNorthCount()
            {
                this.consoleMock.Verify(x => x.PrintLine($"Left hand (north) side of the street:3"));
            }

            [Test]
            public void ThenShouldPrintSouthCount()
            {
                this.consoleMock.Verify(x => x.PrintLine("Right hand (south) side of the street:2"));
            }
        }

        public class WhenProcessingAInvalidFile : SpecsFor<DoorNumberService>
        {
            private Mock<IConsoleProxy> consoleMock;

            protected override void InitializeClassUnderTest()
            {
                this.consoleMock = new Mock<IConsoleProxy>();

                this.SUT = new DoorNumberService(
                    new StreetNumberFileParser(this.consoleMock.Object),
                    this.consoleMock.Object);
            }

            protected override void When()
            {
                this.SUT.ProcessForTownPlanner(Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\parseErro.txt"));
            }

            [Test]
            public void ThenShouldPrintErrorMsg()
            {
                this.consoleMock.Verify(x => x.PrintLine("Unable to find file."));
            }

            [Test]
            public void ThenShouldPrintFailedValidation()
            {
                this.consoleMock.Verify(x => x.PrintLine($"File is invalid."));
            }

        }

    }
}