using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;
using InkAndIvy.Graphics;

namespace InkAndIvy
{
    public class Player
    {
        private readonly int Scale;

        private Texture2D SpriteSheet;
        private int numOfRows;
        private int numOfCols;

        private int currentRow;
        private int currentCol;

        private AnimationManager am;
        private bool isMoving;
        private int speed;

        public Vector2 position { get; set; }

        private int frameCounter;
        private int interval;

        private List<Rectangle> collisionTiles;
        private Vector2 tilemapOffset;


        public Player(int scale, Texture2D spriteSheet, int numOfRows, int numOfCols, int speed, Vector2 pos, int frameCounter, int interval, string collisionFilepath, Vector2 tilemapOffset, ContentManager content)
        {
            Scale = scale;
            SpriteSheet = spriteSheet;
            this.numOfRows = numOfRows - 1;
            this.numOfCols = numOfCols;

            currentCol = 0;
            currentRow = 0;
            isMoving = false;
            this.speed = speed;

            position = pos;
            this.frameCounter = frameCounter;
            this.interval = interval;

            collisionTiles = GetTiles(collisionFilepath, tilemapOffset);
            this.tilemapOffset = tilemapOffset;

            am = new AnimationManager();
        }

        public List<Rectangle> GetTiles(string filepath, Vector2 tilemapOffset)
        {
            List<Rectangle> tiles = new List<Rectangle>();
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
                            Rectangle tileBox = new Rectangle((x * 16 * Scale) + (int)tilemapOffset.X, (y * 16 * Scale) + (int)tilemapOffset.Y, 16 * Scale, 16 * Scale);
                            tiles.Add(tileBox);
                        }
                    }
                }
                y++;
            }


            return tiles;
        }

        public Rectangle Update(GameTime gameTime)
        {
            Rectangle playerBounds = new Rectangle((int)position.X + (1 * Scale), (int)position.Y + (20 * Scale), 14 * Scale, 12 * Scale);

            if (am != null)
            {
                currentRow = am.GetKeybind();
            }

            isMoving = false;
            Vector2 movement = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.X -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                movement.Y -= 1;

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                movement.Y += 1;
            }

            if (movement != Vector2.Zero)
            {
                movement = Vector2.Normalize(movement);
                isMoving = true;
            }

            Vector2 velocity = movement * speed;

            Vector2 horizontalMovement = new Vector2(velocity.X, 0);
            Rectangle horizontalRect = new Rectangle(
                (int)(playerBounds.X + horizontalMovement.X),
                playerBounds.Y,
                playerBounds.Width,
                playerBounds.Height);

            foreach (var tile in collisionTiles)
            {
                if (horizontalRect.Intersects(tile))
                {
                    velocity.X = 0;
                    break;
                }
            }

            Vector2 verticalMovement = new Vector2(0, velocity.Y);
            Rectangle verticalRect = new Rectangle(
                playerBounds.X,
                playerBounds.Y + (int)verticalMovement.Y,
                playerBounds.Width,
                playerBounds.Height);

            foreach (var tile in collisionTiles)
            {
                if (verticalRect.Intersects(tile))
                {
                    velocity.Y = 0;
                    break;
                }
            }

            position += velocity;
            


            if (isMoving)
            {
                frameCounter++;
                if (frameCounter > interval)
                {
                    frameCounter = 0;
                    currentCol++;
                    if (currentCol >= numOfCols)
                    {
                        currentCol = 0;
                    }
                }
                
            }
            else
            {
                currentCol = 0;
            }

            return playerBounds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(
                currentCol * 16,
                currentRow * 32,
                16,
                32);

            spriteBatch.Draw(
                SpriteSheet,
                new Rectangle((int)position.X, (int)position.Y, 16 * Scale, 32 * Scale),
                sourceRect,
                Color.White);

        }
    }
}
