using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace GridWars2
{
    class SoundManager
    {
        public static Game1 game;
        static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();

        public static void AddSound(string name)
        {
            if (!Sounds.ContainsKey(name))
                Sounds.Add(name, game.Content.Load<SoundEffect>("sounds/" + name));
        }

        public static SoundEffect GetSound(string name)
        {

            //try {
            return Sounds[name];// }
            //catch (Exception) { return game .Content .Load <Texture2D >("pic/pixel"); }
        }
    }
}
