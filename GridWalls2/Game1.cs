using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GridWars2
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Screen MyScreen;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //this.graphics.PreferredBackBufferWidth = 1000;
            //this.graphics.PreferredBackBufferHeight = 700;
            this.graphics.IsFullScreen = true;

        }

        protected override void Initialize()
        {
            MyScreen = new Screen(this);
            base.Initialize();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(Content.Load<Song>("sounds/Theme1"));
            MyScreen.NewGame();
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            MyScreen.Update(gameTime);
            MyScreen.CheckAll();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black  );
            spriteBatch.Begin();
            MyScreen.DrawBack(spriteBatch);
            MyScreen.DrawLight(spriteBatch);
            MyScreen.DrawObjects(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

