using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy.Graphics
{
    public class Tileset
    {
        public Texture2D TextureAtlas;
        public int TileWidth { get; }
        public int TileHeight { get; }
        //Total number of columns
        public int Columns { get; }
        // And rows
        public int Rows { get; }
        // This gets the total number of tiles says the voices in my head (they tell me things...)
        public int Count { get; }

        public List<Rectangle> _tiles;

        public Tileset(Texture2D textureAtlas, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Columns = textureAtlas.Width / tileWidth;
            Rows = textureAtlas.Height / tileHeight;
            Count = Columns * Rows;

            // Create a dictionary with all the tiles
            _tiles = new List<Rectangle>();

            for (int i = 0; i < Count; i++)
            {
                int x = i % Columns * tileWidth;
                int y = i / Rows * tileHeight;
                _tiles[i] = new Rectangle(x, y, TileWidth, TileHeight);
            }
        }

        public Rectangle GetTile(int index) => _tiles[index];

        public Rectangle GetTile(int column, int row)
        {
            int index = row * Columns + column;
            return GetTile(index);
        }
    }
}
