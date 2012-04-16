using Microsoft.Xna.Framework;

namespace GridWars2
{
    class BlueCircleEnemy : FindingEnemy
    {
        public BlueCircleEnemy(Vector2 Position, Vector2 Velocity, Spaceship Ship)
            : base(Position, Velocity, Ship)
        {
            PicName=("bluecircle");
            Speed = 3;
            Radius = 14;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update( gameTime);
        }

    }
}
