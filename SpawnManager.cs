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

        private string bgFilepath;

        public SpawnManager(Texture2D textureAtlas, Vector2 tilemapOffset, int scale, string bgFilepath)
        {
            this.textureAtlas = textureAtlas;

            spawnIndex = 0;

            srcRect = new List<Rectangle>();
            dstRect = new List<Rectangle>();

            this.tilemapOffset = tilemapOffset;
            this.scale = scale;
            gridManager = new GridManager(scale);
            this.bgFilepath = bgFilepath;
        }

        public void SpawnTrees(bool newDay)
        {
            List<Rectangle> tiles = new List<Rectangle>();
            StreamReader bgReader = new StreamReader(bgFilepath);

        }
    }
}
