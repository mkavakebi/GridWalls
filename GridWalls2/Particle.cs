using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{
    class Particle : Obj
    {
        int length = 50;
        byte TimeUnit;
        float Break;
        public Particle(Vector2 Position, Vector2 Velocity, byte TimeUnit, float Break)
            : base(Position, Velocity)
        {
            PicName = "Particle";
            Bombable = false;
            ColorFilter = new Color(rand.Next(150) + 100, rand.Next(150), rand.Next(255));
            Origin = new Vector2(0, 5);
            this.TimeUnit = TimeUnit;
            this.Break = Break;

        }
        public override void Update(GameTime gameTime)
        {
            length -= 3;
            ScaleVector = new Vector2((length + 50) / 30, 0.5f);
            Angle = VectorAngle(Velocity) + (float)Math.PI;
            Velocity *= Break;
            if (ColorFilter.A > 10) ColorFilter.A -= TimeUnit;//=(byte )( ColorFilter.A/2);  
            else ColorFilter.A = 0;

            if (ColorFilter.A == 0 || Velocity.LengthSquared()<2  ) kill();
            base.Update(gameTime);
        }
    }
}
