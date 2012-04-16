using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{
    class Grid
    {
        Game1 game;
        static public Vector2[,] TirField;
        static public Vector2[,] BlkField;
        static public int step = 25;
        float Width, Height;
        Texture2D t1;
        public Grid(float Width, float Height, Game1 game)
        {
            this.game = game;
            this.Width = Width;
            this.Height = Height;
            TirField = new Vector2[(int)(Width / step) + 1, (int)(Height / step) + 1];
            BlkField = new Vector2[(int)(Width / step) + 1, (int)(Height / step) + 1];
            t1 = game.Content.Load<Texture2D>("pic/mypixel");
        }
        //Vector2 L = new Vector2(100, 0);
        public void Draw(SpriteBatch sp)
        {
            return;
            for (int x = 0; x < Width / step; x++)
                for (int y = 0; y < Height / step; y++)
                {
                    DrawLine(GetEndPoint(x, y), GetEndPoint(x, y + 1), sp);
                    DrawLine(GetEndPoint(x, y), GetEndPoint(x + 1, y), sp);
                    //DrawLine(GetMeanEndPoint(x, y), GetMeanEndPoint(x, y + 1), sp);
                    //DrawLine(GetMeanEndPoint(x, y), GetMeanEndPoint(x + 1, y), sp);
                }
            //L = Obj.RotateVector(L, 0.3f);
            //DrawLine(new Vector2(300, 300), L + new Vector2(300, 300), sp);
        }
        void DrawLine(Vector2 A, Vector2 B, SpriteBatch sp)
        {
            Vector2 D = B - A;
            float tet = Obj.VectorAngle(D);
            sp.Draw(t1, A, new Rectangle(0, 0, t1.Width, t1.Height), Color.White, tet - (float)Math.PI / 2, new Vector2(1, 0), new Vector2(1, D.Length()), SpriteEffects.None, 0f);
        }
        public void Update(GameTime gameTime)
        {
            ZeroBounds();
            for (int x = 0; x < Width / step; x++)
                for (int y = 0; y < Height / step; y++)
                {
                    TirField[x, y] = TirField[x, y] * 0.95f;
                    // BlkField[x, y] = BlkField[x, y] * 0.95f;
                }
        }

        public void Finish()
        {
            BlkField = new Vector2[(int)(Width / step) + 1, (int)(Height / step) + 1];
        }

        Vector2 GetEndPoint(int i, int j)
        { return new Vector2(i * step, j * step) + GetVector(i, j); }

        static Vector2 GetVector(int i, int j)
        {
            Vector2 l;
            try { l = TirField[i, j] + BlkField[i, j]; }
            catch (Exception)
            { l = Vector2.Zero; }
            return l;
        }

        public static Vector2 GetBlkHoleVector(Vector2 P)
        {
            Vector2 l;
            try { l = BlkField[(int)(P.X / step), (int)(P.Y / step)]; }
            catch (Exception)
            { l = Vector2.Zero; }
            return l / 20;
        }
        public static Vector2 GetBlkSpaceVector(Vector2 P)
        {
            Vector2 l;
            try { l = TirField[(int)(P.X / step), (int)(P.Y / step)]; }
            catch (Exception)
            { l = Vector2.Zero; }
            return l;
        }

        public static Vector2 GetVectorFloat(Vector2 P)
        {
            return GetVector((int)(P.X / step), (int)(P.Y / step));
        }



        Vector2 GetMeanEndPoint(int i, int j)
        { return new Vector2(i * step, j * step) + GetVector(i, j) + GetVector(i + 1, j) + GetVector(i, j + 1) + GetVector(i, j - 1) + GetVector(i - 1, j); }// +GetVector(i - 1, j - 1) + GetVector(i + 1, j + 1) + GetVector(i - 1, j + 1) + GetVector(i + 1, j - 1); }

        void ZeroBounds()
        {
            for (int i = 0; i < TirField.GetLongLength(0); i++)
            { TirField[i, 0] = Vector2.Zero; TirField[i, TirField.GetLength(1) - 1] = Vector2.Zero; }

            for (int i = 0; i < TirField.GetLongLength(1); i++)
            { TirField[0, i] = Vector2.Zero; TirField[TirField.GetLength(0) - 1, i] = Vector2.Zero; }
        }
        public static void AddForce(Vector2 pos, float R, float Zarib, string Object)
        {
            return;
            double pow = 1;
            for (int x = (int)((pos.X - R) / step); x < (pos.X + R) / step; x++)
                for (int y = (int)((pos.Y - R) / step) - 1; y < (pos.Y + R) / step; y++)
                {
                    Vector2 D = new Vector2(x * step, y * step) - pos;
                    if (D.LengthSquared() <= R * R)
                        if ((x > 0 && x < TirField.GetLength(0)) && (y > 0 && y < TirField.GetLength(1)))
                        {
                            float f = 0;
                            float l = D.Length() + 0.01f;
                            if (Object == "BlackHole")
                            {
                                if (l > 30) f = (float)(1 / ((Math.Pow((l - 30) + 10, pow))));
                                //if (D.Length() <= 40) f = (float)0;
                                //if (f >= l * 0.95) f = l / 2;
                                //BlkField[x, y] = -D *10*(float)Math.Log((double)l) / l; //* f;
                                //f = (float)(Zarib / (Math.Pow(D.Length(), pow+2)));

                                if (l <= 50)
                                    BlkField[x, y] = (20 - l) * D / l;
                                else if (l > 50)
                                {
                                    f = (R - l) / 2;
                                    BlkField[x, y] += -D * (f) / l;
                                }
                            }
                            if (Object == "Tir" || Object == "Bomb")
                            {
                                f = (float)(Zarib / (10 + Math.Pow((D.Length()), pow)));
                                TirField[x, y] += D * f / l;
                            }
                        }
                }
        }
    }
}
