using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Transactions;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";
        const double MetersToMiles = 0.00062137; 

        static void Main(string[] args)
        {
            // Objective: Find the two Taco Bells that are the farthest apart from one another.
            // Some of the TODO's are done for you to get you started. 

            logger.LogInfo("Log initialized");

            // Use File.ReadAllLines(path) to grab all the lines from your csv file. 
            // Optional: Log an error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            // This will display the first item in your lines array
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            var locations = lines.Select(parser.Parse).ToArray();

  
            // Complete the Parse method in TacoParser class first and then START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. 
            // These will be used to store your two Taco Bells that are the farthest from each other.
            
            ITrackable tacobellOne = null;
            ITrackable tacobellTwo = null;    

            // TODO: Create a `double` variable to store the distance

            double maxDistance = 0;

            // TODO: Add the Geolocation library to enable location comparisons: using GeoCoordinatePortable;
            // Look up what methods you have access to within this library.

            // NESTED LOOPS SECTION----------------------------

            foreach (var locA in locations)
            {
                var corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);

                foreach (var locB in locations)
                {

                    if (locA == locB) continue;

                    var corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    double distance = corA.GetDistanceTo(corB);

                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        tacobellOne = locA;
                        tacobellTwo = locB;
                    }

                }

            }
            // NESTED LOOPS SECTION COMPLETE ---------------------

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            // Display these two Taco Bell locations to the console.

            Console.WriteLine($"The two farthest TacoBell locations are {tacobellOne.Name} and {tacobellTwo.Name}"); 
            Console.WriteLine($"They are roughly {Math.Round(maxDistance / 1000, 2)} km apart");
            
        }
    }
}
