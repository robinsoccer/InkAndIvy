using InkAndIvy.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InkAndIvy.Scenes
{
    public class HouseScene : IScene
    {
        private ContentManager Content;
        private SceneManager sceneManager;
        private GraphicsDeviceManager graphics;

        private FurnitureManager furnitureManager;

        private Texture2D playerTexture;
        private Player player;

        private Tilemap BG;
        private Tilemap MG;
        private Tilemap FG;
        private Dictionary<Vector2, int> tilemap;
        private List<Rectangle> textureStore;
        private Texture2D tileset;

        private Texture2D windowTexture;

        private Vector2 tilemapPos;

        private Game1 game;

        private KeyboardState oldState;

        private bool newGame;

        public HouseScene(ContentManager contentManager, SceneManager sceneManager, GraphicsDeviceManager graphicsDeviceManager, Game1 game, bool newGame)
        {
            Content = contentManager;
            this.sceneManager = sceneManager;
            graphics = graphicsDeviceManager;

            tilemapPos = new Vector2(graphics.PreferredBackBufferWidth / 2 - 7 * 16 * 5, graphics.PreferredBackBufferHeight / 2 - 7 * 16 * 5);

            this.game = game;

            this.newGame = newGame;
        }

        public void Load()
        {
            playerTexture = Content.Load<Texture2D>("player/heroSpriteSheetv2");


            player = new Player(5, playerTexture, 4, 8, 5, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), 0, 7, "../../../Content/Data/HouseTilemapv1_Collision.csv", tilemapPos, Content);

            

            windowTexture = Content.Load<Texture2D>("furniture/windowImg");

            tileset = Content.Load<Texture2D>("tilesets/HouseTilesetv2");

            BG = new Tilemap(tileset, "../../../Content/Data/HouseTilemapv1_BG.csv", 24);
            textureStore = BG.TileSourceRect();
            tilemap = BG.LoadMap("../../../Content/Data/HouseTilemapv1_BG.csv");

            MG = new Tilemap(tileset, "../../../Content/Data/HouseTilemapv1_MG.csv", 24);
            textureStore = MG.TileSourceRect();
            tilemap = MG.LoadMap("../../../Content/Data/HouseTilemapv1_MG.csv");

            FG = new Tilemap(tileset, "../../../Content/Data/HouseTilemapv1_FG.csv", 24);
            textureStore = FG.TileSourceRect();
            tilemap = FG.LoadMap("../../../Content/Data/HouseTilemapv1_FG.csv");

            if (newGame == false)
            {
                game._playerData = game._playerData.LoadGame(game._savePath);
                player.position = game._playerData.Position;
            }

            furnitureManager = new FurnitureManager(windowTexture, tilemapPos, 5);
            furnitureManager.AddFurniture(new Vector2(5, 1), new Rectangle(0, 0, 16, 32));
            furnitureManager.AddFurniture(new Vector2(7, 1), new Rectangle(0, 0, 16, 32));
        }
        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();

            Rectangle playerRect = player.Update(gameTime);
            Vector2 playerPos = new Vector2(playerRect.X, playerRect.Y);

            if (kState.IsKeyDown(Keys.O) && oldState.IsKeyUp(Keys.O))
            {
                game._playerData.Position = playerPos;

                game._playerData.SaveGame(game._playerData, game._savePath);
            }
            if (kState.IsKeyDown(Keys.L) && oldState.IsKeyUp(Keys.L))
            {
                game._playerData = game._playerData.LoadGame(game._savePath);

                player.position = game._playerData.Position;
            }

            if (playerPos.Y + 17 * 5 > tilemapPos.Y + (13 * 16 * 5))
            {
                sceneManager.AddScene(new ForestScene(Content, sceneManager, graphics, "house", game));
            }

            oldState = kState;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            BG.Draw(spriteBatch, tileset, tilemapPos, 1.0f);
            MG.Draw(spriteBatch, tileset, tilemapPos, 1.0f);

            furnitureManager.Draw(spriteBatch);

            player.Draw(spriteBatch);

            FG.Draw(spriteBatch, tileset, tilemapPos, 1.0f);

            spriteBatch.End();
        }
    }
}
