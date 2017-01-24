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
    class Wolf
    {

        enum Movement
        {
            attack,
            sleep,
            patrol,
            flee,
        }
        Movement currentMovement = Movement.attack;

        Vector2 accelaration;
        Vector2 position;
        Vector2 velocity;

        Rectangle sourceRect;
        Texture2D tex, tex2;

        Queue<Vector2> waypoints = new Queue<Vector2>();
        Point startPoint, endPoint, previous;
        Pathfinder pathfinder;
        Point pr;

        double frameTimer, frameInterval;
        float speed;
        float speedS;
        int aggroRange = 400;
        int frame;
        int startHp;
        bool changeDirection = false;

        SpriteEffects wolfFx;

        public Wolf(Texture2D tex, Texture2D tex2, int x, int y)
        {
            this.tex = tex;
            this.tex2 = tex2;

            sourceRect = new Rectangle(0, 50, 96, 37);
            position = new Vector2(x, y);
            accelaration = Vector2.Zero;
            wolfFx = SpriteEffects.None;

            frameTimer = 175;
            frameInterval = 175;
            speed = 5f;

        }

        public void Update(GameTime gameTime, Point target, TileGrid grid)
        {

            switch (currentMovement)
            {

                case Movement.attack:

                    position += (velocity + accelaration) / 2;
                    FindPath(target, grid);
                    UpdatePos();
                    break;

                case Movement.sleep:

                    break;

                case Movement.patrol:

                    break;

                case Movement.flee:

                    break;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(tex, position, sourceRect, Color.White, 0, new Vector2(), 1, SpriteEffects.None, 1);

            //if (waypoints != null)
            //{

            //    //foreach (Vector2 v in waypoints)
            //    //{
            //    //    spriteBatch.Draw(tex2, new Vector2(v.X, v.Y), Color.White);
            //    //}
            //}

        }

        public Rectangle HitBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, 60, 40);
        }

        public Point myPoint
        {
            get { return new Point((int)position.X / 40, (int)position.Y / 40); }
        }

        public void WolfKill()
        {

            position = new Vector2(2880, 360);
        }


        //Pathfinding
        float DistanceToWaypoint
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public void FindPath(Point targetPoint, TileGrid grid)
        {
            if (waypoints != null)
            {
                waypoints.Clear();
                if (waypoints.Count() == 0 && targetPoint != myPoint)
                {
                    pathfinder = new Pathfinder(grid);
                    waypoints.Clear();
                    startPoint = myPoint;
                    endPoint = targetPoint;
                    waypoints = pathfinder.FindPath(startPoint, endPoint, previous);
                }

            }

        }

        protected virtual void UpdatePos()
        {
            if (waypoints != null)
            {
                if (waypoints.Count > 0)
                {
                    if (DistanceToWaypoint < 2f)
                    {
                        position = waypoints.Peek();
                        previous = new Point((int)waypoints.Peek().X / 40, (int)waypoints.Peek().Y / 40);
                        waypoints.Dequeue();
                    }
                    else
                    {
                        Vector2 direction = waypoints.Peek() - position;
                        direction.Normalize();
                        velocity = Vector2.Multiply(direction, speed);
                    }
                }
                else
                    velocity = Vector2.Zero;
            }
        }

        private int FoundPlayer(Point TP)
        {
            if (Range(TP) < aggroRange)
            {
                return 1;
            }

            else if (Range(TP) > aggroRange && myPoint != pr)
            {
                return 2;
            }

            return 0;
        }

        protected float Range(Point point)
        {
            Vector2 range = new Vector2(point.X * 40, point.Y * 40);
            return Vector2.Distance(this.position, range);
        }
    }


}
//public override void HandelCollision(GameObject g, int o)
//{
//    //Top
//    if (o == 1)
//    {
//        speed.Y = 0;
//        drawPos.Y = g.HitBox().Y - HitBox().Height + 1;
//    }

//    //Left
//    if (o == 3)
//    {
//        drawPos.X = drawPos.X - 1;
//        changeDirection = true;

//    }

//    //Right
//    if (o == 4)
//    {
//        drawPos.X = drawPos.X + 1;
//        changeDirection = false;
//    }


//}
//Animation and Texture switch

//frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
//if (frameTimer <= 0)
//{
//    frameTimer = frameInterval;
//    sourceRect.X = (frame % 5) * 95;
//}

//if (changeDirection == false)
//{
//    currentMovement = Movement.walkRight;
//    wolfFx = SpriteEffects.None;
//}

//if (changeDirection == true)
//{
//    currentMovement = Movement.walkLeft;
//    wolfFx = SpriteEffects.FlipHorizontally;
//}

//drawPos += speed;
//switch (currentMovement)
//{


//    case Movement.walkRight:
//        speed.X = 1;
//        frame++;
//        break;

//    case Movement.walkLeft:
//        speed.X = -1;
//        frame++;
//        break;
//}

