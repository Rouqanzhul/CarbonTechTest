using System.IO;
using System;
using TreasureHunt.Helpers;

namespace TreasureHunt
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string scurrentdirectory = AppDomain.CurrentDomain.BaseDirectory;
            
            string inputDirectory = System.IO.Path.GetFullPath(@"..\..\..\Inputs");


            foreach (var input in Directory.GetFiles(inputDirectory))
            {
                //transform file into map
                var fileLines = FileManager.ReadFile(input);
                var map = new Map();
                map.Build(fileLines);

                //run map as long as it is good
                while (!map.IsDone)
                {
                    map.UpdateOneMovement();
                };
                FileManager.OutputResult(map, input.Replace("Inputs", "Outputs").Replace(".txt", ".result.txt"));
            }
        }
    }
}