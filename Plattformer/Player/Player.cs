using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Plattformer.Enemy;
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
            left,
            up,
            down
        }
        Direction currentDir = Direction.still;

        //Player related Bools
        public bool alive = true;

        public bool hasJumped = false;
        public bool standingStill = true;
        public bool run = false;

        //Player speed and gravity
        Vector2 velocity;
        Vector2 newPos;
        float speed = 1.0f;
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

        public Point myPosition
        {
            get { return new Point(((int)drawPos.X + 20) / 40, ((int)drawPos.Y + 20) / 40); }
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
            //if (n == 1)
            //{
            //    alive = true;
            //    hasJumped = false;
            //    speed.Y = 0;
            //    drawPos.Y = b.HitBox().Y - HitBox().Height -2;
            //}

            ////Bottom             
            //if (n == 2)
            //{
            //    //hasJumped = true;                
            //    speed.Y = 0f;
            //    drawPos.Y = b.HitBox().Y + HitBox().Height + 4;
            //}

            //// Left            
            //if (n == 3)
            //{
            //    drawPos.X = drawPos.X - 4;
            //}

            //// Right            
            //if (n == 4)
            //{
            //    drawPos.X = drawPos.X + 4;
            //}

        }

        float Distance
        {
            get { return Vector2.Distance(drawPos, newPos); }
        }

        private void UpdatePostion(GameTime gameTime)
        {
            Vector2 direction = newPos - drawPos;
            direction.Normalize();

            if (Distance != 0)
            {
                velocity = Vector2.Multiply(direction, speed);

            }

            else
            {
                newPos = drawPos;
                currentDir = Direction.still;
            }


        }

        public override void Update(GameTime gameTime, TileGrid grid)
        {
            //speed.Y += gravity;

            //Animation and Texture switch

            Console.WriteLine(myPoint);

            drawPos += (velocity);

            Animation(gameTime);

            KeyInput(grid);



            //drawPos += speed;
            switch (currentDir)
            {
                case Direction.still:
                    //UpdatePostion(gameTime);

                    newPos = drawPos;
                    velocity = new Vector2(0, 0);
                    run = false;
                    break;

                case Direction.right:
                    frame++;
                    UpdatePostion(gameTime);           
                    break;

                case Direction.left:
                    frame++;        
                    UpdatePostion(gameTime);
                    break;

                case Direction.up:
                    frame++;         
                    UpdatePostion(gameTime);
                    break;

                case Direction.down:
                    frame++;            
                    UpdatePostion(gameTime);
                    break;

            }
        }

        private void Animation(GameTime gameTime)
        {
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
        }

        private void KeyInput(TileGrid grid)
        {
            //Input
            if (KeyMouseReader.KeyPressed(Keys.Left) && KeyMouseReader.oldKeyState != KeyMouseReader.keyState)
            {
                if (grid.Check(myPoint.X - 1, myPoint.Y) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2(drawPos.X - 40, drawPos.Y);

                    currentDir = Direction.left;
                    playerFx = SpriteEffects.FlipHorizontally;
                }
            }

            else if (KeyMouseReader.KeyPressed(Keys.Right) && KeyMouseReader.oldKeyState != KeyMouseReader.keyState)
            {
                if (grid.Check(myPoint.X + 1, myPoint.Y) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2(drawPos.X + 40, drawPos.Y);

                    currentDir = Direction.right;
                    playerFx = SpriteEffects.None;
                }
            }

            else if (KeyMouseReader.KeyPressed(Keys.Up) && KeyMouseReader.oldKeyState != KeyMouseReader.keyState)
            {
                if (grid.Check(myPoint.X, myPoint.Y - 1) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2(drawPos.X, drawPos.Y - 40);

                
                    currentDir = Direction.up;
                }
            }

            else if (KeyMouseReader.KeyPressed(Keys.Down) && KeyMouseReader.oldKeyState != KeyMouseReader.keyState)
            {
                if (grid.Check(myPoint.X, myPoint.Y + 1) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2(drawPos.X, drawPos.Y + 40);

                    currentDir = Direction.down;

                }
            }

            else
            {
                standingStill = true;
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
