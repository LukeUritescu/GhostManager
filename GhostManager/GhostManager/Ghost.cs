using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostManager
{
    public enum GhostMovementState { Roaming, Idle }

    class Ghost
    {
        GhostMovementState _ghostState;
        public GhostMovementState GhostState
        {
            get { return _ghostState; }
            set
            {
                if (_ghostState != value)
                {
                    _ghostState = value;
                }
            }
        }

        public Ghost()
        {
            this._ghostState = GhostMovementState.Roaming;
        }

        public virtual void Log(string s)
        {
            //nothing
            Console.WriteLine(s);
        }
    }
}
