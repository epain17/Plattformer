﻿using Microsoft.Xna.Framework;
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
        Point point;
        float speed = 100.0f;
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
            get { return new Point(((int)drawPos.X) / 40, ((int)drawPos.Y) / 40); }
            set { myPosition = value; }
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

        float Distance
        {
            get { return Vector2.Distance(drawPos, newPos); }
        }

        private bool UpdatePostion(Vector2 goal, float elapsed)
        {
        
            if (drawPos == goal) return true;
            Vector2 direction = Vector2.Normalize(goal - drawPos);
            drawPos += direction * speed * elapsed;
            standingStill = false;
            if (Math.Abs(Vector2.Dot(direction, Vector2.Normalize(goal - drawPos)) + 1) < 0.1f)
            {
                drawPos = goal;
                standingStill = true;
            }


            return drawPos == goal;
        }

        public override void Update(GameTime gameTime, TileGrid grid)
        {

            Console.WriteLine(myPoint);

            if (velocity != Vector2.Zero)
            {
                drawPos += (velocity);

            }

            Animation(gameTime);

            KeyInput(grid);



            //drawPos += speed;
            switch (currentDir)
            {
                case Direction.still:
                    newPos = drawPos;
                    velocity = new Vector2(0, 0);
                    run = false;
                    break;

                case Direction.right:
                    frame++;
                    UpdatePostion(newPos, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;

                case Direction.left:
                    frame++;
                    UpdatePostion(newPos, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;

                case Direction.up:
                    frame++;
                    UpdatePostion(newPos, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;

                case Direction.down:
                    frame++;
                    UpdatePostion(newPos, (float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;

            }
        }

        private void KeyInput(TileGrid grid)
        {
            //Input
            if (KeyMouseReader.KeyPressed(Keys.Left) /*&& KeyMouseReader.oldKeyState != KeyMouseReader.keyState*/ && standingStill == true)
            {
                if (grid.Check(myPoint.X - 1, myPoint.Y) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2(drawPos.X - 40, drawPos.Y);

                    currentDir = Direction.left;
                    playerFx = SpriteEffects.FlipHorizontally;
                }
            }

            else if (KeyMouseReader.KeyPressed(Keys.Right) /*&& KeyMouseReader.oldKeyState != KeyMouseReader.keyState*/ && standingStill == true)
            {
                if (grid.Check(myPoint.X + 1, myPoint.Y) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2(drawPos.X + 40, drawPos.Y);

                    currentDir = Direction.right;
                    playerFx = SpriteEffects.None;
                }
            }

            else if (KeyMouseReader.KeyPressed(Keys.Up) /*&& KeyMouseReader.oldKeyState != KeyMouseReader.keyState*/ && standingStill == true)
            {
                if (grid.Check(myPoint.X, myPoint.Y - 1) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2((myPosition.X * 40), (myPosition.Y * 40) - 40);


                    currentDir = Direction.up;
                }
            }

            else if (KeyMouseReader.KeyPressed(Keys.Down) /*&& KeyMouseReader.oldKeyState != KeyMouseReader.keyState*/ && standingStill == true)
            {
                if (grid.Check(myPoint.X, myPoint.Y + 1) != 1)
                {
                    standingStill = false;
                    newPos = new Vector2((myPosition.X * 40), (myPosition.Y * 40) + 40);

                    currentDir = Direction.down;

                }
            }

            else
            {
                standingStill = true;
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
