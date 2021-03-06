﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plattformer.Enemy;

namespace Plattformer.DTree
{
    class DTenemy : BaseEnemy
    {
        Tree tree;
        State currentState;
        PatrolState pState;
        SleepState sState;
        ApproatchState aState;

        public DTenemy(Texture2D texture, int startX, int startY, Point sleep)
            : base(texture, startX, startY, sleep)
        {
            this.texture = texture;
            this.position = new Vector2(startX, startY);
            this.sleepPoint = sleep;

            this.searchTimer = 0;
            this.searchTimerReset = 200;

            this.patrolTimer = 0;
            this.patrolTimerReset = 200;

            this.energyTimer = 5000;
            this.energyTimerReset = 5000;
            this.energy = 15;

            this.aggroRange = 200;
            this.speed = 50;
            this.random = new Random();

            this.patrolPointX = 0;
            this.patrolPointY = 0;

            this.enemyHP = 10;
            this.energy = 15;

            pState = new PatrolState(this);
            sState = new SleepState(this);
            aState = new ApproatchState(this);

            currentState = new State(this);
            MakeDTree();
        }

        public Tree MakeDTree()
        {
            tree = new Tree(this);
            tree.Insert(8, this.FoundPlayer(this.target) == 1, this, null);
            tree.Insert(3, this.energy <= 0, this, null);
            tree.Insert(10, this.GetHP >= 5, this, null);
            tree.Insert(1, true, this, pState);
            tree.Insert(6, true, this, sState);
            tree.Insert(9, true, this, sState);
            tree.Insert(14, true, this, aState);
           // Console.WriteLine(tree.DrawTree());

            return tree;

        }

        public State Behaviour(State state, Tree tree)
        {
            state = tree.Traverse(this.target);
            return state;
        }

        public bool TargetUpdate(Point target)
        {
            if(this.FoundPlayer(target) == 1)
            {
                return true;
            }
            return false;
        }
         

        public override void Update(GameTime gameTime, TileGrid grid, Point target)
        {
            HP();
            currentState = Behaviour(currentState, tree);
            currentState.RunBehaviour(gameTime, grid, target);
            //Console.WriteLine(currentState);
            Console.WriteLine(this.enemyHP + "DTEnemy");


            tree = MakeDTree();

            base.Update(gameTime, grid, target);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
