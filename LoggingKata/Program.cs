using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);
            if(lines.Length == 0)
            {
                logger.LogError($"Error: CSV file has {lines.Length} lines of data.");
            }
            else if (lines.Length == 1)
            {
                logger.LogWarning($"Warning: CSV file has only {lines.Length} line of data.");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            double distanceBetween;
            double currentHighestDistance = 0.0;

            for(int i = 0; i<locations.Length; i++)
            {
                var locA = locations[i];
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);
                for(int x = 0; x<locations.Length; x++)
                {
                    var locB = locations[x];
                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);
                    distanceBetween = corA.GetDistanceTo(corB);
                    if (distanceBetween > currentHighestDistance)
                    {
                        currentHighestDistance = distanceBetween;
                        tacoBell1 = locations[i];
                        tacoBell2 = locations[x];
                    }
                }
            }

            Console.WriteLine("The two Taco Bells furthest from one another are as follows...");
            Console.WriteLine($"The Taco Bell called {tacoBell1.Name} at Latitude: {tacoBell1.Location.Latitude} and Longitude: {tacoBell1.Location.Longitude}");
            Console.WriteLine($"The Taco Bell called {tacoBell2.Name} at Latitude {tacoBell2.Location.Latitude} and Longitude: {tacoBell2.Location.Longitude}");
            Console.WriteLine($"These two Taco Bells have a total distance of {currentHighestDistance} meters from one another."); 
        }
    }
}
