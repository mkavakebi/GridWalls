using Microsoft.Xna.Framework;

namespace GridWars2
{
    class ScapeFinderEnemy: FindingEnemy
    {
        public ScapeFinderEnemy(Vector2 Position, Vector2 Velocity, Spaceship Ship)
            : base(Position, Velocity, Ship)
        {
            AddScore = 150;
            PicName=("greensquare");
        }

        public override void Update(GameTime gameTime)
        {
            ExternalVelocity = Grid.GetVectorFloat(Pos);
            base.Update( gameTime);
        }

    }
}
