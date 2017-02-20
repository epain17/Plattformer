using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Plattformer.Enemy;

namespace Plattformer.DTree
{
    class ApproatchState : State
    {
        DTenemy enemy;

        public ApproatchState(DTenemy enemy) : base(enemy)
        {
            this.enemy = enemy;

        }
        public override void RunBehaviour(GameTime gameTime, TileGrid grid, Point target)
        {

            enemy.FindPath(target, grid);

            if (enemy.waypoints.Count() != 0 && enemy.waypoints != null)
            {
                enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            base.RunBehaviour(gameTime, grid, target);
        }
    }
}
