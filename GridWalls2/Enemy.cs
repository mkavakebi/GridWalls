using Microsoft.Xna.Framework;

namespace GridWars2
{
    class Enemy : Obj
    {
        public int TirToDie = 1;
        public int AddScore;
        public float Angle_Acceleration = 0.1f;
        public Enemy(Vector2 Position, Vector2 Velocity)
            : base(Position, Velocity)
        {
            AddScore = 10;
            Radius = 17;
            PicName = "pinkpinwheel";
        }
        public override void Update(GameTime gameTime)
        {
            if (Gravitable)
            {
                if (!(this is BlackHoleEnemy))
                    if (this is ScapeFinderEnemy) ExternalVelocity += Grid.GetVectorFloat(Pos);
                    else if (!(this is SnakeEnemy)) ExternalVelocity += Grid.GetBlkHoleVector(Pos);
            }
            Angle += Angle_Acceleration;
            base.Update(gameTime);
        }
        public override void Contact(Obj O)
        {
            
            if (O is Tir && Alive) { kill(); O.kill(); (O as Tir ).SpShip.Score += AddScore; }
            if (O is BlackHoleEnemy && !(this is BlackHoleEnemy)) { (O as BlackHoleEnemy).Fallen(this); }
            base.Contact(O);

        }
        public override void kill()
        {
            TirToDie--;
            if (TirToDie <= 0)
                base.kill();
        }
    }
}
