﻿using Microsoft.Xna.Framework;
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
        Point target;
        TileGrid grid;

        public AiFuSMControl(FuSMenemy enemy, Point target, TileGrid grid)
        {
            this.enemy = enemy;
            
            this.target = target;
            this.grid = grid;
            attack = new FStateAttack(enemy, target, grid);
            AddStates();
        }

        public void AddStates()
        {
            fuzzy = new FuSM(this);
            fuzzy.AddState(attack);
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