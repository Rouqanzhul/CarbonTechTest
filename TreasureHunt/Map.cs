using System.Text;
using TreasureHunt.Helpers;

namespace TreasureHunt
{
    public class Map
    {

        public Coordinates Dimensions { get; set; }
        public IList<Adventurer> Adventurers { get; set; }
        public IList<Coordinates> Mountains { get; set; }
        public IDictionary<Coordinates, int> Treasures { get; set; }
        public bool IsDone { get; set; } = false;

        public void Build(IList<string> mapElements)
        {
            var builder = new StringBuilder();
            string dimensions = mapElements.FirstOrDefault(line => line.StartsWith('C'));
            IList<String> mountainsInfos = mapElements.Where(line => line.StartsWith('M')).ToList();
            IList<String> treasuresInfos = mapElements.Where(line => line.StartsWith('T')).ToList();
            IList<String> adventurersInfos = mapElements.Where(line => line.StartsWith('A')).ToList();

            Dimensions = MapParserHelper.GetLimits(dimensions);
            Mountains = MapParserHelper.GetMountains(mountainsInfos);
            Treasures = MapParserHelper.GetTreasures(treasuresInfos);
            Adventurers = MapParserHelper.GetAdventurers(adventurersInfos);
        }
        internal void UpdateOneMovement()
        {
            if (Adventurers.Where(adventurer => adventurer.Directions.Count > 0).Count() > 0)
            {
                foreach (var adventurer in Adventurers)
                {
                    adventurer.Move(this);
                }
            }
            else
            {
                IsDone = true;
            }

        }
    }
}
