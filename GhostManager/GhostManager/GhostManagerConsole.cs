using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostManager
{
    class GhostManagerConsole :  Ghost
    {
        GameConsole console;
        public GhostManagerConsole()
        {
            this.console = null;
        }

        public GhostManagerConsole(GameConsole console)
        {
            this.console = console;
        }

        public override void Log(string s)
        {
            if (console  != null)
            {
                console.GameConsoleWrite(s);
            }
            else
            {
                base.Log(s);
            }
        }
    }
}
