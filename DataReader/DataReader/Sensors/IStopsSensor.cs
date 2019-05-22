using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.Sensors
{
    interface IStopsSensor 
    {
        void SetStops(int count, int on, int off);
        string GetCount();
        void Stop();
        string GetPeopleOff();
        string GetPeopleOn();
    }
}
