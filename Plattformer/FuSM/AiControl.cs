using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class AiControl
    {
        FuSMenemy enemy;
        private FuSM fuzzy;
        FStateAttack attack;
        FStateAvoid avoid;
        FStateHide hide;
        Point target;
        TileGrid grid;

        public AiControl(FuSMenemy enemy, Point target, TileGrid grid)
        {
            this.enemy = enemy;     
            this.target = target;
            this.grid = grid;
            attack = new FStateAttack(enemy, target, grid);
            avoid = new FStateAvoid(enemy, target, grid);
            hide = new FStateHide(enemy, target, grid);
            
            AddStates();
        }

        public void AddStates()
        {
            fuzzy = new FuSM(this);
            fuzzy.AddState(attack);
            fuzzy.AddState(avoid);
            //fuzzy.AddState(hide);
            fuzzy.Reset();
        }

        public void Update(GameTime gameTime, Point target, TileGrid grid)
        {
            enemy.Update(gameTime, grid, target);
            fuzzy.UpdateMachine(gameTime);
        }

        void UpdatePerceptions(GameTime gameTime)
        {

        }
    }
}
