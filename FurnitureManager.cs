using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy
{
    public class FurnitureManager
    {
        private Texture2D textureAtlas;
        private int Scale;

        private List<Rectangle> srcRect; // Where the texture is located in the atlas
        private List<Rectangle> dstRect; // Where the texture is drawn

        private int furnitureIndex; // How this works is that a texture has the same src and dst Rect index number on the list

        private Vector2 tilemapOffset;


        public FurnitureManager(Texture2D textureAtlas, Vector2 tilemapOffset, int Scale)
        {
            this.textureAtlas = textureAtlas;

            furnitureIndex = 0;

            srcRect = new List<Rectangle>();
            dstRect = new List<Rectangle>(); // Apparently you have to initlize this little f***ers

            this.tilemapOffset = tilemapOffset;
            this.Scale = Scale;
        }

        public void AddFurniture(Vector2 positionOnTilemap, Rectangle srcImport)
        {
            Rectangle src = srcImport;
            Rectangle dst = new Rectangle(
                ((int)positionOnTilemap.X * 16 * Scale) + (int)tilemapOffset.X,
                ((int)positionOnTilemap.Y * 16 * Scale) + (int)tilemapOffset.Y,
                srcImport.Width * Scale,
                srcImport.Height * Scale);

            srcRect.Add(src);
            dstRect.Add(dst);
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
