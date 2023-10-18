namespace TreasureHunt.Helpers
{
    public static class MapParserHelper
    {
        public static IList<Adventurer> GetAdventurers(IList<String> adventurersInfos)
        {
            try
            {
                IList<Adventurer> adventurers = new List<Adventurer>();
                foreach (var info in adventurersInfos)
                {
                    string[] adventurerInfo = info.Replace(" ", "").Remove(0, 2).Split("-");
                    int coordX, coordY;
                    char orientation;
                    Adventurer newAdventurer = new Adventurer();
                    newAdventurer.Name = adventurerInfo[0];
                    newAdventurer.Directions = new Queue<char>(adventurerInfo[4]);
                    newAdventurer.TreasuresCollected = 0;
                    if (Int32.TryParse(adventurerInfo[1], out coordX) && Int32.TryParse(adventurerInfo[2], out coordY) && Char.TryParse(adventurerInfo[3], out orientation))
                    {
                        Coordinates coords = new Coordinates(coordX, coordY);
                        newAdventurer.Coordinates = coords;
                        newAdventurer.Orientation = orientation;
                    }
                    else
                    {
                        throw new FormatException("Wrong input for adventurers information");
                    }
                    adventurers.Add(newAdventurer);
                }
                return adventurers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IList<Coordinates> GetMountains(IList<String> mountainInputs)
        {
            try
            {
                IList<Coordinates> mountainCoords = new List<Coordinates>();
                foreach (var mountain in mountainInputs)
                {
                    string[] mountainInfo = mountain.Replace(" ", "").Remove(0, 2).Split("-");
                    int coordX, coordY;
                    Coordinates coords;
                    if (Int32.TryParse(mountainInfo[0], out coordX) && Int32.TryParse(mountainInfo[1], out coordY))
                    {
                        coords = new Coordinates(coordX, coordY);
                        mountainCoords.Add(coords);
                    }
                    else
                    {
                        throw new FormatException("Wrong input for mountain information");
                    }
                }
                return mountainCoords;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IDictionary<Coordinates, int> GetTreasures(IList<String> treasuresInfos)
        {
            try
            {
                IDictionary<Coordinates, int> treasures = new Dictionary<Coordinates, int>();
                foreach (var info in treasuresInfos)
                {
                    string[] treasureInfo = info.Replace(" ", "").Remove(0, 2).Split("-");
                    int coordX, coordY, numberOfTreasure;
                    if (Int32.TryParse(treasureInfo[0], out coordX) && Int32.TryParse(treasureInfo[1], out coordY) && Int32.TryParse(treasureInfo[2], out numberOfTreasure))
                    {
                        Coordinates coords = new Coordinates(coordX, coordY);
                        treasures[coords] = numberOfTreasure;
                    }
                    else
                    {
                        throw new FormatException("Wrong input for Treasure information");
                    }
                }
                return treasures;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Coordinates GetLimits(string dimensions)
        {
            try
            {
                dimensions = dimensions.Trim();
                if (!dimensions.StartsWith("C") && dimensions.Where(e => (e == '-')).Count() != 2)
                {
                    throw new FormatException("Wrong map informations format");
                }
                string[] splitInfo = dimensions.Replace(" ", "").Remove(0, 2).Split("-");
                int borderX, borderY;
                Coordinates borders;
                if (Int32.TryParse(splitInfo[0], out borderX) && Int32.TryParse(splitInfo[1], out borderY))
                {
                    borders = new Coordinates(borderX, borderY);
                }
                else
                {
                    throw new FormatException("Wrong map informations format");
                }
                return borders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
