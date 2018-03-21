namespace ValTech.Tests.PaperRoundService.TownPlanner.TownPlanner
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NUnit.Framework;

    using Should;

    using SpecsFor;

    public class WhenTestingWestToEastAndEastToWestApproach
    {

        public class WhenTestingSatchelOrderWithSingleHouseStreets : SpecsFor<DoorNumberService>
        {
            [TestCase("1", 1)]
            [TestCase("2", 2)]
            public void ShouldReturnCorrectSatchelOrder(string result, int value)
            {
                DoorNumberService.GetSatchelOrderFor(Approach.WestToEastAndEastToWest, new List<int>() { value }).ShouldEqual(result);
            }

            [TestCase(0, 1)]
            [TestCase(0, 2)]
            public void ShouldReturnCorrectRoadCrossingCount(int result, int value)
            {
                DoorNumberService.GetRoadCrossingCount(Approach.WestToEastAndEastToWest, new List<int>() { value }).ShouldEqual(result);
            }

        }

        public class WhenTestingSatchelOrderWithMultipleValues : SpecsFor<DoorNumberService>
        {
            [TestCase("1 2", 1, 2)]
            [TestCase("1 3 5 4 2", 1, 3, 2, 4, 5)]
            [TestCase("1 3 2", 2, 1, 3)]
            [TestCase("6 4 2", 2, 4, 6)]
            [TestCase("1 3 5 6 4 2", 1, 2, 3, 4, 5, 6)]
            [TestCase("1 3 5 7 9 11 13 15 12 10 8 6 4 2", 1, 2, 4, 3, 6, 5, 7, 8, 9, 10, 12, 11, 13, 15)]
            public void ShouldReturnCorrectSatchelOrder(string result, params int[] list)
            {
                DoorNumberService.GetSatchelOrderFor(Approach.WestToEastAndEastToWest, list.ToList()).ShouldEqual(result);
            }


            [TestCase(1, 1, 2)]
            [TestCase(1, 1, 3, 2, 4, 5)]
            [TestCase(1, 2, 1, 3)]
            [TestCase(0, 2, 4, 6)]
            [TestCase(0, 1, 3, 5)]
            [TestCase(1, 1, 2, 3, 4, 5, 6)]
            [TestCase(1, 1, 2, 4, 3, 6, 5, 7, 8, 9, 10, 12, 11, 13, 15)]
            public void ShouldReturnCorrectRoadCrossingCount(int result, params int[] list)
            {
                DoorNumberService.GetRoadCrossingCount(Approach.WestToEastAndEastToWest, list.ToList()).ShouldEqual(result);
            }
        }
    }

   
}

