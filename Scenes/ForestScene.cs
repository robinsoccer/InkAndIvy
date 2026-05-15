using InkAndIvy.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy.Scenes
{
    internal class ForestScene : IScene
    {
        private ContentManager Content;
        private SceneManager sceneManager;
        private GraphicsDeviceManager graphics;

        private Texture2D playerTexture;
        private Player player;

        private Tilemap BG;
        private Tilemap MG;
        private Tilemap FG;

        private Dictionary<Vector2, int> tilemap;
        private List<Rectangle> textureStore;
        private Texture2D tileset;

        private Vector2 tilemapPos;
        private Vector2 playerPos;

        private GridManager gridM;

        private int totalTextures;
        private int tilemapLength;

        private Camera camera;

        private int scale;

        private Game game;

        public ForestScene(ContentManager Content, SceneManager sceneManager, GraphicsDeviceManager graphics, string starting, Game game)
        {
            this.Content = Content;
            this.sceneManager = sceneManager;
            this.graphics = graphics;

            tilemapLength = 40;
            scale = 5;

            this.game = game;

            gridM = new GridManager(scale);

            if (starting == "house")
            {
                // Player should spawn in at these grid coords: (16, 22)
                playerPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
                
                tilemapPos.X = playerPos.X - (15 * 16 * scale);
                tilemapPos.Y = Vector2.Zero.Y - (12 * 16 * scale);
            }
            else
            {
                playerPos = Vector2.Zero;
                tilemapPos = Vector2.Zero;
            }
        }

        public void Load()
        {
            playerTexture = Content.Load<Texture2D>("player/heroSpriteSheetv1");
            player = new Player(5, playerTexture, 4, 8, 5, playerPos, 0, 7, "../../../Content/Data/Forest/ForestTilemapv5_collision.csv", tilemapPos, Content);
            
            tileset = Content.Load<Texture2D>("tilesets/ForestTilesetv5");

            camera = new Camera(game.GraphicsDevice);

            totalTextures = tileset.Width / 16;

            BG = new Tilemap(tileset, "../../../Content/Data/Forest/ForestTilemapv5_BG.csv", totalTextures);
            textureStore = BG.TileSourceRect();
            tilemap = BG.LoadMap("../../../Content/Data/Forest/ForestTilemapv5_BG.csv");

            MG = new Tilemap(tileset, "../../../Content/Data/Forest/ForestTilemapv5_MG.csv", totalTextures);
            textureStore = MG.TileSourceRect();
            tilemap = MG.LoadMap("../../../Content/Data/Forest/ForestTilemapv5_MG.csv");

            FG = new Tilemap(tileset, "../../../Content/Data/Forest/ForestTilemapv5_FG.csv", totalTextures);
            textureStore = FG.TileSourceRect();
            tilemap = FG.LoadMap("../../../Content/Data/Forest/ForestTilemapv5_FG.csv");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();

            Rectangle playerRect = player.Update(gameTime);
            Vector2 playerPos = new Vector2(playerRect.X, playerRect.Y);

            camera.Position = player.position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetTransformation());

            BG.Draw(spriteBatch, tileset, tilemapPos);
            MG.Draw(spriteBatch, tileset, tilemapPos);

            player.Draw(spriteBatch);

            FG.Draw(spriteBatch, tileset, tilemapPos);
        }
    }
}
