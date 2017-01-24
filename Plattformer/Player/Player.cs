using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{

    class Player : GameObject
    {
        //Directions
        enum Direction
        {
            still,
            jump,
            right,
            left
        }
        Direction currentDir = Direction.still;

        //Player related Bools
        public bool alive = true;

        public bool hasJumped = false;
        public bool standingStill = true;
        public bool run = false;

        //Player speed and gravity
        Vector2 speed;
        float gravity = 0.5f;

        //Player Textures and Animation        
        SpriteEffects playerFx;
        private double frameTimer, frameInterval;
        private int frame;


        //Player HitBox
        public override Rectangle HitBox()
        {
            return new Rectangle((int)drawPos.X + 10, (int)drawPos.Y, 20, 40);
        }

        public Point myPoint
        {
            get { return new Point((int)drawPos.X / 40, (int)drawPos.Y / 40); }
        }

        //Constructor
        public Player(Texture2D tex, int x, int y)
            : base(tex, x, y)
        {
            sourceRect = new Rectangle(0, 0, 40, 40);
            frameTimer = 110;
            frameInterval = 110;
            playerFx = SpriteEffects.None;
        }

        public void SpikeHit()
        {
            drawPos = new Vector2(200, 440);
        }

        public void Teleport()
        {
            drawPos = new Vector2(3760, 80);
        }

        //Collision handle with Platforms
        public override void HandelCollision(GameObject b, int n)
        {
            //Top
            if (n == 1)
            {
                alive = true;
                hasJumped = false;
                speed.Y = 0;
                drawPos.Y = b.HitBox().Y - HitBox().Height + 1;
            }

            //Bottom             
            if (n == 2)
            {
                hasJumped = true;
                speed.X = 0;
                speed.Y = 0.6f;
            }

            // Left            
            if (n == 3)
            {
                drawPos.X = drawPos.X - 4;
            }

            // Right            
            if (n == 4)
            {
                drawPos.X = drawPos.X + 4;
            }

        }

        public override void Update(GameTime gameTime)
        {
            speed.Y += gravity;

            //Animation and Texture switch
            if (standingStill == true)
            {
                sourceRect.X = 240;
                sourceRect.Y = 200;
            }

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0 && !hasJumped)
            {
                sourceRect.X = 0;
                sourceRect.Y = 0;
                frameTimer = frameInterval;
                sourceRect.X = (frame % 9) * 40;
            }

            else if (hasJumped == true)
            {
                sourceRect.X = 240;
                sourceRect.Y = 360;
            }

            while (KeyMouseReader.KeyPressed(Keys.C))
            {
                run = true;
                break;
            }

            //Input
            if (KeyMouseReader.KeyPressed(Keys.Left))
            {
                standingStill = false;
                currentDir = Direction.left;
                playerFx = SpriteEffects.FlipHorizontally;
            }

            else if (KeyMouseReader.KeyPressed(Keys.Right))
            {
                standingStill = false;
                currentDir = Direction.right;
                playerFx = SpriteEffects.None;
            }

            else
            {
                standingStill = true;
                currentDir = Direction.still;
            }


            if (KeyMouseReader.KeyPressed(Keys.Space) && hasJumped == false)
            {
                currentDir = Direction.jump;
            }

            drawPos += speed;
            switch (currentDir)
            {
                case Direction.still:
                    speed.X = 0;
                    run = false;
                    break;

                case Direction.right:
                    frame++;
                    speed.X = 3;

                    if (run == true)
                    {
                        speed.X = 4;
                    }
                    break;

                case Direction.left:
                    frame++;
                    speed.X = -3f;
                    if (run == true)
                    {
                        speed.X = -4;
                    }
                    break;

                case Direction.jump:
                    drawPos.Y -= 1;
                    speed.Y = -10f;
                    run = false;
                    hasJumped = true;
                    break;

            }
        }

        public void Fall()
        {
            hasJumped = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (standingStill == true)
            {
                sourceRect = new Rectangle(240, 200, 40, 40);
                spriteBatch.Draw(tex, drawPos, sourceRect, Color.White);
                //spriteBatch.Draw(Game1.temptex, HitBox(), Color.Red);
            }
            else if (run == true)
            {
                sourceRect = new Rectangle(0, 400, 40, 40);
                spriteBatch.Draw(tex, drawPos, sourceRect, Color.White, 0, new Vector2(), 1, playerFx, 1);

            }
            else
            {
                spriteBatch.Draw(tex, drawPos, sourceRect, Color.White, 0, new Vector2(), 1, playerFx, 1);
                // spriteBatch.Draw(Game1.temptex, HitBox(), Color.Red);

            }
        }

    }
}
