using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GhostManager
{
    class MonogameGhost : DrawableSprite
    {
        protected GhostManagerConsole ghostConsole;
        public GhostManagerConsole Ghost
        {
            get { return this.ghostConsole; }
            protected set { this.ghostConsole = value; }
        }

        protected Spawner ghostManager;
        protected KeepObjectOnScreen keepOnScreen;

        public Texture2D ghostTexture;
        public string strGhostTexture;

        Random rand;

        public MonogameGhost(Game game, Spawner s) : base(game)
        {
            keepOnScreen = new KeepObjectOnScreen();
             
            this.ghostManager = s;
            this.ghostConsole = new GhostManagerConsole((GameConsole)game.Services.GetService<IGameConsole>());
            strGhostTexture = "PurpleGhost";
            this.ghostConsole.GhostState = GhostMovementState.Roaming;

            rand = new Random();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.ghostTexture = this.Game.Content.Load<Texture2D>(strGhostTexture);
            this.spriteTexture = ghostTexture;
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            this.Direction = new Vector2(0, 0);
            this.Speed = 100.0f;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeepOnScreen();
            UpdateMovement(lastUpdateTime);
            Collision();
            Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);
            base.Update(gameTime);
        }

        public void UpdateMovement(float lastUpdateTime)
        {
            switch (this.ghostConsole.GhostState)
            {
                case GhostMovementState.Idle:
                    this.ghostConsole.GhostState = GhostMovementState.Roaming;
                    this.Direction = this.GetRandomDirection();
                    ;
                    break;

                case GhostMovementState.Roaming:
                    if(!(this.spriteTexture == this.ghostTexture))
                    {
                        this.spriteTexture = this.ghostTexture;
                    }
                    break;  

            }
        }
        
        public void KeepOnScreen()
        {
            this.Direction = keepOnScreen.UpdatePacManKeepOnScreen(this, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height).Direction;
            this.Location = keepOnScreen.UpdatePacManKeepOnScreen(this, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height).Location;
        }

        public void Collision()
        {
            foreach (MonogameGhost mg in ghostManager.Ghosts)
                if (this != mg)
                {
                    if(this.Intersects(mg))
                    {
                        if (this.PerPixelCollision(mg))
                        {
                            this.ghostConsole.Log("intersection");
                            this.Direction.X = this.Direction.X * -1;
                            this.Direction.Y = this.Direction.Y * -1;
                        }
                    }
                }
        }

        public Vector2 GetRandomDirection()
        {
            Vector2 vect = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
            Vector2.Normalize(ref vect, out vect);
            return vect;
        }

        public Vector2 GetRandomLocation()
        {
            Vector2 location;
            location.X = rand.Next((Game.Window.ClientBounds.Width - this.spriteTexture.Width) + this.spriteTexture.Width);
            location.Y = rand.Next((Game.Window.ClientBounds.Height - this.spriteTexture.Height) + this.spriteTexture.Height);
            return location;
        }
    }
}
