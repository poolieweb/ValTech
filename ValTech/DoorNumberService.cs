namespace ValTech
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DoorNumberService
    {
        private readonly IConsoleProxy consoleProxy;

        private readonly IParseFileToInt parser;

        public DoorNumberService(IParseFileToInt parser, IConsoleProxy consoleProxy)
        {
            this.parser = parser;
            this.consoleProxy = consoleProxy;
        }

        public void ProcessForPaperRound(string filePath)
        {
            if (this.LoadAndValidateSequence(out var doorNumbers, filePath))
            {
                this.consoleProxy.PrintLine($"For approach west to east and east to west:");
                this.consoleProxy.PrintLine(
                    $"Satchel order:{GetSatchelOrderFor(Approach.WestToEastAndEastToWest, doorNumbers)}");
                this.consoleProxy.PrintLine(
                    $"Number of road crossings:{GetRoadCrossingCount(Approach.WestToEastAndEastToWest, doorNumbers)}");

                this.consoleProxy.PrintLine($"For approach west to east with road crossing:");
                this.consoleProxy.PrintLine(
                    $"Satchel order:{GetSatchelOrderFor(Approach.WestToEastWithRoadCrossing, doorNumbers)}");
                this.consoleProxy.PrintLine(
                    $"Number of road crossings:{GetRoadCrossingCount(Approach.WestToEastWithRoadCrossing, doorNumbers)}");
            }
            else
            {
                this.consoleProxy.PrintLine("File is invalid.");
            }
        }

        public void ProcessForTownPlanner(string filePath)
        {
            if (this.LoadAndValidateSequence(out var doorNumbers, filePath))
            {
                this.consoleProxy.PrintLine("File is valid.");
                this.consoleProxy.PrintLine($"Houses on the street:{GetNumberOfHousesOnStreet(doorNumbers)}");
                this.consoleProxy.PrintLine(
                    $"Left hand (north) side of the street:{GetNumberOfHousesOnNorthSideOfStreet(doorNumbers)}");
                this.consoleProxy.PrintLine(
                    $"Right hand (south) side of the street:{GetNumberOfHousesOnSouthSideOfStreet(doorNumbers)}");
            }
            else
            {
                this.consoleProxy.PrintLine("File is invalid.");
            }
        }


        internal static int GetNumberOfHousesOnNorthSideOfStreet(IEnumerable<int> doorNumbers)
        {
            return doorNumbers.Where(IsOdd).Count();
        }

        internal static int GetNumberOfHousesOnSouthSideOfStreet(IEnumerable<int> doorNumbers)
        {
            return doorNumbers.Count(x => !IsOdd(x));
        }

        internal static int GetNumberOfHousesOnStreet(IEnumerable<int> doorNumbers)
        {
            return doorNumbers.Count();
        }

        internal static int GetRoadCrossingCount(Approach approach, IEnumerable<int> doorNumbers)
        {
            var enumerable = doorNumbers as int[] ?? doorNumbers.ToArray();
            switch (approach)
            {
                case Approach.WestToEastAndEastToWest:
                  
                    if (enumerable.Any(x => !IsOdd(x)) && enumerable.Any(IsOdd))
                    {
                        return 1;
                    }

                    return 0;
                case Approach.WestToEastWithRoadCrossing:
                    var prev = 0;
                    var roadCrossCount = 0;
                    foreach (var current in enumerable)
                    {
                        if (prev != 0)
                            if (IsOdd(current) != IsOdd(prev))
                                roadCrossCount++;
                        prev = current;
                    }

                    return roadCrossCount;
                default:
                    throw new ArgumentOutOfRangeException(nameof(approach), approach, null);
            }

        }

        internal static string GetSatchelOrderFor(Approach approach, IEnumerable<int> doorNumbers)
        {
            var enumerable = doorNumbers as int[] ?? doorNumbers.ToArray();
            switch (approach)
            {
                case Approach.WestToEastAndEastToWest:
                    var northRun = enumerable.Where(IsOdd).ToList();
                    var southRun = enumerable.Where(x => !IsOdd(x)).Reverse().ToList();

                    northRun.AddRange(southRun);
                    return PrintArray(northRun);

                case Approach.WestToEastWithRoadCrossing:
                    return PrintArray(enumerable);
                default:
                    throw new ArgumentOutOfRangeException(nameof(approach), approach, null);
            }

        }

        internal bool LoadAndValidateSequence(out List<int> doorNumbers, string filePath)
        {
            if (this.parser.TryParseFileToEnumerableOfInt(filePath, out doorNumbers))
            {
                if (!CheckFirstNumbersAreValid(doorNumbers))
                {
                    return false;
                }

                var oddNumbers = doorNumbers.Where(IsOdd).ToArray();
                var evenNumbers = doorNumbers.Where(x => !IsOdd(x)).ToArray();

                if (!CheckForMissingOddNumbers(oddNumbers))
                {
                    return false;
                }

                if (!CheckForMissingEvenNumbers(evenNumbers))
                {
                    return false;
                }

                if (!CheckForOutOfSequenceOddNumbers(oddNumbers))
                {
                    return false;
                }

                if (!CheckForOutOfSequenceEvenNumbers(evenNumbers))
                {
                    return false;
                }

                return CheckForDuplicates(doorNumbers);
            }

            return false;
        }

        private static bool CheckForDuplicates(List<int> doorNumbers)
        {
            var groups = doorNumbers.GroupBy(v => v);
            return groups.All(@group => @group.Count() == 1);
        }

        private static bool CheckForOutOfSequenceEvenNumbers(int[] evenNumbers)
        {
            for (var i = 0; i < evenNumbers.Length - 1; i++)
            {
                if (evenNumbers[i].CompareTo(evenNumbers[i + 1]) == 1)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckForOutOfSequenceOddNumbers(int[] oddNumbers)
        {
            for (var i = 0; i < oddNumbers.Length - 1; i++)
            {
                if (oddNumbers[i].CompareTo(oddNumbers[i + 1]) == 1)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckForMissingEvenNumbers(int[] evenNumbers)
        {
            if (evenNumbers.Any() && Enumerable.Range(evenNumbers.Min(), evenNumbers.Max())
                    .Any(x => !IsOdd(x) && !evenNumbers.Contains(x)))
            {
                return false;
            }

            return true;
        }

        private static bool CheckForMissingOddNumbers(int[] oddNumbers)
        {
            if (oddNumbers.Any() && Enumerable.Range(oddNumbers.Min(), oddNumbers.Max())
                    .Any(x => IsOdd(x) && !oddNumbers.Contains(x)))
            {
                return false;
            }

            return true;
        }

        private static bool CheckFirstNumbersAreValid(List<int> doorNumbers)
        {
            if (doorNumbers.First() != 1 && doorNumbers.First() != 2)
            {
                return false;
            }

            return true;
        }

        private static bool IsOdd(int number)
        {
            return number % 2 != 0;
        }

        private static string PrintArray(IEnumerable<int> intArray) => string.Join(" ", intArray.ToArray());
    }
}