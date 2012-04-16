using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GridWars2
{
    class ParticleManager
    {
        public static List<Obj> Objs;
        public ParticleManager() { }
        public static void Explode(Vector2 Pos, int Speed, int Number, byte TimeUnit, float Break)
        {
            for (int i = 0; i < Number; i++)
            {
                Vector2 Velo = Obj.RotateVector(new Vector2(0, 10), (float)(Math.PI * 2 * Obj.rand.Next(1000) / 1000));
                Particle P = new Particle(Pos, Velo * Speed * (float)(((float)Obj.rand.Next(100) / 200)+0.5f), TimeUnit, Break);
                Objs.Add(P);
            }
        }

        public static void Smoking(Vector2 Pos, int Speed, int Number, byte TimeUnit, float Break)
        {
            for (int i = 0; i < Number; i++)
            {
                Vector2 Velo = Obj.RotateVector(new Vector2(0, 3), (float)(Math.PI * 2 * Obj.rand.Next(1000) / 1000));
                Smoke P = new Smoke (Pos, Velo * Speed * (float)(((float)Obj.rand.Next(100) / 200) + 0.5f));
                Objs.Add(P);
            }
        }
    }
}
