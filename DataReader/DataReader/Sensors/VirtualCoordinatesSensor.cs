using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.Sensors
{
    class VirtualCoordinatesSensor : ICoordinatesSensor, ISensor
    {
        public void SetCoordinates(double lat, double longit)
        { }

        public double GetLatitude()
        {
            double x0 = 45.0;
            double y0 = 23.9;
            int radius = 100000;

            Random random = new Random();

            // Convert radius from meters to degrees
            double radiusInDegrees = radius / 111000f;

            double u = random.NextDouble();
            double v = random.NextDouble();
            double w = radiusInDegrees * Math.Sqrt(u);
            double t = 2 * Math.PI * v;
            double x = w * Math.Cos(t);
            double y = w * Math.Sin(t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            double new_x = x / Math.Cos(Math.PI * y0 / 180.0);

            double foundLongitude = new_x + x0;
            double foundLatitude = y + y0;
            //Console.Write("Longitude: " + foundLongitude + "  Latitude: " + foundLatitude);
            return foundLatitude;
        }

        public double GetLongitude()
        {
            double x0 = 45.0;
            double y0 = 23.9;
            int radius = 100000;

            Random random = new Random();

            //Convert radius from meters to degrees
            double radiusInDegrees = radius / 111000f;

            double u = random.NextDouble();
            double v = random.NextDouble();
            double w = radiusInDegrees * Math.Sqrt(u);
            double t = 2 * Math.PI * v;
            double x = w * Math.Cos(t);
            double y = w * Math.Sin(t);

            // Adjust the x-coordinate for the shrinking of the east-west distances
            double new_x = x / Math.Cos(Math.PI * y0 / 180.0);

            double foundLongitude = new_x + x0;
            double foundLatitude = y + y0;
            //Console.Write("Longitude: " + foundLongitude + "  Latitude: " + foundLatitude);
            return foundLongitude;
        }

        public string ToJson()
        {
            return "\"latitude\":\"" + GetLatitude() + "\"," + "\"longitude\":\"" + GetLongitude() + "\",";

        }
    }
}

