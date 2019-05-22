using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.Sensors
{
    interface ICoordinatesSensor
    {
        void SetCoordinates(double lat, double longit);
        double GetLatitude();
        double GetLongitude();
    }
}
