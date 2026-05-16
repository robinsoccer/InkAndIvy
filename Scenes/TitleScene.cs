using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy.Scenes
{
    public class TitleScene : IScene
    {
        private ContentManager Content;
        private SceneManager sceneManager;
        private GraphicsDeviceManager graphics;

        private Texture2D titleImage;
        private Texture2D loadButton;
        private Texture2D newButton;
        private Texture2D madeByTitle;

        private bool beenFiveSecsonds;
        private float timer = 0f;

        private Color loadColor;
        private Color newColor;

        private Song introSong;

        private bool inSpot;
        private Vector2 titlePos;
        private Vector2 loadPos;
        private Vector2 newPos;

        private int speed;
        private int scale;

        private Game1 _game;

        public TitleScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphics, int speed, int scale, Game1 game)
        {
            this.speed = speed;
            Content = contentManager;
            this.sceneManager = sceneManager;
            this.graphics = graphics;
            this.scale = scale;
            
            _game = game;

            inSpot = false;
            beenFiveSecsonds = false;
        }

        public void Load()
        {
            introSong = Content.Load<Song>("audio/Intro");
            MediaPlayer.Play(introSong);

            titleImage = Content.Load<Texture2D>("titles/maintitleimage");
            loadButton = Content.Load<Texture2D>("titles/LoadImageTS");
            newButton = Content.Load<Texture2D>("titles/NewImageTS");
            madeByTitle = Content.Load<Texture2D>("titles/madeByoldAwesome");

            titlePos = new Vector2(graphics.PreferredBackBufferWidth / 2 - titleImage.Width / 2 * scale, graphics.PreferredBackBufferHeight);
            
        }
        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= 5f)
            {
                beenFiveSecsonds = true;
                timer = 0f;
            }
            
            // Calculate the target Y position
            float targetY = graphics.PreferredBackBufferHeight / 15f;

            // If we haven't reached the target yet, keep moving up
            if (titlePos.Y > targetY && beenFiveSecsonds)
            {
                titlePos.Y -= speed;

                // "Snap" to the target if we overshoot it
                if (titlePos.Y < targetY)
                {
                    titlePos.Y = targetY;
                }
            } else
            {
                inSpot = true;
            }

            loadColor = new Color(180, 180, 180);
            newColor = new Color(180, 180, 180);
            newPos = new Vector2((graphics.PreferredBackBufferWidth / 2 - newButton.Width / 2 * scale * 2) - 500, titlePos.Y + titleImage.Height * scale + 200);
            loadPos = new Vector2((graphics.PreferredBackBufferWidth / 2 - newButton.Width / 2 * scale * 2) + 500, titlePos.Y + titleImage.Height * scale + 200);


            MouseState mouseState = Mouse.GetState();
            Rectangle newBounds = new Rectangle((int)newPos.X, (int)newPos.Y, newButton.Width * 2 * scale, newButton.Height * 2 * scale);
            Rectangle loadBounds = new Rectangle((int)loadPos.X, (int)loadPos.Y, loadButton.Width * 2 * scale, loadButton.Height * 2 * scale);

            if (newBounds.Contains(mouseState.Position))
            {
                newColor = new Color(255, 255, 255);
            }

            if (newBounds.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MediaPlayer.Pause();
                _game._playerData = new PlayerData() { Name = "New Player", Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2) };

                sceneManager.AddScene(new HouseScene(Content, sceneManager, graphics, _game, true));
            }

            if (loadBounds.Contains(mouseState.Position))
            {
                loadColor = new Color(255, 255, 255);
            }

            if (loadBounds.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                MediaPlayer.Pause();

                if (_game._playerData == null)
                {
                    _game._playerData = new PlayerData();
                }

                _game._playerData.LoadGame(_game._savePath);

                sceneManager.AddScene(new HouseScene(Content, sceneManager, graphics, _game, false));
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if (!beenFiveSecsonds)
            {
                spriteBatch.Draw(madeByTitle, new Rectangle(graphics.PreferredBackBufferWidth / 2 - (madeByTitle.Width * 8 / 2), graphics.PreferredBackBufferHeight / 2 - (madeByTitle.Height * 8 / 2), madeByTitle.Width * 8, madeByTitle.Height * 8), Color.White * 0.9f);
            }

            if (beenFiveSecsonds)
            {
                spriteBatch.Draw(titleImage, new Rectangle((int)titlePos.X, (int)titlePos.Y, titleImage.Width * scale, titleImage.Height * scale), Color.White);
            }

            if (inSpot)
            {
                spriteBatch.Draw(newButton, new Rectangle((int)newPos.X, (int)newPos.Y, newButton.Width * scale * 2, newButton.Height * scale * 2), newColor);
                spriteBatch.Draw(loadButton, new Rectangle((int)loadPos.X, (int)loadPos.Y, loadButton.Width * scale * 2, loadButton.Height * scale * 2), loadColor);
            }

            spriteBatch.End();
        }
    }
}
