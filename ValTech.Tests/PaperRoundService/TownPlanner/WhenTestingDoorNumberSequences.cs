namespace ValTech.Tests.PaperRoundService.TownPlanner.TownPlanner
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using Should;

    using SpecsFor;

    public class WhenTestingDoorNumberSequences
    {
        public class WhenTestingInvalidSequenceWithSingleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(0)]
            [TestCase(3)]
            [TestCase(4)]
            public void ShouldReturnFalse(int value)
            {
                var doorNumberEnumerable = new List<int>(){value};
                this.GetMockFor<IParseFileToInt>()
                    .Setup(x => x.TryParseFileToEnumerableOfInt(It.IsAny<string>(), out doorNumberEnumerable))
                    .Returns(true);

                this.SUT.LoadAndValidateSequence(out doorNumberEnumerable, string.Empty).ShouldBeFalse();
            }
        }

        public class WhenTestingInvalidSequenceWithMultipleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(0, 1)]
            [TestCase(1, 1)]
            [TestCase(1, 5)]
            [TestCase(2, 6)]
            [TestCase(1, 2, 3, 4, 5, 5)]
            [TestCase(1, 2, 3, 4, 5, 6, 10, 8)]
            public void ShouldReturnFalse(params int[] list)
            {
                var doorNumberEnumerable = list.ToList();
                this.GetMockFor<IParseFileToInt>()
                    .Setup(x => x.TryParseFileToEnumerableOfInt(It.IsAny<string>(), out doorNumberEnumerable))
                    .Returns(true);

                this.SUT.LoadAndValidateSequence(out doorNumberEnumerable, string.Empty).ShouldBeFalse();
            }
        }

        public class WhenTestingValidSequenceWithSingleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(1)]
            [TestCase(2)]
            public void ShouldReturnFalse(int value)
            {
                var doorNumberEnumerable = new List<int>() { value };
                this.GetMockFor<IParseFileToInt>()
                    .Setup(x => x.TryParseFileToEnumerableOfInt(It.IsAny<string>(), out doorNumberEnumerable))
                    .Returns(true);

                this.SUT.LoadAndValidateSequence(out doorNumberEnumerable, string.Empty).ShouldBeTrue();
            }
        }

        public class WhenTestingValidSequenceWithMultipleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(1, 2)]
            [TestCase(1, 3, 2, 4, 5)]
            [TestCase(1, 3)]
            [TestCase(2, 4, 6)]
            [TestCase(1, 2, 3, 4, 5, 6)]
            [TestCase(1, 2, 4, 3, 6, 5, 7, 8, 9, 10, 12, 11, 13, 15)]
            public void ShouldReturnFalse(params int[] list)
            {
                var doorNumberEnumerable = list.ToList();
                this.GetMockFor<IParseFileToInt>()
                    .Setup(x => x.TryParseFileToEnumerableOfInt(It.IsAny<string>(), out doorNumberEnumerable))
                    .Returns(true);

                this.SUT.LoadAndValidateSequence(out doorNumberEnumerable, string.Empty).ShouldBeTrue();
            }
        }

    }
}