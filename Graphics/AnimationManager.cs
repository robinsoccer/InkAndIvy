using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkAndIvy.Graphics
{
    public class AnimationManager
    {
        private int keyPressed;
        public AnimationManager()
        {
            keyPressed = 0;
        }

        public int GetKeybind()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.S)) keyPressed = 0;
            else if (Keyboard.GetState().IsKeyDown(Keys.W)) keyPressed = 1;
            else if (Keyboard.GetState().IsKeyDown(Keys.D)) keyPressed = 2;
            else if (Keyboard.GetState().IsKeyDown(Keys.A)) keyPressed = 3;
            
            return keyPressed;
        }
    }
}
