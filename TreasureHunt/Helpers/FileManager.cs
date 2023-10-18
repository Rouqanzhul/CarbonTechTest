namespace TreasureHunt.Helpers
{
    public static class FileManager
    {
        public static IList<string> ReadFile(string inputFilePath)
        {
            List<string> fileElements = new List<string>();

            if (File.Exists(inputFilePath))
            {
                using (StreamReader streamReader = File.OpenText(inputFilePath))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        fileElements.Add(line);
                    }
                }
            }
            return fileElements;
        }

        internal static void OutputResult(Map map, string outputFilePath)
        {
            using (StreamWriter outputFile = new StreamWriter(outputFilePath))
            {
                outputFile.WriteLine($"C - {map.Dimensions.X} - {map.Dimensions.Y}");

                foreach(var mountain in map.Mountains)
                {
                    outputFile.WriteLine($"M - {mountain.X} - {mountain.Y}");
                }
                foreach (var treasure in map.Treasures)
                {
                    outputFile.WriteLine($"T - {treasure.Key.X} - {treasure.Key.Y} - {treasure.Value}");
                }
                foreach (var adventurer in map.Adventurers)
                {
                    outputFile.WriteLine($"A - {adventurer.Name} - {adventurer.Coordinates.X} - {adventurer.Coordinates.Y} - {adventurer.Orientation} - {adventurer.TreasuresCollected}");
                }
            }
        }
    }
}
