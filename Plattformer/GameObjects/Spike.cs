using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Plattformer
{
    class Spike : GameObject
    {
        public Vector2 spikePos
        {
            get { return drawPos; }
        }

        public override Rectangle HitBox()
        {
            return new Rectangle((int)drawPos.X, (int)drawPos.Y, 40, 40);
        }

        public Spike(Texture2D tex, int x, int y) : base(tex, x, y)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, drawPos, Color.White);
        }


    }
}
