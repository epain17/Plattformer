using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.DTree
{
    class PatrolState:State
    {
        DTenemy enemy;

        public PatrolState(DTenemy enemy) : base(enemy)
        {
            this.enemy = enemy;

        }

        public override void RunBehaviour(GameTime gameTime, TileGrid grid, Point target)
        {
       
            if (enemy.patrolTimer <= 0)
            {
                enemy.SetPatroll(enemy.p1, grid);
                enemy.FindPath(enemy.p1, grid);
                enemy.patrolTimer = enemy.patrolTimerReset;
            }

            enemy.patrolTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (enemy.waypoints.Count() != 0 && enemy.waypoints != null)
            {
                enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            base.RunBehaviour(gameTime, grid, target);
        }
    }
}
