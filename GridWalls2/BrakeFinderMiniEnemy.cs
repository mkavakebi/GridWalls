using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{
    class BrakeFinderMiniEnemy : FindingEnemy
    {
        Vector2 Circular_Move = new Vector2(3, 0);  
        public BrakeFinderMiniEnemy(Vector2 Position, Vector2 Velocity, Spaceship Ship)
            : base(Position, Velocity, Ship)
        {

            AddScore = 1; 
            PicName=("purplesquare2");
            Radius = 12;
        }
        public override void Update(GameTime gameTime)
        {
            Circular_Move =RotateVector(Circular_Move, 0.1f);
            ExternalVelocity = Circular_Move;
            base.Update( gameTime);
        } 
    }
}
