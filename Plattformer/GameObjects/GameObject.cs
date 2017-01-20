using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    abstract class GameObject
    {
        protected Texture2D tex;
        protected Vector2 drawPos;
        protected Rectangle sourceRect;


        public Vector2 Pos
        {
            get { return drawPos; }
        }

        public GameObject(Texture2D tex, int x, int y)
        {
            this.tex = tex;
            this.drawPos = new Vector2(x, y);



        }

        public virtual Rectangle HitBox()
        {
            return new Rectangle((int)drawPos.X, (int)drawPos.Y, 40, 40);


        }

        public bool PixelCollision(Spike other)
        {
            if (HitBox().Intersects(other.HitBox()))
            {
                Color[] dataA = new Color[sourceRect.Width * sourceRect.Height];
                tex.GetData(0, sourceRect, dataA, 0, dataA.Length);
                Color[] dataB = new Color[other.tex.Width * other.tex.Height];
                other.tex.GetData(dataB);

                int top = Math.Max(HitBox().Top, other.HitBox().Top);
                int bottom = Math.Min(HitBox().Bottom, other.HitBox().Bottom);
                int left = Math.Max(HitBox().Left, other.HitBox().Left);
                int right = Math.Min(HitBox().Right, other.HitBox().Right);

                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        Color colorA = dataA[(x - HitBox().Left) + (y - HitBox().Top) * HitBox().Width];
                        Color colorB = dataB[(x - other.HitBox().Left) + (y - other.HitBox().Top) * other.HitBox().Width];
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public virtual int Collision(GameObject b)
        {
            Rectangle top = b.HitBox();
            top.Height = 10;


            Rectangle bottom = b.HitBox();
            bottom.Height = 5;
            bottom.Y = bottom.Y + b.HitBox().Height - 5;

            Rectangle left = b.HitBox();
            left.Width = 2;
            left.Y = b.HitBox().Y + 10;
            left.Height = b.HitBox().Height - 20;

            Rectangle right = b.HitBox();
            right.X = right.X + right.Width - 2;
            right.Width = 2;
            right.Y = b.HitBox().Y + 10;
            right.Height = b.HitBox().Height - 20;



            if (HitBox().Intersects(left))
            {
                return 3;
            }

            else if (HitBox().Intersects(right))
            {
                return 4;
            }
            if (HitBox().Intersects(top))
            {
                return 1;
            }

            if (HitBox().Intersects(bottom))
            {
                return 2;
            }
            return 0;
        }

        public virtual void HandelCollision(GameObject other, int n)
        {
            drawPos.X = HitBox().X;
            drawPos.Y = HitBox().Y;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, drawPos, Color.White);
        }

        ////public virtual void DrawHitbox(SpriteBatch spriteBatch, Texture2D hitboxTexture, Color color)
        //{
        //    spriteBatch.Draw(hitboxTexture, HitBox(), color);
        //}
    }

}
