using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class FStateAvoid : FuSMState
    {
        FuSMenemy enemy;
        Point target;
        TileGrid grid;

        public FStateAvoid(FuSMenemy enemy, Point target, TileGrid grid) : base(target, grid)
        {
            this.enemy = enemy;
            this.target = target;
            this.grid = grid;
        }

        public override void Init()
        {
            activationLevel = 0.0f;
            enemy.Speed = 50;
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            Point temp = new Point(0, 0);
            Point flee = enemy.EscapePoint(enemy.myGridPoint, target, grid);
            //SetSpeed(target);
            if (flee != temp)
            {

                enemy.FindPath(flee, grid);
                enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            base.Update(gameTime);
        }

        public override float CalculateActivation()
        {
            target = enemy.GetTarget;
            SetSpeed(target);
            if (enemy.Speed <= 30)
            {
                activationLevel = 0.0f;

            }
            else if(enemy.Speed > 30 && KeyMouseReader.KeyPressed(Microsoft.Xna.Framework.Input.Keys.B))
            {
                activationLevel = 2.0f;
            }
            CheckBounds();
            return base.CalculateActivation();
        }
        public void SetSpeed(Point target)
        {
            target = enemy.GetTarget;
            if (enemy.DistanceTo(target, enemy.myGridPoint) <= 12 && enemy.DistanceTo(target, enemy.myGridPoint) >= 10)
            {
                enemy.Speed = 50;
            }

            else if (enemy.DistanceTo(target, enemy.myGridPoint) <= 10 && enemy.DistanceTo(target, enemy.myGridPoint) >= 5)
            {
                enemy.Speed = 80;
            }

            else if(enemy.DistanceTo(target, enemy.myGridPoint) <= 5 && enemy.DistanceTo(target, enemy.myGridPoint) >= 0)
            {
                enemy.Speed = 120;
            }


        }
    }
}
