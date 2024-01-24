using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            string[] cells = line.Split(",");

            logger.LogInfo($"Splitting data into array of length: {cells.Length}");

            if (cells.Length < 3)
            {
                logger.LogError($"Data is incomplete. Does not contain three components (lat, long, and name). Length of array is: {cells.Length}.");
                return null; 
            }

            double latitude = Convert.ToDouble(cells[0]);
            logger.LogInfo($"Parsing latitude: {latitude}");

            double longitude = Convert.ToDouble(cells[1]);
            logger.LogInfo($"Parsing longitude: {longitude}");

            string name = cells[2];
            logger.LogInfo($"Parsing out the name: {name}");


            logger.LogInfo($"Creating new instance of Point struct using {latitude} and {longitude}.");
            var point = new Point();
            point.Latitude = latitude;
            point.Longitude = longitude;

            var newTacoBell = new TacoBell();

            newTacoBell.Location = point;

            newTacoBell.Name = name;
            logger.LogInfo($"Successfully created {newTacoBell.Location.Latitude}, {newTacoBell.Location.Longitude}, and {newTacoBell.Name} in new Taco Bell instance.");

            return newTacoBell;
        }
    }
}
