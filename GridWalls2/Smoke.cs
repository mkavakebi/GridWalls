using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{
    class Smoke : Obj
    {
        public Smoke(Vector2 Position, Vector2 Velocity)
            : base(Position, Velocity)
        {
            PicName = "Smoke";
            Bombable = false;
            ColorFilter = Color.RosyBrown   ;
            Origin = new Vector2(Pic.Width/2 , Pic.Height /2);
            this.Pos = Position + Velocity;
            Velocity = Vector2.Zero; 

        }
        public override void Update(GameTime gameTime)
        {
            //length -= 3;
            ScaleVector = new Vector2(2,2)/2;
            //Angle = VectorAngle(Velocity) + (float)Math.PI;
            //Velocity *= Break;
            if (ColorFilter.A > 10) ColorFilter.A -=3;//=(byte )( ColorFilter.A/2);  
            else ColorFilter.A = 0;

            if (ColorFilter.A == 0 ) kill();
            base.Update(gameTime);
        }
    }
}
