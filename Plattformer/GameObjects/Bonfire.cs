using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class Bonfire : GameObject
    {

        private double frameTimer, frameInterval;
        private int frame;

        public override Rectangle HitBox()
        {
            return new Rectangle((int)drawPos.X, (int)drawPos.Y, 40, 40);
        }
        public Bonfire(Texture2D tex, int x, int y)
            : base(tex, x, y)
        {
            this.tex = tex;

            sourceRect = new Rectangle(0, 0, 40, 40);
            frameTimer = 170;
            frameInterval = 170;

            drawPos.Y = drawPos.Y + 5;


        }


        public override void Update(GameTime gameTime, TileGrid grid)
        {
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                frame++;
                sourceRect.X = (frame % 5) * 40;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, drawPos, sourceRect, Color.White, 0, new Vector2(), 1, SpriteEffects.None, 1);
            //spriteBatch.Draw(Game1.temptex, HitBox(), Color.Red);
        }
    }
}
