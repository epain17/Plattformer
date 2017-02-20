using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.DTree
{
    class SleepState : State
    {
        DTenemy enemy;

        public SleepState(DTenemy enemy) : base(enemy)
        {
            this.enemy = enemy;

        }

        public override void RunBehaviour(GameTime gameTime, TileGrid grid, Point target)
        {

            if (enemy.waypoints.Count() != 0 && enemy.sleepPoint != enemy.myGridPoint)
            {
                enemy.FindPath(enemy.sleepPoint, grid);
            }
            enemy.energyTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (enemy.energyTimer < 0)
            {
                enemy.energy = 15;
                enemy.enemyHP = 10;
                enemy.energyTimer = enemy.energyTimerReset;
            }
            if (enemy.waypoints.Count() != 0 && enemy.waypoints != null)
            {
                enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }



            base.RunBehaviour(gameTime, grid, target);
        }
    }
}
