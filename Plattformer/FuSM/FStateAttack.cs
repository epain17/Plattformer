using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Plattformer.Enemy;

namespace Plattformer
{
    class FStateAttack : FuSMState
    {
        FuSMenemy enemy;
        Point target;
        TileGrid grid;
        public FStateAttack(FuSMenemy enemy, Point target, TileGrid grid) : base(target, grid)
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
            if (enemy.FoundPlayer(target) == 1 )
            {
                enemy.FindPath(target, grid);
            }
            enemy.UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        public override float CalculateActivation()
        {
            target = enemy.GetTarget;
            if(enemy.FoundPlayer(target) == 2 && enemy.PlayerHP > 5)
            {
                activationLevel = 0.0f;
            }

            else if(enemy.FoundPlayer(target) == 1 && enemy.PlayerHP <=5)
            {
                activationLevel = 2.0f;
                enemy.Speed = 120;
            }

            else if (enemy.FoundPlayer(target) == 1 && enemy.PlayerHP <= 3)
            {
                activationLevel = 2.0f;
                enemy.Speed = 140;
            }

            else if(enemy.FoundPlayer(target) == 1 && enemy.PlayerHP > 5)
            {
                activationLevel = 0.0f;

            }

            CheckBounds();
            return base.CalculateActivation();
        }

       
    }
}
