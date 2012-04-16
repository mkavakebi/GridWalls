using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GridWars2
{
    public class Screen
    {
        public static int Bombing = 0;
        Overlap MyOverlap;
        TimeSpan MyTime, BombTime;
        Random rand = new Random();
        Spaceship MySpaceShip;
        List<Obj> objects = new List<Obj>();
        public Boolean Doshoot = false;
        Grid grid1;
        Game1 game;
        Texture2D BackGnd;
        GameTime mygmtime = new GameTime();

        public Screen(Game1 game)
        {
            this.game = game; MyTime = TimeSpan.FromMilliseconds(500);

            MyOverlap = new Overlap(game);
            Obj.MyOverLap = MyOverlap;
            PictureManager.game = game;
            BackGnd = game.Content.Load<Texture2D>("pic/Layer0_0");
            SoundManager.game = game;
            Random rand = new Random();
            Obj.game = game;
            Obj.ScreenHeight = game.Window.ClientBounds.Height;
            Obj.ScreenWidth = game.Window.ClientBounds.Width;
            Generator.MyScreen = this;
        }

        public void NewGame()
        {
            objects = new List<Obj>();
            grid1 = new Grid(Obj.ScreenWidth, Obj.ScreenHeight, game);
            MySpaceShip = new Spaceship(new Vector2(500, 500));
            MySpaceShip.ShotTime = MyTime;
            objects.Add(MySpaceShip);
            ParticleManager.Objs = objects;
        }

        public void Do_Game(GameTime Gametime)
        {
            //return;
            if (Keyboard.GetState().IsKeyDown(Keys.S)) AddEnemy(6);
            if (Keyboard.GetState().IsKeyDown(Keys.G)) AddEnemy(7);
            if (Keyboard.GetState().IsKeyDown(Keys.B)) AddEnemy(5);
            if (Keyboard.GetState().IsKeyDown(Keys.Z)) AddEnemy(4);


            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1 )) AddBonus(1);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2)) AddBonus(2);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3)) AddBonus(3);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4)) AddBonus(4);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad5)) AddBonus(5);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6)) AddBonus(6);
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8)) AddBonus(8);


            int TimeZarib;
                       if (objects.Count > 7 || objects.Count == 0)
            TimeZarib = (int)(1 + objects.Count / 30);// * (int)Gametime.ElapsedRealTime.Ticks;
                       else 
                           TimeZarib=50;

            if (BombTime > TimeSpan.Zero) return;
            /*if (rand.Next(1 * TimeZarib) == 0 && MySpaceShip.Score > -1) AddEnemy(0);
            if (rand.Next(1 * TimeZarib) == 0 && MySpaceShip.Score > 20) AddEnemy(1);
            if (rand.Next(3 * TimeZarib) == 0 && MySpaceShip.Score > 700) AddEnemy(2);
            if (rand.Next(4 * TimeZarib) == 0 && MySpaceShip.Score > 1000) AddEnemy(3);
            if (rand.Next(5 * TimeZarib) == 0 && MySpaceShip.Score > 1200) AddEnemy(4);
            if (rand.Next(6 * TimeZarib) == 0 && MySpaceShip.Score > 1500) AddEnemy(5);
            if (rand.Next(6 * TimeZarib) == 0 && MySpaceShip.Score > 1500) AddEnemy(6);
            if (rand.Next(7 * TimeZarib) == 0 && MySpaceShip.Score > 1000) AddEnemy(7);
            if (rand.Next(8 * TimeZarib) == 0 && MySpaceShip.Score > 100) AddEnemy(8);

            if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(8);
            //if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(7);
            if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(6);
            if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(5);
            if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(4);
            if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(3);
            if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(2);
            if (rand.Next(25 * TimeZarib) == 0 && MySpaceShip.Score > 200) AddBonus(1);*/
            //if (rand.Next(1000) == 0) AddEnemy(9);
            //int i;
            //for (i = 1; i < 2; i++)
            //  for (int j = 0; j < 10; j++)
            //    if (j != 6 && j != 7)
            //      AddEnemy(j);
        }

        public void Update(GameTime gameTime)
        {
            Do_Game(gameTime);
            mygmtime = gameTime;
            if (BombTime > TimeSpan.Zero) BombTime -= gameTime.ElapsedGameTime;

            MyTime -= gameTime.ElapsedGameTime;
            if (MySpaceShip.IsShooting && MySpaceShip.Alive && MyTime <= TimeSpan.Zero)
            {
                MyTime = TimeSpan.FromMilliseconds(100);
                MySpaceShip.Shoot(objects);
            }

            grid1.Finish();
            for (int i = 0; i < objects.Count; i++)
                objects[i].Move(gameTime);

            //grid1.Update(gameTime);
            if (Bombing > 0) Bomb();

        }
        public void CheckAll()
        {


            for (int i = objects.Count - 1; i >= 0; i--)
                for (int j = objects.Count - 1; j >= 0; j--)
                {
                    Obj o1 = objects[i];
                    Obj o2 = objects[j];
                    if (o1.Alive && o2.Alive)
                        if (((Vector2)(o1.Next - o2.Next)).Length() < o2.Radius + o1.Radius)
                        {
                            if (o1.GetType() == o2.GetType() && !(o2 is SnakeTail) && o1 != o2)
                            {
                                Obj.BallBounce(ref o1.Velocity, ref o2.Velocity);
                                Obj.ColisionFixer(ref o1.Pos, ref o2.Pos, o1.Radius, o2.Radius);
                            }
                            else
                            {
                                o1.Contact(o2);
                                o2.Contact(o1);
                            }
                        }
                }
            for (int i = objects.Count - 1; i >= 0; i--)
                if (!objects[i].Alive)
                {
                    if (objects[i] is BrakeFinderEnemy)
                        for (int t = 0; t < 3; t++)
                            AddEnemy(222, objects[i].Pos + new Vector2(rand.Next(100) - 50, rand.Next(100) - 50));

                    objects.RemoveAt(i);
                }

        }

        public void killAboutAll()
        {
            for (int i = objects.Count - 1; i >= 0; i--)
                if (objects[i].Bombable)
                {
                    if (objects[i] is Enemy) ((Enemy)objects[i]).TirToDie = 1;
                    objects[i].kill();
                }
        }

        public void Bomb()
        {

            Grid.AddForce(new Vector2(Obj.ScreenWidth / 2, Obj.ScreenHeight / 2), 1000, -10200, "Bomb");
            if (Bombing == 1)
            {
                BombTime = TimeSpan.FromMilliseconds(1500);
                killAboutAll();
            }
            Bombing = 2;
            if (BombTime < TimeSpan.Zero) Bombing = 3;
        }

        public void DrawBack(SpriteBatch sp)
        {
            // sp.Draw(BackGnd, new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);     
            grid1.Draw(sp);
            Obj.MyOverLap.Draw(sp);
            SpriteFont myfont = game.Content.Load<SpriteFont>("Hud");
            sp.DrawString(myfont, objects.Count.ToString(), new Vector2(10, 30), Color.Firebrick);
            sp.DrawString(myfont, "kk"/*mygmtime.ElapsedRealTime.Ticks.ToString()*/, new Vector2(10, 50), Color.Firebrick);
        }

        public void DrawLight(SpriteBatch sp)
        {
        }

        public void DrawObjects(SpriteBatch sp)
        {
            foreach (Obj o1 in objects)
                if (o1.Visible)
                    o1.Draw(sp);
        }

        public void AddEnemy(int type) { AddEnemy(type, GetValidPos(rand.Next(0, 4))); }

        public void AddBonus(int type) { AddBonus(type, GetValidPos(rand.Next(0, 4))); }

        public void AddEnemy(int type, Vector2 position)
        {
            if (mygmtime.IsRunningSlowly) return;

            switch (type)
            {
                case 0: objects.Add(new Enemy(position, new Vector2((float)(rand.Next(30) - 15) / 10, (float)(rand.Next(30) - 15) / 10)));
                    break;
                case 1: objects.Add(new FindingEnemy(position, new Vector2(1, 1), MySpaceShip));
                    break;
                case 2: objects.Add(new BrakeFinderEnemy(position, new Vector2(1, 1), MySpaceShip));
                    break;
                case 222: objects.Add(new BrakeFinderMiniEnemy(position, new Vector2(1, 1), MySpaceShip));
                    break;
                case 3: objects.Add(new BlueCircleEnemy(position, new Vector2(1, 1), MySpaceShip));
                    break;
                case 4: objects.Add(new ScapeFinderEnemy(position, new Vector2(1, 1), MySpaceShip));
                    break;
                case 5: objects.Add(new BlackHoleEnemy(position, new Vector2(0.2f, 0.2f)));
                    break;
                case 6: MakeSnake(40, position);
                    break;
                case 7: objects.Add(new Generator(position, 0));
                    break;
                case 8: objects.Add(new Generator(position, 1));
                    break;
            }
        }

        public void AddBonus(int type, Vector2 position)
        {
            if (mygmtime.IsRunningSlowly) return;

            objects.Add(new Bonus(position, new Vector2((float)(rand.Next(30) - 15) / 10, (float)(rand.Next(30) - 15) / 10), type));

        }

        public void MakeSnake(int TailCount, Vector2 position)
        {
            objects.Add(new SnakeEnemy(position, Obj.RotateVector(Vector2.One, rand.Next(1000))));
            SnakeTail st;
            st = new SnakeTail(position);
            st.Before = objects.Last<Obj>();
            st.Number = 0;
            st.TaleCount = TailCount;
            objects.Add(st);
            for (int i = 1; i < TailCount; i++)
            {
                st = new SnakeTail(position + new Vector2(i));
                st.Before = objects.Last<Obj>();
                st.Number = i;
                st.TaleCount = TailCount;
                objects.Add(st);
            }

        }

        public Vector2 GetValidPos(int code)
        {
            Vector2 pos = Vector2.Zero;
            int Bound = 80;
            float Alpha = 0;
            float r = 0;
            do
            {

                switch (code)
                {
                    case 0:
                        pos = new Vector2(Obj.ScreenWidth / 2, Obj.ScreenHeight / 2);
                        Alpha = ((float)rand.Next(1000) / 1000) * (float)(Math.PI * 2);
                        r = (float)(Obj.ScreenWidth - 2 * Bound) * (float)rand.Next(1000) / 1000;
                        break;
                    case 1:
                        pos = new Vector2(Bound, Bound);
                        Alpha = ((float)rand.Next(1000) / 1000) * (float)(Math.PI / 2);
                        r = (float)(Obj.ScreenWidth / 4) * (float)rand.Next(1000) / 1000;
                        break;
                    case 2:
                        pos = new Vector2(Bound, Obj.ScreenHeight - Bound);
                        Alpha = (float)(Math.PI / 2) + ((float)rand.Next(1000) / 1000) * (float)(Math.PI / 2);
                        r = (float)(Obj.ScreenWidth / 4) * (float)rand.Next(1000) / 1000;
                        break;
                    case 3:
                        pos = new Vector2(Obj.ScreenWidth - Bound, Obj.ScreenHeight - Bound);
                        Alpha = (float)(Math.PI) + ((float)rand.Next(1000) / 1000) * (float)(Math.PI / 2);
                        r = (float)(Obj.ScreenWidth / 4) * (float)rand.Next(1000) / 1000;
                        break;
                    case 4:
                        pos = new Vector2(Obj.ScreenWidth - Bound, Bound);
                        Alpha = -(float)(Math.PI / 2) + ((float)rand.Next(1000) / 1000) * (float)(Math.PI / 2);
                        r = (float)(Obj.ScreenWidth / 4) * (float)rand.Next(1000) / 1000;
                        break;

                }
                pos += Obj.RotateVector(new Vector2(0, r), Alpha);
            } while (!IsPosValid(pos));

            return pos;
        }

        public Boolean IsPosValid(Vector2 Position)
        {
            if (((Vector2)(Position - MySpaceShip.Pos)).LengthSquared() < 10000) return false;
            if (Position.X < 75 || Position.X > Obj.ScreenWidth - 75) return false;
            if (Position.Y < 75 || Position.Y > Obj.ScreenHeight - 75) return false;
            return true;
        }
    }
}
