using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TreasureHunt;
using TreasureHunt.Enums;

namespace TreasureHunt.UnitTests
{
    public class AdventurerTests
    {
        private static readonly object[] _mountainCoordinates = {
            new object[] {new Coordinates(2, 1)},
            new object[] {new Coordinates(1, 0)}
        };

        private static readonly object[] _noMountainCoordinates = {
            new object[] {new Coordinates(2, 3)},
            new object[] {new Coordinates(1, 1)}
        };

        private static readonly object[] extraAdventurerCoordinates = {
            new object[] {new Coordinates(1, 1)},
            new object[] {new Coordinates(2, 3)}
        };

        private static readonly object[] _noExtraAdventurerCoordinates = {
            new object[] {new Coordinates(1, 0)},
            new object[] {new Coordinates(2, 1)}
        };

        private static readonly object[] _treasureCoordinates = {
            new object[] {new Coordinates(0, 3)}
        };

        private static readonly object[] _noTreasureCoordinates = {
            new object[] {new Coordinates(1, 2)},
            new object[] {new Coordinates(1, 3)}
        };

        private static readonly object[] _adventurerCoordinates = {
            new object[] {new Coordinates(1, 1)}
        };


        Map map = null;
        Adventurer adventurer = null;
        IList<Adventurer> adventurers = null;
        IList<Coordinates> mountains = null;
        IDictionary<Coordinates, int> treasures = null;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            IList<string> fileElements = new List<string> {
                "C - 3 - 4","M - 1 - 0", "M - 2 - 1", "T - 0 - 3 - 2",
                "T - 1 - 3 - 0", "A - Lara - 1 - 1 - S - AADADAGGA", "A - Clark - 2 - 3 - N - AADADAGGA"
            };
            map = new Map();
            map.Build(fileElements);
            mountains = map.Mountains;
        }

        [SetUp]
        public void SetUp()
        {
            adventurer = map.Adventurers.FirstOrDefault();
            adventurers = map.Adventurers;
            treasures = map.Treasures;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            map = null;
            mountains = null;
        }

        [TearDown]
        public void TearDown()
        {
            adventurer.Coordinates = new Coordinates(1,1);
            adventurer.TreasuresCollected = 0;
        }

        [TestCase('D')]
        public void ChangeOrientation_Should_change_the_orientation_from_south_to_west(char direction)
        {
            adventurer.Orientation = 'S';
            adventurer.ChangeOrientation(direction);
            Assert.AreEqual(adventurer.Orientation, 'O');
        }

        [TestCase('G')]
        public void ChangeOrientation_Should_change_the_orientation_from_south_to_east(char direction)
        {
            adventurer.Orientation = 'S';
            adventurer.ChangeOrientation( direction);
            Assert.AreEqual(adventurer.Orientation, 'E');
        }

        [TestCase('D')]
        public void ChangeOrientation_Should_change_the_orientation_from_east_to_north(char direction)
        {
            adventurer.Orientation = 'E';
            adventurer.ChangeOrientation( direction);
            Assert.AreEqual(adventurer.Orientation, 'S');
        }

        [TestCase('G')]
        public void ChangeOrientation_Should_change_the_orientation_from_east_to_south(char direction)
        {
            adventurer.Orientation = 'E';
            adventurer.ChangeOrientation(direction);
            Assert.AreEqual(adventurer.Orientation, 'N');
        }

        [TestCase('D')]
        public void ChangeOrientation_Should_change_the_orientation_from_north_to_west(char direction)
        {
            adventurer.Orientation = 'N';

            adventurer.ChangeOrientation( direction);
            Assert.AreEqual(adventurer.Orientation, 'E');
        }

        [TestCase('G')]
        public void ChangeOrientation_Should_change_the_orientation_from_north_to_east(char direction)
        {
            adventurer.Orientation = 'N';
            adventurer.ChangeOrientation( direction);
            Assert.AreEqual(adventurer.Orientation, 'O');
        }

        [TestCase('D')]
        public void ChangeOrientation_Should_change_the_orientation_from_west_to_north(char direction)
        {
            adventurer.Orientation = 'O';
            adventurer.ChangeOrientation( direction);
            Assert.AreEqual(adventurer.Orientation, 'N');
        }

        [TestCase('G')]
        public void ChangeOrientation_Should_change_the_orientation_from_west_to_south(char direction)
        {
            adventurer.Orientation = 'O';
            adventurer.ChangeOrientation( direction);
            Assert.AreEqual(adventurer.Orientation, 'S');
        }

        [TestCaseSource(nameof(_mountainCoordinates))]
        public void IsCollidingWithMountains_Should_return_true(Coordinates nextCoordinates)
        {
            bool isColliding = Adventurer.IsCollidingWithMountains(mountains, nextCoordinates);
            Assert.IsTrue(isColliding);
        }

        [TestCaseSource(nameof(_noMountainCoordinates))]
        public void IsCollidingWithMountains_Should_return_false(Coordinates nextCoordinates)
        {
            bool isColliding = Adventurer.IsCollidingWithMountains(mountains, nextCoordinates);
            Assert.IsFalse(isColliding);
        }

        [TestCaseSource(nameof(_adventurerCoordinates))]
        public void IsCollidingWithAdventurers_Should_return_true(Coordinates nextCoordinates)
        {
            bool isColliding = Adventurer.IsCollidingWithAdventurers(adventurers, nextCoordinates);
            Assert.IsTrue(isColliding);
        }

        [TestCaseSource(nameof(_noExtraAdventurerCoordinates))]
        public void IsCollidingWithAdventurers_Should_return_false(Coordinates nextCoordinates)
        {
            bool isColliding = Adventurer.IsCollidingWithAdventurers(adventurers, nextCoordinates);
            Assert.IsFalse(isColliding);
        }

        [TestCaseSource(nameof(_treasureCoordinates))]
        public void CollectTreasure_Should_increase_adventurer_collectedTreasures_to_1_and_decrease_treasures_numbers(Coordinates adventurerCoordinates)
        {
            adventurer.CollectPotentialTreasures(treasures, adventurerCoordinates);
            Assert.AreEqual(adventurer.TreasuresCollected, 1);
        }

        [TestCaseSource(nameof(_noTreasureCoordinates))]
        public void CollectTreasure_Should_not_increase_adventurer_collectedTreasures_and_decrease_treasures_numbers(Coordinates adventurerCoordinates)
        {
            adventurer.CollectPotentialTreasures(treasures, adventurerCoordinates);
             Assert.AreEqual(adventurer.TreasuresCollected, 0);
        }

        [TestCaseSource(nameof(_adventurerCoordinates))]
        public void MoveForward_Should_update_adventurer_position_from_1_1_to_1_2(Coordinates currentCoordinates)
        {
            adventurer.Orientation = 'S';
            adventurer.Coordinates = currentCoordinates;
            adventurer.MoveForward(map);
            Assert.AreEqual(adventurer.Coordinates.X, 1);
            Assert.AreEqual(adventurer.Coordinates.Y, 2);
        }

        [TestCaseSource(nameof(_adventurerCoordinates))]
        public void MoveForward_Should_update_adventurer_position_from_1_1_to_0_1(Coordinates currentCoordinates)
        {
            adventurer.Orientation = 'O';
            adventurer.Coordinates = currentCoordinates;
            adventurer.MoveForward(map);
            Assert.AreEqual(adventurer.Coordinates.X, 0);
            Assert.AreEqual(adventurer.Coordinates.Y, 1);
        }

        [TestCaseSource(nameof(_adventurerCoordinates))]
        public void MoveForward_Should_not_update_adventurer_position_because_of_north_mountain(Coordinates currentCoordinates)
        {
            adventurer.Orientation = 'N';
            adventurer.Coordinates = currentCoordinates;
            adventurer.MoveForward(map);
            Assert.AreEqual(adventurer.Coordinates.X, 1);
            Assert.AreEqual(adventurer.Coordinates.Y, 1);
        }

        [TestCaseSource(nameof(_adventurerCoordinates))]
        public void MoveForward_Should_not_update_adventurer_position_because_of_east_mountain(Coordinates currentCoordinates)
        {
            adventurer.Orientation = 'E';
            adventurer.Coordinates = currentCoordinates;
            adventurer.MoveForward(map);
            Assert.AreEqual(adventurer.Coordinates.X, 1);
            Assert.AreEqual(adventurer.Coordinates.Y, 1);
        }
    }
}