using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_4_Time_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle window;
        Rectangle bombSize;
        Texture2D bombTexture;
        SpriteFont timeFont;
        MouseState mouseState;
        SoundEffect explosionEffect;
        float seconds;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();

            bombSize = new Rectangle(50, 50, 700, 400);
            seconds = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("TimeFont");
            explosionEffect = Content.Load<SoundEffect>("explosion");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > 15)
            {
                seconds = 0f;
            }

            mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                seconds = 0f;
            }

            if (seconds >= 15)
            {
                explosionEffect.Play();
                seconds = 0f;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombSize, Color.White);
            _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
