namespace ValTech.Tests.Acceptance
{
    using System.IO;

    using Moq;

    using NUnit.Framework;

    using SpecsFor;

    public class AsAPaperRoundUser
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
                   this.SUT.ProcessForPaperRound(Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\valid.txt"));     
            }

            [Test]
            public void ThenShouldPrintHeaderAp1()
            {
                this.consoleMock.Verify(x => x.PrintLine("For approach west to east and east to west:"));
            }

            [Test]
            public void ThenShouldPrintSatchelOrderAp1()
            {
                this.consoleMock.Verify(x => x.PrintLine($"Satchel order:1 3 5 4 2"));
            }

            [Test]
            public void ThenShouldPrintRoadCrossingsAp1()
            {
                this.consoleMock.Verify(x => x.PrintLine($"Number of road crossings:1"));
            }

            [Test]
            public void ThenShouldPrintHeaderAp2()
            {
                this.consoleMock.Verify(x => x.PrintLine("For approach west to east with road crossing:"));
            }

            [Test]
            public void ThenShouldPrintSatchelOrderAp2()
            {
                this.consoleMock.Verify(x => x.PrintLine($"Satchel order:1 2 3 4 5"));
            }

            [Test]
            public void ThenShouldPrintRoadCrossingsAp3()
            {
                this.consoleMock.Verify(x => x.PrintLine($"Number of road crossings:4"));
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
                this.SUT.ProcessForPaperRound(Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\parseErro.txt"));
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