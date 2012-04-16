using Microsoft.Xna.Framework;

namespace GridWars2
{
    class Generator : Enemy
    {//milad.korjani@gmail.com
        int Type = 0;
        public static Screen MyScreen;
        public Generator(Vector2 Position, int Type)
            : base(Position, Vector2.Zero)
        {
            TirToDie = 27;
            this.Type = Type;
            switch (Type)
            {
                case 0: PicName = "pinwheelgen";
                    break;
                case 1: PicName = "greensquaregen";
                    break;
            }
            Radius = 14;
        }

        public override void Update(GameTime gameTime)
        {
            if (rand.Next(300) == 0) MyScreen.AddEnemy(Type, Pos + new Vector2(rand.Next(40) - 20, rand.Next(40) - 20));
            if (rand.Next(2000) == 0)
                for (int i = 0; i < 10; i++)
                    MyScreen.AddEnemy(Type, Pos + new Vector2(rand.Next(40) - 20, rand.Next(40) - 20));

            base.Update(gameTime);
        }


    }
}
