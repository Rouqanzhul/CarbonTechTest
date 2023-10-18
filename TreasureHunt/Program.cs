using TreasureHunt.Helpers;

namespace TreasureHunt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = "C:\\Users\\smoug\\source\\repos\\CarbonTechTest\\TreasureHunt\\Inputs\\1.txt";
            var output = "C:\\Users\\smoug\\source\\repos\\CarbonTechTest\\TreasureHunt\\Outputs\\1.result.txt";

            //transform file into map
            var fileLines = FileManager.ReadFile(input);
            var map = new Map();
            map.Build(fileLines);

            //run map as long as it is good
            while (!map.IsDone)
            {
                map.UpdateOneMovement();
            };
            FileManager.OutputResult(map, output);
        }
    }
}