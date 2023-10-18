using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureHunt.Enums;
using TreasureHunt.Helpers;

namespace TreasureHunt.UnitTests
{
    public class ParserTests
    {
        private static readonly object[] _mapDimensions = {
            new object[] {"C - 35 - 40"}
        };

        private static readonly object[] _mountainsInfo = {
            new object[] { new List<string>{"M - 1 - 0", "M - 20 - 15"}}
        };

        private static readonly object[] _treasuresInfo = {
            new object[] { new List<string>{"T - 0 - 3 - 2", "T - 1 - 3 - 3"}}
        };

        private static readonly object[] _adventurersInfo = {
            new object[] { new List<string>{"A - Lara - 1 - 1 - S - AADADAGGA"}}
        };


        [TestCaseSource(nameof(_mapDimensions))]
        public void GetBorders_Should_Return_Coordinatels_with_X_35_and__Y_40(string value)
        {
            var result = MapParserHelper.GetLimits(value);
            Assert.IsInstanceOf<Coordinates>(result);
            Assert.AreEqual(result.X, 35);
            Assert.AreEqual(result.Y, 40);
        }

        [TestCaseSource(nameof(_mountainsInfo))]
        public void GetMountains_Should_Return_a_List_of_Coordinates_of_Length_2(List<string> value)
        {
            var result = MapParserHelper.GetMountains(value);
            Assert.IsInstanceOf<IList<Coordinates>>(result);
            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(result.First().X, 1);
            Assert.AreEqual(result.First().Y, 0);
        }

        [TestCaseSource(nameof(_treasuresInfo))]
        public void GetTreasures_Should_Return_a_dictionary_of_Coordinates_and_int_of_length_2(List<string> value)
        {
            var result = MapParserHelper.GetTreasures(value);
            Assert.IsInstanceOf<IDictionary<Coordinates, int>>(result);
            Assert.AreEqual(result.Count, 2);

            Assert.AreEqual(result.First().Key.X, 0);
            Assert.AreEqual(result.First().Key.Y, 3);
            Assert.AreEqual(result.First().Value, 2);
        }

        [TestCaseSource(nameof(_adventurersInfo))]
        public void GetAdventurer_Should_Return_a_List_of_Adventurers_of_length_1(List<string> value)
        {
            var result = MapParserHelper.GetAdventurers(value);
            Assert.IsInstanceOf<IList<Adventurer>>(result);
            Assert.AreEqual(result.Count, 1);

            Assert.AreEqual(result.First().Name, "Lara");
            Assert.AreEqual(result.First().Coordinates.X, 1);
            Assert.AreEqual(result.First().Coordinates.Y, 1);
            Assert.AreEqual(result.First().Orientation, 'S');
        }
    }
}