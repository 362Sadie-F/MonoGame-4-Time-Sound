using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        SoundEffectInstance explodeInstance;
        Rectangle redButton;
        Texture2D button;
        float seconds;
        bool exploded = false;

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
            redButton = new Rectangle(258, 133, 50, 50);
            seconds = 0;
            button = new Texture2D(GraphicsDevice, 50, 50);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("TimeFont");
            explosionEffect = Content.Load<SoundEffect>("explosion");
            explodeInstance = explosionEffect.CreateInstance();
            

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
            //if (mouseState.LeftButton == ButtonState.Pressed)
            //{
            //    seconds = 0f;
            //}

            if (redButton.Contains(mouseState.Position))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    seconds = 0f;
                }
            }


            if (seconds >= 15)
            {
                explodeInstance.Play();
                seconds = 0f;
            }

            this.Window.Title = mouseState.Position.ToString();

            if (explodeInstance.State == SoundState.Stopped)
            {
                exploded = true;
            }

            if (exploded == true && explodeInstance.State == SoundState.Stopped)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombSize, Color.White);
            _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            _spriteBatch.Draw(button, redButton, Color.Transparent);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
