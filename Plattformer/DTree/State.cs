using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.DTree
{
    class State
    {
        string name;

        public State(string name)
        {
            this.name = name;
        }

        public virtual void RunBehaviour(GameTime gameTime, TileGrid grid, Point target)
        {

        }
    }
}
