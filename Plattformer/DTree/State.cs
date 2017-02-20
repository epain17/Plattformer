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
        DTenemy enemy;
        public State(DTenemy enemy)
        {
            this.enemy = enemy;
        }

        public virtual void RunBehaviour(GameTime gameTime, TileGrid grid, Point target)
        {

        }
    }
}
