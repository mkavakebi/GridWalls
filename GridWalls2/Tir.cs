using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace GridWars2
{
    class Tir : Obj
    {
        public static float TirSpeed = 10;
        public Spaceship SpShip;
        public static Boolean Reflectable = false;
        public Tir(Spaceship SpShip, int code)
            : base(SpShip.Pos, SpShip.Velocity)
        {
            this.SpShip = SpShip;
            SoundCreateName = "shotborn";
            SoundCreate.Play();
            Bombable = false;
            PicName = "yellowshot";
            Radius = 5;
            Origin = new Vector2(16, 7);
            float MyAngle = 0;
            switch (code)
            {

                case 0: MyAngle = 0f; break;
                case 1: MyAngle = (float)+Math.PI / 16; break;
                case 2: MyAngle = (float)-Math.PI / 16; break;
                case 3: MyAngle = (float)-Math.PI / 2; break;
                case 4: MyAngle = (float)+Math.PI / 2; break;
                case 5: MyAngle = (float)-Math.PI; break;
            }
            Angle = SpShip.Angle + MyAngle + (float)Math.PI / 2;
            Velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
            Velocity *= TirSpeed;

        }
        public override void Update(GameTime gameTime)
        {
            //if (Pos.LengthSquared() > 490000) kill();
            Angle = VectorAngle(Velocity) + (float)(Math.PI / 2);
            Grid.AddForce(Pos, 100, 70, "Tir");
            base.Update(gameTime);
        }
        public override void Contact(Obj O)
        {
            //if (O.GetType().Name.Contains("Enemy") && KillCount==0 ) { kill(); O.KillCount++; } 
            base.Contact(O);
        }
        public override void Contact(int Place)
        {
            if (!Reflectable) kill();
            base.Contact(Place);
        }

    }
}
