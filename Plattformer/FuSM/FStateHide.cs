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
        Point target, hide, currentHide;
        TileGrid grid;

        public FStateHide(FuSMenemy enemy, Point target, TileGrid grid) : base(target, grid)
        {
            this.enemy = enemy;
            this.target = target;
            this.grid = grid;
        }

        public override void Init()
        {
            activationLevel = 0.0f;
            hide = grid.FindEscapePoint(target);
            enemy.Speed = 160;

            base.Init();
        }

        public override void Update(GameTime gameTime)
        {
            if (enemy.myGridPoint != hide && enemy.DistanceTo(target, enemy.myGridPoint)  > 2)
            {
                enemy.Speed = 160;
                enemy.FindPath(hide, grid);
                enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if(enemy.myGridPoint == hide && enemy.DistanceTo(target, enemy.myGridPoint) < 2)
            {
                enemy.Speed = 0;
                hide = grid.FindEscapePoint(target);
            }



                base.Update(gameTime);
        }

        public override float CalculateActivation()
        {
            if(enemy.myGridPoint != hide && enemy.DistanceTo(target, enemy.myGridPoint) > 5)
            {
                activationLevel = 2f;
            }

            else if(enemy.myGridPoint == hide)
            {
                activationLevel = 0;
            }
            CheckBounds();
            return base.CalculateActivation();
        }
    }
}
