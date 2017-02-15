using Microsoft.Xna.Framework;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class AiFuSMControl
    {
        FuSMenemy enemy;
        private FuSM fuzzy;
        FStateAttack attack;
        FStateAvoid avoid;
        Point target;
        TileGrid grid;
        int playerHP;

        public AiFuSMControl(FuSMenemy enemy, Point target, TileGrid grid, int playerHP)
        {
            this.enemy = enemy;     
            this.target = target;
            this.grid = grid;
            this.playerHP = playerHP;
            attack = new FStateAttack(enemy, target, grid);
            avoid = new FStateAvoid(enemy, target, grid);
            AddStates();
        }

        public void AddStates()
        {
            fuzzy = new FuSM(this);
            fuzzy.AddState(attack);
            fuzzy.AddState(avoid);
            fuzzy.Reset();
        }

        public void Update(GameTime gameTime, Point target, TileGrid grid, int playerHP)
        {
            enemy.Update(gameTime, grid, target, playerHP);
            fuzzy.UpdateMachine(gameTime);
        }

        void UpdatePerceptions(GameTime gameTime)
        {

        }
    }
}
