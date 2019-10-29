using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GhostManager
{
    class Spawner : Microsoft.Xna.Framework.DrawableGameComponent
    {
       public List<MonogameGhost> Ghosts;
        Random rand;

        InputHandler inputHandler;
        GameConsole console;

        public Spawner(Game game) : base(game)
        {
            Ghosts = new List<MonogameGhost>();
            rand = new Random(System.DateTime.Now.Millisecond);
            inputHandler = (InputHandler)game.Services.GetService(typeof(IInputHandler));
            console = (GameConsole)game.Services.GetService(typeof(IGameConsole));
        }

        public override void Update(GameTime gameTime)
        {
            for(int i = 0; i < Ghosts.Count; i++)
            {
                Ghosts[i].Update(gameTime);
            }
            UpdateGhosts();
            base.Update(gameTime);
        }

        protected void ChangeState(GhostMovementState ghostState)
        {
            foreach(MonogameGhost mg in Ghosts)
            {
                mg.Ghost.GhostState = ghostState;
            }
        }

        public void AddGhost()
        {
            AddGhost("PurpleGhost");

        }

        public void AddFourGhosts()
        {
            for (int i = 0; i < 4; i++)
            {

                AddGhost("PurpleGhost");

            }
        }
        protected void RemoveGhost(MonogameGhost mg)
        {
            this.Ghosts.Remove(mg);
        }

        private void AddGhost(string TextureName)
        {
            MonogameGhost gm = new MonogameGhost(Game, this);
            gm.strGhostTexture = TextureName;
            gm.Initialize();
            gm.Direction = gm.GetRandomDirection();
            gm.Location = gm.GetRandomLocation();
            gm.SetTranformAndRect();

            foreach(MonogameGhost otherGhost in Ghosts)
            {
                while (gm.Intersects(otherGhost))
                {
                    gm.Location = gm.GetRandomLocation();
                    gm.SetTranformAndRect();
                }
            }
            gm.Scale = 1.0f;
            gm.Enabled = true;
            gm.Visible = true;
            Ghosts.Add(gm);

        }
        protected void UpdateGhosts()
        {
            if (inputHandler.WasKeyPressed(Keys.A))
            {
                this.ChangeState(GhostMovementState.Roaming);
                this.AddGhost();
                console.GameConsoleWrite("A Ghost was added");
            }
            if (inputHandler.WasKeyPressed(Keys.R))
            {
                this.RemoveGhost(Ghosts[rand.Next(0, Ghosts.Count)]);
                console.GameConsoleWrite("A Ghost was removed");
            }
            if (inputHandler.WasKeyPressed(Keys.F))
            {
                this.AddFourGhosts();
                console.GameConsoleWrite("Four Ghosts created");
            }
        }

        protected override void LoadContent()
        {
            AddGhost();
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(MonogameGhost gm in Ghosts)
            {
                if(gm.Enabled)
                gm.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

    }
}
