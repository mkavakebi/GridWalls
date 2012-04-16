using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{

    class SnakeTail : Obj
    {
        private TimeSpan KillTimer;
        public int Number = 0;
        public int TaleCount = 1;
        public Obj Before;
        public SnakeTail(Vector2 Position)
            : base(Position, new Vector2(0.1f))
        {
            PicName = "snaketail";
            Radius = 10;
            KillTimer = TimeSpan.FromMilliseconds(100);
        }

        public override void Update(GameTime gameTime)
        {
            //Obj.BallBounce(ref this.Velocity, ref Before.Velocity);
            //Obj.ColisionFixer(ref Pos, ref Before.Pos, Radius, Before.Radius);
            if (!Before.Alive)
            {
                Velocity = Vector2.Zero;   
                KillTimer -= gameTime.ElapsedGameTime;
                if (KillTimer <= TimeSpan.Zero) kill();
            }
            else
            {
                Vector2 D = Before.Pos - Pos;
                Vector2 Vb = RotateVector(Before.Velocity, -VectorAngle(D));
                Velocity.X = Vb.X;
                Velocity.Y = 0;
                Velocity = RotateVector(Velocity, VectorAngle(D));
                //it makes them move rapidly//////////////////////////////////////////// 
                if (Before.shake)///////////////////////////////////////////////////////
                {
                    Before.shake = false;
                    shake = true;
                    Pos = Before.Pos - (20 * Before.Velocity / Before.Velocity.Length());
                }
                //////////////////////////////////////////////////////////////////////// 
                Angle = VectorAngle(D) + (float)Math.PI / 2;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sp)
        {
            int code = 23 - (int)(24 * Number / TaleCount);
            Rectangle rect;
            rect = new Rectangle((code % 8) * Pic.Width / 8, (int)(code / 8) * Pic.Height / 3, Pic.Width / 8, Pic.Height / 3);
            sp.Draw(Pic, Pos, rect, Color.White, Angle, new Vector2(rect.Width / 2, rect.Height / 2), 1, SpriteEffects.None, 1f);

            //base.Draw(sp);
        }
        public override void Contact(Obj O)
        {
            if (O is Tir) O.kill();
            base.Contact(O);
        }
        public override void Contact(int Place)
        {
            //base.Contact(Place);
        }

    }
}