using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GridWars2
{

    class SnakeEnemy : Enemy
    {
        public Boolean Enabled = true;
        public float AngAcc = 0;
        public float AngSpeed = 0;
        public SnakeEnemy(Vector2 Position, Vector2 Velocity)
            : base(Position, Velocity)
        {
            time = rand.Next(100);
            Velocity *= 10;
            SoundCreateName = "snake1";
            SoundCreate.Play();
            Angle_Acceleration = 0;
            PicName = "snakehead";
            AngAcc = 0.01f;
            Radius = 15;
            Origin = new Vector2(Pic.Width / 2, Pic.Height / 2 - Radius);
        }
        int sign = 1;
        int time = 0;
        public override void Update(GameTime gameTime)
        {
            time++;
            //AngAcc = 0; 
            AngAcc = (float)Math.Sin(time / 100) * 10;
            //if (rand.Next(80) == 0) { sign*= -1; AngSpeed *= -1; }
            //if (rand.Next(10) == 0)
            //AngAcc = sign *(float)rand.Next(1000) / 100000000;
            AngSpeed += AngAcc;
            if (AngSpeed > 0.01f) AngSpeed = 0.009f;
            if (AngSpeed < -0.01f) AngSpeed = -0.009f;

            Origin = new Vector2(31, 30);
            Angle = VectorAngle(Velocity) + (float)Math.PI / 2;
            Velocity = RotateVector(Velocity, AngSpeed);
            base.Update(gameTime);
        }
        public override void Contact(int Place)
        {
            shake = true;
            AngSpeed = -AngSpeed;
            Velocity = RotateVector(Velocity, sign * (float)Math.PI / 16);
            //base.Contact(Place);
        }

        public override void Contact(Obj O)
        {
            //if (O is SnakeEnemy && O!=this) shake = true;
            base.Contact(O);
        }

    }
}
