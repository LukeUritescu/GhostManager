using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GhostManager
{
        public interface IInputHandler
        {
            bool WasPressed(int playerIndex, Keys keys);
            bool WasKeyPressed(Keys keys);

            KeyboardHandler KeyboardState { get; }


        }
    public partial class InputHandler : Microsoft.Xna.Framework.GameComponent, IInputHandler
    {
        //Wrapper for keyboard
        private KeyboardHandler keyboard;

        public override void Initialize()
        {

            base.Initialize();
        }

        public InputHandler(Game game) : this(game, false) { }

        public InputHandler(Game game, bool allowsExiting) : base(game)
        {
            game.Services.AddService(typeof(IInputHandler), this);

            keyboard = new KeyboardHandler();
        }


        public override void Update(GameTime gameTime)
        {
            keyboard.Update();
        }

        public bool WasPressed(int playerIndex, Keys keys)
        {
            if (keyboard.WasKeyPressed(keys))
                return (true);
            else
                return (false);
        }

        public bool WasKeyPressed(Keys keys)
        {
            return keyboard.WasKeyPressed(keys);
        }

        public KeyboardHandler KeyboardState
        {
            get { return (keyboard); }
        }
    }
}
