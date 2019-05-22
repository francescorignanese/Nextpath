using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.Sensors
{
    class  VirtualStopsSensor : IStopsSensor , ISensor
    {
        Random rnd = new Random();
        public int count=0;
        public int count_fermata = 0;
        public int people = 0;
        public bool isStop;
        public string off;
        public string on;
        


        public void SetStops(int count,int on,int off)
        { }

        public void Stop()
        {            
           
            if ((count % 18) == 0)
            {                
                isStop = true;
                count_fermata++;
            }
            else
            {
                isStop = false;
            }
           
        }

                
        public string GetCount()
        {            
            if (people == 0)
            {
                people = rnd.Next(10, 25);
            }
            count++;

            Stop();

            //Console.WriteLine(count + " " + isStop);

            if (isStop)
            {
                off = GetPeopleOff();
                on = GetPeopleOn();
                people = (people - Convert.ToInt32(off)) + Convert.ToInt32(on);
                return Convert.ToString(people);
                //return Convert.ToString((people - Convert.ToInt32(off)) + Convert.ToInt32(on));
            }
            else
            {
                on = "N.A.";
                off = "N.A.";
                return Convert.ToString(people);
            }
            
        }

        public string GetPeopleOff()
        {
            if (isStop)
            {
                int aux = rnd.Next(1, 8);
                if (people - aux <= 0)
                {
                    aux = 0;
                }
                return Convert.ToString(aux);

            }
            else
            {
                return "N.A.";
            }

        }

        public string GetPeopleOn()
        {
            if (isStop)
            {
                int aux = rnd.Next(1, 8);
                return Convert.ToString(aux);

            }
            else
            {
                return "N.A.";
            }

        }

        public string ToJson()
        {
            return "\"counting\":\"" + GetCount() + "\"," + "\"stop\":\"" + isStop + "\"," + "\"people_left\":\"" + off + "\"," + "\"people_enter\":\"" + on + "\"}";            
        }
    }
}
