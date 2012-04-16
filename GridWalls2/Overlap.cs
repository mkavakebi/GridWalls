using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GridWars2
{
    class Overlap
    {
        Game game;
        public int Score = 0;
        public int Lives = 0;
        public int Bombs = 0;
        public int HightScore;
        public int Health = 100;
        Vector2 Center;
        public Texture2D TextDie, TextLose, TextIcons, TextHealth;
        public String Situation = "";
        public static SpriteFont MyFont;
        public Overlap(Game game)
        {
            this.game = game;
            TextDie = game.Content.Load<Texture2D>("pic/you_died");
            TextLose = game.Content.Load<Texture2D>("pic/you_lose");
            TextIcons = game.Content.Load<Texture2D>("pic/icons");
            Center = new Vector2((game.Window.ClientBounds.Width - TextDie.Width) / 2, (game.Window.ClientBounds.Height - TextDie.Height) / 2);
            MyFont = game.Content.Load<SpriteFont>("Hud");
            HightScore = (int)Microsoft.Win32.Registry.GetValue("HKEY_CLASSES_ROOT", "Hight", 0);
            TextHealth = game.Content.Load<Texture2D>("pic/Capsule");
        }

        public void Draw(SpriteBatch Sp)
        {

            Sp.DrawString(MyFont, Score.ToString(), new Vector2(10, 10), Color.Green);
            switch (Situation)
            {
                case "Die": Sp.Draw(TextDie, Center, Color.White);
                    break;
                case "Lose": Sp.Draw(TextLose, Center, Color.White);
                    HightScore = (int)Microsoft.Win32.Registry.GetValue("HKEY_CLASSES_ROOT", "Hight", 0);
                    break;
            }

            Vector2 ItemPos = new Vector2(Obj.ScreenWidth - 50, TextIcons.Height / 4 + 10);
            Sp.Draw(TextIcons, ItemPos, new Rectangle(0, 0, TextIcons.Width, TextIcons.Height / 2), Color.White, 0, new Vector2(TextIcons.Width / 2, TextIcons.Height / 4), 1, SpriteEffects.None, 0);
            ItemPos -= new Vector2(TextIcons.Width / 4 + MyFont.MeasureString(Lives.ToString()).X, MyFont.MeasureString(Lives.ToString()).Y / 2);
            Sp.DrawString(MyFont, Lives.ToString(), ItemPos, Color.Yellow);

            ItemPos -= new Vector2(TextIcons.Width / 2, -MyFont.MeasureString(Lives.ToString()).Y / 2);
            Sp.Draw(TextIcons, ItemPos, new Rectangle(0, TextIcons.Height / 2, TextIcons.Width, TextIcons.Height / 2), Color.White, 0, new Vector2(TextIcons.Width / 2, TextIcons.Height / 4), 1, SpriteEffects.None, 0);
            ItemPos -= new Vector2(TextIcons.Width / 4 + MyFont.MeasureString(Bombs.ToString()).X, MyFont.MeasureString(Bombs.ToString()).Y / 2);
            Sp.DrawString(MyFont, Bombs.ToString(), ItemPos, Color.Yellow);

            Sp.DrawString(MyFont, "Hight Score: " + HightScore.ToString(), new Vector2(Obj.ScreenWidth / 2 - 50, Obj.ScreenHeight - 20), Color.Violet);

            Rectangle HealthRect=new Rectangle(0, (int)( TextHealth.Height * ((float)(100-Health) / 100)),TextHealth.Width, (int)(TextHealth.Height * ((float)Health / 100)));
            Sp.Draw(TextHealth, new Vector2(0, Obj.ScreenHeight - TextHealth.Height * ((float)Health / 100)), HealthRect, Color.White);
        }

        public void SetSituation(string sit)
        {
            this.Situation = sit;
        }

    }
}
