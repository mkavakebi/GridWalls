using Microsoft.Xna.Framework;

namespace GridWars2
{
    class BrakeFinderEnemy : FindingEnemy
    {
        public BrakeFinderEnemy(Vector2 Position, Vector2 Velocity, Spaceship Ship)
            : base(Position, Velocity, Ship)
        {
            AddScore = 10; 
            PicName=("purplesquare1");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update( gameTime);
        }

    }
}
