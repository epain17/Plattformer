using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class FStateHide : FuSMState
    {
        FuSMenemy enemy;
        Point hide;

        public FStateHide(FuSMenemy enemy, Point target, TileGrid grid) : base(target, grid)
        {
            this.enemy = enemy;
            this.target = target;
            this.grid = grid;
        }

        public override void Init()
        {
            activationLevel = 0.0f;
            enemy.Speed = 160;
            hide = grid.FindEscapePoint(target);


            base.Init();
        }

        public override void Exit()
        {
            hide = grid.FindEscapePoint(target);

            base.Exit();
        }

        public override void Update(GameTime gameTime)
        {

            if (enemy.myGridPoint != hide)
            {
                enemy.FindPath(hide, grid);
                enemy.Speed = 160;
                enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (enemy.myGridPoint == hide)
            {
                enemy.Speed = 0;
            }
            //Console.WriteLine("hide" + hide);
            base.Update(gameTime);
        }

        public override float CalculateActivation()
        {
            target = enemy.GetTarget;

            if (enemy.GetHP > 3 && enemy.DistanceTo(target, enemy.myGridPoint) < 3 && enemy.myGridPoint == hide)
            {
                activationLevel = 0;
            }


            else if(enemy.DistanceTo(target, enemy.myGridPoint) < 3)
            {
                activationLevel = 0;
            }

            else if (enemy.GetHP < 3 && enemy.DistanceTo(target, enemy.myGridPoint) > 3)
            {
                activationLevel = (enemy.DistanceTo(target, enemy.myGridPoint)/ enemy.GetHP);
            }

            CheckBounds();
            return base.CalculateActivation();
        }
    }
}
