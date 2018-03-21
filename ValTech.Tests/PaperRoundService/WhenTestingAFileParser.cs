namespace ValTech.Tests.PaperRoundService.TownPlanner
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using Should;

    using SpecsFor;
    using SpecsFor.ShouldExtensions;

    public class WhenTestingAFileParser
    {
        public class WhenFileIsParsed: SpecsFor<StreetNumberFileParser>
        {
            private List<int> doorNumberEnumerable;

            private bool parseResult;

            protected override void When()
            {
                this.parseResult = this.SUT.TryParseFileToEnumerableOfInt(
                    Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\valid.txt"),
                    out this.doorNumberEnumerable);
            }
            
            [Test]
            public void ThenShouldReturnTrue()
            {
                this.parseResult.ShouldBeTrue();
            }

            [Test]
            public void ThenShouldPopulateDoorNumbers()
            {
                this.doorNumberEnumerable.ShouldLookLike(new List<int> { 1, 2, 3, 4, 5 });
            }
        }
        public class WhenFileCanNotBeFound : SpecsFor<StreetNumberFileParser>
        {
            private List<int> doorNumberEnumerable;

            private bool parseResult;

            protected override void When()
            {
                this.parseResult = this.SUT.TryParseFileToEnumerableOfInt(string.Empty, out this.doorNumberEnumerable);
            }

            [Test]
            public void ThenShouldOutFriendlyMessage()
            {
                this.GetMockFor<IConsoleProxy>()
                    .Verify(s => s.PrintLine("Unable to find file."));
            }

            [Test]
            public void ThenShouldReturnFalse()
            {
                this.parseResult.ShouldBeFalse();
            }

            [Test]
            public void ThenShouldPopulateWithEmptyArray()
            {
                this.doorNumberEnumerable.Any().ShouldBeFalse();
            }
        }

        public class WhenFileIsBlank : SpecsFor<StreetNumberFileParser>
        {
            private List<int> doorNumberEnumerable;

            private bool parseResult;

            protected override void When()
            {
                this.parseResult = this.SUT.TryParseFileToEnumerableOfInt(
                    Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\blankFile.txt"),
                    out this.doorNumberEnumerable);
            }

            [Test]
            public void ThenShouldOutFriendlyMessage()
            {
                this.GetMockFor<IConsoleProxy>()
                    .Verify(s => s.PrintLine("Unable to find any valid routes"));
            }


            [Test]
            public void ThenShouldReturnFalse()
            {
                this.parseResult.ShouldBeFalse();
            }
            
            [Test]
            public void ThenShouldPopulateWithEmptyArray()
            {
                this.doorNumberEnumerable.Any().ShouldBeFalse();
            }
        }

        public class WhenFileCanNotBeParsedToInt : SpecsFor<StreetNumberFileParser>
        {
            private List<int> doorNumberEnumerable;

            private bool parseResult;

            protected override void When()
            {
                this.parseResult = this.SUT.TryParseFileToEnumerableOfInt(
                    Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\parseError.txt"),
                    out this.doorNumberEnumerable);
            }

            [Test]
            public void ThenShouldOutFriendlyMessage()
            {
                this.GetMockFor<IConsoleProxy>()
                    .Verify(s => s.PrintLine("Unable to parse file to valid house numbers"));
            }

            [Test]
            public void ThenShouldReturnFalse()
            {
                this.parseResult.ShouldBeFalse();
            }
            
            [Test]
            public void ThenShouldPopulateWithEmptyArray()
            {
                this.doorNumberEnumerable.Any().ShouldBeFalse();
            }
        }

        public class WhenFileParserThrowsUnhandledException : SpecsFor<StreetNumberFileParser>
        {
            private List<int> doorNumberEnumerable;

            private bool parseResult;

            protected override void When()
            {
                this.parseResult = this.SUT.TryParseFileToEnumerableOfInt(
                    Path.Combine(TestContext.CurrentContext.TestDirectory, @"PaperRoundService\TestFiles\overflow.txt"),
                    out this.doorNumberEnumerable);
            }

            [Test]
            public void ThenShouldOutFriendlyMessage()
            {
                this.GetMockFor<IConsoleProxy>()
                    .Verify(s => s.PrintLine("Unexpected exception:Value was either too large or too small for an Int32."));
            }
            
            [Test]
            public void ThenShouldReturnFalse()
            {
                this.parseResult.ShouldBeFalse();
            }
            
            [Test]
            public void ThenShouldPopulateWithEmptyArray()
            {
                this.doorNumberEnumerable.Any().ShouldBeFalse();
            }
        }

    }
}