using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class Decoration : GameObject
    {

        public Decoration(Texture2D tex, int x, int y) : base(tex, x, y)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, drawPos, Color.White);
        }
    }
}
