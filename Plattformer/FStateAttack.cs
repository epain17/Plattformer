﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Plattformer
{
    class FStateAttack : FuSMState
    {
        FuSMenemy enemy;
        public FStateAttack(FuSMenemy enemy)
        {
            this.enemy = enemy;
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}
