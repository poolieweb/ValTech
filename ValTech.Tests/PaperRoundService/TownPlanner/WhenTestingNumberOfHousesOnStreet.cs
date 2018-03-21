namespace ValTech.Tests.PaperRoundService.TownPlanner.TownPlanner
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using Should;

    using SpecsFor;

    public class WhenTestingNumberOfHousesOnStreet
    {

        public class WhenTestingValidSequenceWithSingleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(1)]
            [TestCase(2)]
            public void ShouldReturnOneHouseOnStreet(int value)
            {
                DoorNumberService.GetNumberOfHousesOnStreet(new List<int>() { value }).ShouldEqual(1);
            }
        }

        public class WhenTestingValidSequenceWithMultipleValues : SpecsFor<DoorNumberService>
        {
            [TestCase(2, 1, 2)]
            [TestCase(5, 1, 3, 2, 4, 5)]
            [TestCase(2, 1, 3)]
            [TestCase(3, 2, 4, 6)]
            [TestCase(6, 1, 2, 3, 4, 5, 6)]
            [TestCase(14, 1, 2, 4, 3, 6, 5, 7, 8, 9, 10, 12, 11, 13, 15)]
            public void ShouldReturnCorrectHouseCountOnStreet(int result, params int[] list)
            {
                DoorNumberService.GetNumberOfHousesOnStreet(list.ToList()).ShouldEqual(result);
            }
        }
    }
}

