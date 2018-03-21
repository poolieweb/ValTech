namespace ValTech.Tests.PaperRoundService.TownPlanner.TownPlanner
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using Should;

    using SpecsFor;

    public class WhenTestingNumberOfHousesOnNorthSideOfStreet
    {

        public class WhenTestingValidSequenceWithSingleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(1, 1)]
            [TestCase(0, 2)]
            public void ShouldReturnOneHouseOnStreet(int result, int value)
            {
                DoorNumberService.GetNumberOfHousesOnNorthSideOfStreet(new List<int>() { value }).ShouldEqual(result);
            }
        }

        public class WhenTestingValidSequenceWithMultipleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(1, 1, 2)]
            [TestCase(3, 1, 3, 2, 4, 5)]
            [TestCase(2, 1, 3)]
            [TestCase(0, 2, 4, 6)]
            [TestCase(3, 1, 2, 3, 4, 5, 6)]
            [TestCase(8, 1, 2, 4, 3, 6, 5, 7, 8, 9, 10, 12, 11, 13, 15)]
            public void ShouldReturnCorrectHouseCountOnStreet(int result, params int[] list)
            {
                DoorNumberService.GetNumberOfHousesOnNorthSideOfStreet(list.ToList()).ShouldEqual(result);
            }
        }
    }
}

