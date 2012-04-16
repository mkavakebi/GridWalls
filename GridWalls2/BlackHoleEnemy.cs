using System;
using Microsoft.Xna.Framework;

namespace GridWars2
{

    class BlackHoleEnemy : Enemy
    {
        
        public float Zarib = 3;
        public Boolean Enabled ;
        public BlackHoleEnemy(Vector2 Position, Vector2 Velocity)
            : base(Position, Velocity)
        {
            PicName = "redcircle";
            SoundRemoveName = "sunexp";
            Radius = 35;
            TirToDie = 20;
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
                Grid.AddForce(Next + Velocity, 20 * Zarib, 1, "BlackHole");
            base.Update(gameTime);
        }

        public void Fallen(Obj O)
        {
            if (Enabled)
            {
                O.kill();
                if (TirToDie < 25)
                    TirToDie++;
                if (Zarib < 15)
                    Zarib++;
            }
        }
        public override void kill()
        {
            Enabled = true;
            base.kill();
        }

        public override void DO_Particle()
        {
            ParticleManager.Explode(Pos, 1, 20,7,0.99f);
        }  

    }
}
