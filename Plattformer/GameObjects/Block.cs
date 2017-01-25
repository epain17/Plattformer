using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class Block : GameObject
    {

        public Vector2 blockPos
        {
            get { return drawPos; }
        }

        public override Rectangle HitBox()
        {
            return new Rectangle((int)drawPos.X, (int)drawPos.Y, 40, 40);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public Block(Texture2D tex, int x, int y) : base(tex, x, y)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, drawPos, Color.White);
            Rectangle top = HitBox();
            top.Height = 10;


            //Rectangle bottom = HitBox();
            //bottom.Height = 5;
            //bottom.Y = bottom.Y + HitBox().Height - 5;

            //Rectangle left = HitBox();
            //left.Width = 2;
            //left.Y = HitBox().Y + 10;
            //left.Height = HitBox().Height - 20;

            //Rectangle right = HitBox();
            //right.X = right.X + right.Width - 2;
            //right.Width = 2;
            //right.Y = HitBox().Y + 10;
            //right.Height = HitBox().Height - 20;

            //spriteBatch.Draw(GameManager.temptex, left, Color.Red);
            //spriteBatch.Draw(GameManager.temptex, right, Color.Green);
            //spriteBatch.Draw(GameManager.temptex, top, Color.Blue);
            //spriteBatch.Draw(GameManager.temptex, bottom, Color.Pink);
        }
    }
}
