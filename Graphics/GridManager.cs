using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy.Graphics
{
    public class GridManager
    {
        private int Scale;
        public GridManager(int Scale) { this.Scale = Scale; }

        public Vector2 ToPos(Vector2 gridPos, Vector2 tilemapOffset)
        {
            Vector2 pos = new Vector2((gridPos.X * 16 * Scale) + tilemapOffset.X, (gridPos.Y * 16 * Scale) + tilemapOffset.Y);
            return pos;
        }

        public Vector2 ToGrid(Vector2 pos, Vector2 tilemapOffset)
        {
            Vector2 gridPos = new Vector2((int)(pos.X / 16 / Scale) - tilemapOffset.X, (int)(pos.X / 16 /Scale) + tilemapOffset.Y);
            return gridPos;
        }
    }
}
