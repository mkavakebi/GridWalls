using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GridWars2
{
    class TimerManager
    {

        static Dictionary<string, TimeSpan> Timers = new Dictionary<string, TimeSpan>();

        public static void Update(GameTime gametime)
        {
            //for (int i = 0; i < Timers.Count; i++)
            //    Timers -= gametime.ElapsedGameTime;
        }

        public static void AddTimer(string name, TimeSpan Time)
        {
            if (!Timers.ContainsKey(name))
                Timers.Add(name, Time);
            else
                Timers[name] = Time;
        }

        public static TimeSpan GetTimer(string name)
        {
            return Timers[name];// }
            //catch (Exception) { return game .Content .Load <Texture2D >("pic/pixel"); }
        }

        public static Boolean CheckFinished(string name)
        {
            return (Timers[name] <= TimeSpan.Zero);
        }
    }
}
