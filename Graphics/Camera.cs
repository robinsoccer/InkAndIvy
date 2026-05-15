using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy.Graphics
{
    public class Camera
    {
        public Vector2 Position { get; set; }
        public float Zoom { get; set; } = 1f;
        public float Rotation { get; set; } = 0f;

        private readonly GraphicsDevice _graphics;

        public Camera(GraphicsDevice graphics)
        {
            _graphics = graphics;
        }

        public Matrix GetTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(_graphics.Viewport.Width * 0.5f, _graphics.Viewport.Height * 0.5f, 0));
        }
    }
}
