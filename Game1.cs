using InkAndIvy.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;

namespace InkAndIvy
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SceneManager sceneManager;

        public PlayerData _playerData;
        public string _savePath;

        private Texture2D cursorTexture;
        private Vector2 cursorPosition;

        private Texture2D itemTextureAtlas;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            sceneManager = new();

            Window.Title = "Ink and Ivy";
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "InkAndIvy");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            _savePath = Path.Combine(folder, "playerData.xml");

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            itemTextureAtlas = Content.Load<Texture2D>("furniture/itemSpritesheetv1");

            sceneManager.AddScene(new TitleScene(Content, sceneManager, _graphics, 4, 2, this, itemTextureAtlas));

            MouseState mouseState = new MouseState();
            cursorPosition = new Vector2(mouseState.X, mouseState.Y);
            cursorTexture = Content.Load<Texture2D>("mousespritev2");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sceneManager.GetCurrentScene().Update(gameTime);

            MouseState mouseState = Mouse.GetState();
            cursorPosition = new Vector2(mouseState.X, mouseState.Y);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Color color = new Color(33, 33, 33);
            GraphicsDevice.Clear(color);

            sceneManager.GetCurrentScene().Draw(_spriteBatch);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            Vector2 scale = new Vector2(2.5f, 2.5f);
            _spriteBatch.Draw(cursorTexture, cursorPosition, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
