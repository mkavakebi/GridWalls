using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{
    abstract class Obj
    {

        public Boolean Gravitable = true;
        public Boolean shake = true;
        //public SoundEffect SoundCreate;
        public static Random rand = new Random();
        public static Overlap MyOverLap;
        public Boolean Bombable = true;
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static Game1 game;
        public Color ColorFilter = Color.White;
        public Boolean Visible = true;
        public Boolean Alive;
        public Vector2 Pos;
        public Vector2 Velocity;
        public Vector2 ExternalVelocity = Vector2.Zero;
        public int Radius;
        public float Angle;
        public Vector2 ScaleVector = new Vector2(1, 1);
        public Vector2 Origin;

        private string _PicName;
        public string PicName
        {
            set
            {
                _PicName = value; PictureManager.AddTexture(value);
                Origin = new Vector2(Pic.Width / 2, Pic.Height / 2);
            }
        }
        public Texture2D Pic { get { return PictureManager.GetTexture(_PicName); } }


        public Vector2 Next
        {
            get
            {
                if (Velocity.Length().ToString() == "NaN")
                    return Pos;
                else
                    return Pos + Velocity;
            }
        }



        private string _SoundCreateName;
        public string SoundCreateName { set { _SoundCreateName = value; SoundManager.AddSound(value); } }
        public SoundEffect SoundCreate { get { return SoundManager.GetSound(_SoundCreateName); } }

        private string _SoundRemoveName;
        public string SoundRemoveName { set { _SoundRemoveName = value; SoundManager.AddSound(value); } }
        public SoundEffect SoundRemove { get { return SoundManager.GetSound(_SoundRemoveName); } }


        public Obj() { }
        //~Obj() { Pic.Dispose(); }

        public Obj(Vector2 Position, Vector2 Velocity)
        {
            Pos = Position;
            Alive = true;
            Visible = true;
            this.Velocity = Velocity;
        }
        virtual public void Contact(Obj O) { }

        virtual public void DO_Particle() { if (Bombable) ParticleManager.Explode(Pos, 4, 4, 2, 0.9f); }
        virtual public void Contact(int Place)
        {
            switch (Place)
            {
                case 0: Velocity.X = -Math.Abs(Velocity.X); Pos.X = ScreenWidth - Radius; break;
                case 1: Velocity.X = Math.Abs(Velocity.X); Pos.X = Radius; break;
                case 2: Velocity.Y = -Math.Abs(Velocity.Y); Pos.Y = ScreenHeight - Radius; break;
                case 3: Velocity.Y = Math.Abs(Velocity.Y); Pos.Y = Radius; break;
            }
        }
        virtual public void Update(GameTime gameTime) { }

        virtual public void kill() { Alive = false; DO_Particle(); }

        public void Move(GameTime gameTime)
        {
            Vector2 NextPos = Next + ExternalVelocity;
            if (NextPos.X + Radius > ScreenWidth) Contact(0);
            if (NextPos.X < Radius) Contact(1);
            if (NextPos.Y + Radius > ScreenHeight) Contact(2);
            if (NextPos.Y < Radius) Contact(3);
            Update(gameTime);
            if (Velocity.LengthSquared() > 0)
                Pos += Velocity + ExternalVelocity;
            ExternalVelocity = Vector2.Zero;
        }

        public Vector2 FindTheWay(Obj O)
        {
            Vector2 RET = O.Pos - Pos;
            RET.Normalize();
            return RET;
        }

        virtual public void Draw(SpriteBatch sp)
        {
            sp.Draw(Pic, Pos, new Rectangle(0, 0, Pic.Width, Pic.Height), ColorFilter, Angle, Origin, ScaleVector, SpriteEffects.None, 1f);
        }
        public static Vector2 RotateVector(Vector2 v, float Tetha)
        {
            Vector2 v2;
            v2.X = (float)(Math.Cos(Tetha) * v.X - Math.Sin(Tetha) * v.Y);
            v2.Y = (float)(Math.Sin(Tetha) * v.X + Math.Cos(Tetha) * v.Y);
            return v2;
        }
        public static void BallBounce(ref Vector2 v3, ref Vector2 v4)
        {
            Vector2 D = new Vector2();
            D = v4 - v3;
            Vector2 a = Obj.RotateVector(v3, -VectorAngle(D));
            Vector2 b = Obj.RotateVector(v4, -VectorAngle(D));
            float xx = a.X;
            a.X = b.X;
            b.X = xx;
            v3 = Obj.RotateVector(a, VectorAngle(D));
            v4 = Obj.RotateVector(b, VectorAngle(D));
        }

        public static float VectorAngle(Vector2 V)
        {
            if (V.Length() > 0)
                return (float)(Math.Atan2(V.Y, V.X));
            return 0;
        }

        public static void ColisionFixer(ref Vector2 A, ref Vector2 B, float Ar, float Br)
        {
            Vector2 d = A - B;
            float l = d.Length();
            d.Normalize();
            A += d * (Ar - l / 2);
            B -= d * (Br - l / 2);
        }
        public void SetLength(ref Vector2 V, float L)
        {
            V *= L / V.Length();
        }
    }
}