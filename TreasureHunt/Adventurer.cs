using TreasureHunt.Enums;

namespace TreasureHunt
{
    public class Adventurer
    {
        public Queue<char> Directions { get; set; } = new Queue<char>();
        public int TreasuresCollected { get; set; }
        public string Name { get; set; }
        public Coordinates Coordinates { get; set; }
        public char Orientation { get; set; }

        public void Move (Map map)
        {
            if (Directions.Count > 0)
            {
                char instruction = Directions.Dequeue();
                switch (instruction)
                {
                    case 'A':
                        MoveForward(map);
                        break;
                    case 'D':
                    case 'G':
                        ChangeOrientation(instruction);
                        break;
                    default:
                        throw new NotSupportedException("The instruction is not supported.");
                }
            }
        }

        public void ChangeOrientation(char direction)
        {
            switch (Orientation)
            {
                case 'N':
                    if (direction == 'D')
                    {
                        this.Orientation = 'E';
                    }
                    else if (direction == 'G')
                    {
                        this.Orientation = 'O';
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported Instruction");
                    }
                    break;
                case 'S':
                    if (direction == 'D')
                    {
                        this.Orientation = 'O';
                    }
                    else if (direction == 'G')
                    {
                        this.Orientation = 'E';
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported Instruction");
                    }
                    break;
                case 'O':
                    if (direction == 'D')
                    {
                        this.Orientation = 'N';
                    }
                    else if (direction == 'G')
                    {
                        this.Orientation = 'S';
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported Instruction");
                    }
                    break;
                case 'E':
                    if (direction == 'D')
                    {
                        this.Orientation = 'S';
                    }
                    else if (direction == 'G')
                    {
                        this.Orientation = 'N';
                    }
                    else
                    {
                        throw new NotSupportedException("Unsupported Instruction");
                    }
                    break;
                default:
                    throw new NotSupportedException("Unsupported Orientation");
            }
        }

        public void MoveForward(Map map)
        {
            Coordinates nextCoordinates = new Coordinates(-1, -1);
            int nextCoordX, nextCoordY;

            switch (Orientation)
            {
                case 'N':
                    nextCoordinates = new Coordinates(Coordinates.X, Coordinates.Y - 1);
                    break;
                case 'S':
                    nextCoordinates = new Coordinates(Coordinates.X, Coordinates.Y + 1);
                    break;
                case 'O':
                    nextCoordinates = new Coordinates(Coordinates.X - 1, Coordinates.Y);
                    break;
                case 'E':
                    nextCoordinates = new Coordinates(Coordinates.X + 1, Coordinates.Y);
                    break;
                default:
                    throw new NotSupportedException("The orientation is not supported");
            }

            if(Enumerable.Range(0, map.Dimensions.Y).Contains(nextCoordinates.Y) && Enumerable.Range(0, map.Dimensions.X).Contains(nextCoordinates.X) && !WillCollideWithAdventurerOrMountain(map, nextCoordinates))
            {
                UpdateCoords(map.Treasures, nextCoordinates);
            }
        }


        internal bool WillCollideWithAdventurerOrMountain (Map map, Coordinates coordinates)
        {
            return IsCollidingWithAdventurers(map.Adventurers, coordinates) || IsCollidingWithMountains(map.Mountains, coordinates);
        }

        public static bool IsCollidingWithAdventurers(IList<Adventurer> adventurers, Coordinates nextCoords)
        {
            return adventurers.Where(adventurer => (adventurer.Coordinates.X == nextCoords.X && adventurer.Coordinates.Y == nextCoords.Y)).Count() > 0;
        }

        public static bool IsCollidingWithMountains(IList<Coordinates> mountains, Coordinates nextCoords)
        {
            return mountains.Where(mountain => (mountain.X == nextCoords.X && mountain.Y == nextCoords.Y)).Count() >0 ;
        }
        private void UpdateCoords(IDictionary<Coordinates, int> treasures, Coordinates nextCoordinates)
        {
            Coordinates = nextCoordinates;
            CollectPotentialTreasures(treasures, nextCoordinates);
        }

        public void CollectPotentialTreasures(IDictionary<Coordinates, int> treasures, Coordinates nextCoordinates)
        {
            try
            {
               if(treasures.ContainsKey(nextCoordinates))
                {
                    var numberOfTreasures = treasures[nextCoordinates];
                    TreasuresCollected++;
                    if (numberOfTreasures > 1)
                    {
                        treasures[nextCoordinates]--;
                    }
                    else
                    {
                        treasures.Remove(nextCoordinates);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
