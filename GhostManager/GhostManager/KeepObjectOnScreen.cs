using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace GhostManager
{
    class KeepObjectOnScreen
    {
        protected Vector2 DirectionScreen;
        protected Vector2 LocationScreen;
        public KeepObjectOnScreen()
        {
            DirectionScreen = new Vector2(0, 0);
            LocationScreen = new Vector2(0, 0);

        }

        public Vector2 GetDirectionScreen()
        {
            return DirectionScreen;
        }

        public Vector2 GetLocationScreen()
        {
            return LocationScreen;
        }

        public DrawableSprite UpdatePacManKeepOnScreen(DrawableSprite sprite, int WindowWidth, int WindowHeight)
        {
            if (sprite.Location.X > WindowWidth - (sprite.spriteTexture.Width / 2))
            {
                sprite.Direction.X = sprite.Direction.X * -1;
                sprite.Location.X = WindowWidth - (sprite.spriteTexture.Width / 2);
            }
            if (sprite.Location.X < ((WindowWidth - WindowWidth) + (sprite.spriteTexture.Height / 2)))
            {
                sprite.Direction.X = sprite.Direction.X * -1;
                sprite.Location.X = sprite.spriteTexture.Width / 2;
            }

            if (sprite.Location.Y > WindowHeight - (sprite.spriteTexture.Height / 2))
            {
                sprite.Direction.Y = sprite.Direction.Y * -1;
                sprite.Location.Y = WindowHeight - (sprite.spriteTexture.Height / 2);
            }

            if (sprite.Location.Y < ((WindowHeight - WindowHeight) + (sprite.spriteTexture.Height / 2)))
            {
                sprite.Direction.Y = sprite.Direction.Y * -1;
                sprite.Location.Y = sprite.spriteTexture.Height / 2;
            }

            return sprite;
        }
    }
}
