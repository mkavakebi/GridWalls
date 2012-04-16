using Microsoft.Xna.Framework;

namespace GridWars2
{

    class FindingEnemy : Enemy
    {
        public float Speed=1;
        public Spaceship Spship;
        public FindingEnemy(Vector2 Position, Vector2 Velocity, Spaceship Ship)
            : base(Position, Velocity)
        {
            AddScore = 30;
            PicName="bluediamond";
            this.Spship = Ship;
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = FindTheWay(Spship) * Speed ;
            base.Update( gameTime);
        }
        

    }
}
