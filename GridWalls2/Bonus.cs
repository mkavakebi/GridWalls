using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GridWars2
{
    class Bonus : Obj
    {
        Texture2D Effect;
        public int code;
        float Rot;
        public Bonus(Vector2 Position, Vector2 Velocity, int code)
            : base(Position, Velocity)
        {
            this.code = code;
            Effect = game.Content.Load<Texture2D>("pic/whitestar");
            PicName = "powerups";
            Radius = 40;
            Bombable = false;
        }
        public override void Draw(SpriteBatch sp)
        {
            Rot -= 0.1f;
            sp.Draw(Effect, Pos, new Rectangle(0, 0, Effect.Width, Effect.Height), Color.White, Rot, new Vector2(Effect.Width / 2, Effect.Height / 2), 1, SpriteEffects.None, 0);

            Rectangle rect;
            rect = new Rectangle((code % 2) * Pic.Width / 2, (int)(code / 2) * Pic.Height / 8, Pic.Width / 2, Pic.Height / 8);
            sp.Draw(Pic, Pos, rect, Color.White, 0, new Vector2(rect.Width / 2, rect.Height / 2), 1, SpriteEffects.None, 0);
            //base.Draw(sp);
        }

    }
}
