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

        public FStateAvoid(FuSMenemy enemy, Point target, TileGrid grid) : base(target, grid)
        {
            this.enemy = enemy;
            this.target = target;
            this.grid = grid;
        }

        public override void Init()
        {
            activationLevel = 0.0f;
            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            Point temp = new Point(0, 0);
            SetSpeed(target);

            Point flee = enemy.EscapePoint(enemy.myGridPoint, target, grid);
            if (flee != temp)
            {
                enemy.FindPath(flee, grid);
                enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            Console.WriteLine("avoid");
            base.Update(gameTime);
        }

        public override float CalculateActivation()
        {
            target = enemy.GetTarget;

            if (enemy.GetHP > 3 && enemy.DistanceTo(target, enemy.myGridPoint) > 3)
            {
                activationLevel = 0.0f;
            }
            else if (enemy.DistanceTo(target, enemy.myGridPoint) > 3)
            {
                activationLevel = 0.0f;
            }
            else if (enemy.GetHP < 3 && enemy.DistanceTo(target, enemy.myGridPoint) < 3)
            {
                activationLevel = (enemy.DistanceTo(target, enemy.myGridPoint)/enemy.GetHP);
            }

            CheckBounds();
            return base.CalculateActivation();
        }

        public void SetSpeed(Point target)
        {
            target = enemy.GetTarget;
            if (enemy.DistanceTo(target, enemy.myGridPoint) < 1)
            {
                enemy.Speed = 0;
            }
            else
            {
                enemy.Speed = 180 / enemy.DistanceTo(target, enemy.myGridPoint);

            }

        }

    }
}
