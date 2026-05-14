using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy
{
    public class Sprite
    {
        private readonly float Scale;

        private Texture2D SpriteSheet;
        private int numOfRows;
        private int numOfCols;

        public Sprite(float scale, Texture2D spriteSheet, int numOfRows, int numOfCols)
        {
            Scale = scale;
            SpriteSheet = spriteSheet;
            this.numOfRows = numOfRows;
            this.numOfCols = numOfCols;
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
