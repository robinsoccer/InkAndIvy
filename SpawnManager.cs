using InkAndIvy.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy
{
    public class SpawnManager
    {
        private Texture2D textureAtlas;
        private int scale;

        private List<Rectangle> srcRect;
        private List<Rectangle> dstRect;

        private int spawnIndex;

        private Vector2 tilemapOffset;
        private GridManager gridManager;

        private List<string> spawnNames;
        private List<Rectangle> spawnableTiles;
        private List<Rectangle> prevSpawnRects;


        private string spawnFilepath;

        public SpawnManager(Texture2D textureAtlas, Vector2 tilemapOffset, int scale, string spawnFilepath, List<string> spawnNames)
        {
            this.textureAtlas = textureAtlas;

            spawnIndex = 0;

            srcRect = new List<Rectangle>();
            dstRect = new List<Rectangle>();
            prevSpawnRects = new List<Rectangle>();

            this.tilemapOffset = tilemapOffset;
            this.scale = scale;
            gridManager = new GridManager(scale);
            this.spawnFilepath = spawnFilepath;

            spawnableTiles = new List<Rectangle>();
        }

        public List<Rectangle> GetSpawnTiles()
        {
            List<Rectangle> tiles = new List<Rectangle>();
            StreamReader reader = new StreamReader(spawnFilepath);
            int y = 0;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > 0)
                        {
                            Rectangle spawnTile = new Rectangle(x * 16 * scale + (int)tilemapOffset.X, y * 16 * scale + (int)tilemapOffset.Y, 16 * scale, 16 * scale);
                            tiles.Add(spawnTile);
                        }
                    }
                }
                y++;
            }
            return tiles;
        }

        public void SpawnItems()
        {
            spawnableTiles = GetSpawnTiles();
            int max = spawnNames.Count;
            Random rnd = new Random();
            
            foreach (Rectangle spawnTile in spawnableTiles)
            {
                if (rnd.Next(101) > 90)
                {
                    int index = rnd.Next(max) - 1;
                    Rectangle thisSrcRect = new Rectangle(
                        16 * index,
                        0,
                        16,
                        16);
                    srcRect.Add(thisSrcRect);
                    dstRect.Add(spawnableTiles[index]);
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (srcRect != null)
            {
                for (int i = 0; i < srcRect.Count; i++)
                {
                    spriteBatch.Draw(textureAtlas, dstRect[i], srcRect[i], Color.White);
                }
            }
        }
    }
}
