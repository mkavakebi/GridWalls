using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace GridWars2
{
    class Spaceship : Obj
    {
        private Boolean Flashing = false;
        private TimeSpan UnvisibleTime = TimeSpan.FromSeconds(7);
        public int Lives = 2;
        public int Bombs = 3;
        public int Score = 0;
        public int Health = 100;
        public TimeSpan ShotTime;
        public int ShotType = 0;
        public Boolean IsShooting = false;
        Vector2 MouseCenter = new Vector2(400, 400);
        Vector2 CurrentPos;
        TimeSpan mouseDelay = TimeSpan.FromMilliseconds(500);
        public Spaceship(Vector2 Position)
            : base(Position, new Vector2(0, 0))
        {
            SoundCreateName = "snake1";
            SoundRemoveName = "die1";
            SoundCreate.Play();
            Bombable = false;
            PicName = "whiteplayer";
            Radius = 15;
            Mouse.SetPosition((int)MouseCenter.X, (int)MouseCenter.Y);
        }
        public override void Update(GameTime gameTime)
        {
            if (Health <= 50 && Visible && Velocity.LengthSquared()>0  ) ParticleManager.Smoking(Pos , 1, 1, 15, 0.9f);
            MyOverLap.Score = Score;
            MyOverLap.Lives = Lives;
            MyOverLap.Bombs = Bombs;
            MyOverLap.Health = Health;

            mouseDelay -= gameTime.ElapsedGameTime;
            this.IsShooting = (Mouse.GetState().LeftButton == ButtonState.Pressed && Visible);

            if (Flashing)
            {
                UnvisibleTime -= gameTime.ElapsedGameTime;
                ColorFilter.A -= 10;
                if (UnvisibleTime <= TimeSpan.Zero)
                {
                    Flashing = false;
                    ColorFilter.A = 255;
                    UnvisibleTime = TimeSpan.FromSeconds(7);
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (MyOverLap.Situation == "Lose")
                {
                    game.MyScreen.NewGame();
                    Flashing = true;
                    Visible = true;
                    MyOverLap.SetSituation("");
                }
                else if (MyOverLap.Situation == "Die")
                {
                    Health = 100;
                    Flashing = true;
                    Visible = true;
                    MyOverLap.SetSituation("");
                }
                else if (Bombs > 0 && Screen.Bombing == 0)
                {
                    Bombs--;
                    Screen.Bombing = 1;
                }
            }
            else if (Screen.Bombing == 3) Screen.Bombing = 0;


            if (mouseDelay <= TimeSpan.Zero)
            {
                mouseDelay = TimeSpan.FromMilliseconds(50);
                CurrentPos.X = Mouse.GetState().X;
                CurrentPos.Y = Mouse.GetState().Y;

                Vector2 Vect = CurrentPos - MouseCenter;

                if (Velocity.LengthSquared() > 0)
                    Velocity = (Vect + Velocity) / 2;
                else
                    Velocity = Vect;
                if (Vect.LengthSquared() == 0) Velocity *= 0;
                Velocity.Normalize();
                Velocity *= 7;

            }

            if (Velocity.LengthSquared() > 0)
                Angle = VectorAngle(Velocity) - (float)(Math.PI / 2);
            Mouse.SetPosition((int)MouseCenter.X, (int)MouseCenter.Y);
        }

        public override void Contact(Obj O)
        {
            if (!Flashing && MyOverLap.Situation != "Die" && MyOverLap.Situation != "Lose")
                if (O is Enemy)
                {
                    O.kill();
                    SoundRemove.Play();
                    if (Lives > 0)
                    {

                        if (Health == 0)
                        {
                            Pos = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
                            Visible = false;
                            MyOverLap.SetSituation("Die");
                            Lives--;
                        }
                        else Health -= 10;
                    }
                    else
                    {
                        Visible = false;
                        if (Score > MyOverLap.HightScore)
                            Microsoft.Win32.Registry.SetValue("HKEY_CLASSES_ROOT", "Hight", Score);
                        MyOverLap.Situation = "Lose";
                    }   //kill();
                }

            if (O is Bonus)
            {
                if (((Bonus)O).code < 5)
                    ShotType = ((Bonus)O).code;
                else if (((Bonus)O).code == 5) Tir.TirSpeed += 5;
                else if (((Bonus)O).code == 6) Lives++;
                //else if (((Bonus)O).code==7)
                else if (((Bonus)O).code == 8) Bombs++;
                //else if (((Bonus)O).code==9)
                //else if (((Bonus)O).code==10)
                O.kill();
            }
            base.Contact(O);
        }

        public void Shoot(List<Obj> Objs)
        {
            switch (ShotType)
            {
                case 0:
                case 1: Objs.Add(new Tir(this, 0));
                    break;
                case 2:
                    Objs.Add(new Tir(this, 0));
                    Objs.Add(new Tir(this, 5));
                    break;
                case 3:
                    Objs.Add(new Tir(this, 0));
                    Objs.Add(new Tir(this, 1));
                    Objs.Add(new Tir(this, 2));
                    break;
                case 4:
                    Objs.Add(new Tir(this, 1));
                    Objs.Add(new Tir(this, 2));
                    Objs.Add(new Tir(this, 3));
                    Objs.Add(new Tir(this, 4));
                    break;
            }
        }
        public override void DO_Particle()
        {
            ParticleManager.Explode(Pos, 7, 100, 1, 0.8f);
            Grid.AddForce(Pos, 400, 50000, "Tir");
            base.DO_Particle();
        }


    }
}
