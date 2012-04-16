using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{
    class PictureManager
    {
        public static Game1 game;
        static Dictionary<string, Texture2D> texts = new Dictionary<string, Texture2D>();

        public static void AddTexture(string name)
        {
            if (!texts.ContainsKey(name))
                texts.Add(name, game.Content.Load<Texture2D>("pic/" + name));
        }

        public static Texture2D GetTexture(string name)
        {

            //try {
            return texts[name];// }
            //catch (Exception) { return game .Content .Load <Texture2D >("pic/pixel"); }
        }
    }
}
