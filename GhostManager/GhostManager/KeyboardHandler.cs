using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;


namespace GhostManager
{
    public class KeyboardHandler
    {
        private KeyboardState prevKeyboardState;
        private KeyboardState currentKeyboardState;

        public KeyboardHandler()
        {
            prevKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key));
        }

        public bool IsKeyHeld(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyDown(key));
        }

        public bool WasKeyPressed(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key));
        }

        public bool HasReleasedKey(Keys key)
        {
            return (currentKeyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key));
        }

        public void Update()
        {
            prevKeyboardState = currentKeyboardState;

            currentKeyboardState = Keyboard.GetState();
        }

        public bool WasAnyKeyPressed()
        {
            Keys[] keyPressed = currentKeyboardState.GetPressedKeys();
            if (keyPressed.Length > 0)
            {
                foreach (Keys k in keyPressed)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
