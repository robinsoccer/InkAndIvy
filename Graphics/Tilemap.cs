using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace InkAndIvy.Graphics
{
    public class Tilemap
    {
        private Dictionary<Vector2, int> tilemap;
        private List<Rectangle> textureStore;
        private int totalTextures;
        private string filepath;

        public Tilemap(Texture2D tileset, string filepath, int totalTextures)
        {
            tilemap = LoadMap(filepath);
            this.totalTextures = totalTextures;
        }

        public Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();

            StreamReader reader = new StreamReader(filepath);


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
                            result[new Vector2(x, y)] = value;
                        }
                    }

                }
                y++;
            }
            return result;
        }

        public List<Rectangle> TileSourceRect()
        {
            textureStore = new();
            for (int i = 0; i < totalTextures; i++)
            {
                Rectangle rect = new Rectangle((int)i * 16, 0, 16, 16);
                textureStore.Add(rect);
            }
            return textureStore;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tileset, Vector2 startingPos, float brightness)
        {
            foreach (var item in tilemap)
            {
                Rectangle dest = new Rectangle(
                    (int)item.Key.X * 16 * 5 + (int)startingPos.X,
                    (int)item.Key.Y * 16 * 5 + (int)startingPos.Y,
                    16 * 5,
                    16 * 5);

                Rectangle src = textureStore[item.Value];

                spriteBatch.Draw(tileset, dest, src, Color.White * brightness);
            }
        }
    }
}
