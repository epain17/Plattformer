using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plattformer.Enemy;

namespace Plattformer
{
    class FuSMenemy : BaseEnemy
    {
       

        public FuSMenemy(Texture2D texture, int startPositionX, int startPositionY, Point sleep) :
            base(texture, startPositionX, startPositionY, sleep)
        {
            this.texture = texture;
            this.position = new Vector2(startPositionX, startPositionY);
            this.sleepPoint = sleep;

            this.searchTimer = 0;
            this.searchTimerReset = 200;

            this.patrolTimer = 200;
            this.patrolTimerReset = 200;

            this.energyTimer = 5000;
            this.energyTimerReset = 5000;
            this.energy = 30;

            this.aggroRange = 100;
            this.speed = 100;
            this.random = new Random();

            this.patrolPointX = 0;
            this.patrolPointY = 0;
        }

        public float Aggro
        {
            get { return aggroRange; }
        }

        public override void Update(GameTime gameTime, TileGrid grid, Point target)
        {
            base.Update(gameTime, grid, target);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Aqua);
            base.Draw(spriteBatch);
        }


    }
}
