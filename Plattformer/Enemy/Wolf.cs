using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class Wolf : GameObject
    {

        enum Movement
        {
            walkRight,
            walkLeft,
            attackLeft,
            attackRight,
            fleeLeft,
            fleeWritght,
        }
        Movement currentMovement = Movement.walkLeft;

        Vector2 speed;

        //Wolf Textures and Animation
        SpriteEffects wolfFx;
        private double frameTimer, frameInterval;
        private int frame;
        private bool changeDirection = false;

        protected float speedS;
        protected int startHp;
        //Pathfinder pathfinder;
        Point startPoint, endPoint, previous;
        protected Queue<Vector2> waypoints = new Queue<Vector2>();
        private Point pr;


        protected int aggroRange = 400;

        //Wolf Hitbox
        public override Rectangle HitBox()
        {
            return new Rectangle((int)drawPos.X, (int)drawPos.Y, 60, 40);
        }

        public Wolf(Texture2D tex, int x, int y) : base(tex, x, y)
        {
            this.tex = tex;
            sourceRect = new Rectangle(0, 50, 96, 37);
            frameTimer = 175;
            frameInterval = 175;

            wolfFx = SpriteEffects.None;
        }


        public override void HandelCollision(GameObject g, int o)
        {
            //Top
            if (o == 1)
            {
                speed.Y = 0;
                drawPos.Y = g.HitBox().Y - HitBox().Height + 1;
            }

            //Left
            if (o == 3)
            {
                drawPos.X = drawPos.X - 1;
                changeDirection = true;

            }

            //Right
            if (o == 4)
            {
                drawPos.X = drawPos.X + 1;
                changeDirection = false;
            }


        }

        public override void Update(GameTime gameTime)
        {
            //Animation and Texture switch

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                sourceRect.X = (frame % 5) * 95;
            }

            if (changeDirection == false)
            {
                currentMovement = Movement.walkRight;
                wolfFx = SpriteEffects.None;
            }

            if (changeDirection == true)
            {
                currentMovement = Movement.walkLeft;
                wolfFx = SpriteEffects.FlipHorizontally;
            }

            drawPos += speed;
            switch (currentMovement)
            {


                case Movement.walkRight:
                    speed.X = 1;
                    frame++;
                    break;

                case Movement.walkLeft:
                    speed.X = -1;
                    frame++;
                    break;
            }
        }

        public void WolfKill()
        {

            drawPos = new Vector2(2880, 360);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(tex, drawPos, sourceRect, Color.White, 0, new Vector2(), 1, wolfFx, 1);

        }
    //    float DistanceToWaypoint
    //    {
    //        get { return Vector2.Distance(position, waypoints.Peek()); }
    //    }

    //    public void FindPath(Point targetPoint, TileGrid grid)
    //    {
    //        if (waypoints != null)
    //        {
    //            if (waypoints.Count() == 0 && targetPoint != myPoint)
    //            {
    //                pathfinder = new Pathfinder(grid);
    //                waypoints.Clear();
    //                startPoint = myPoint;
    //                endPoint = targetPoint;
    //                waypoints = pathfinder.FindPath(startPoint, endPoint, previous);
    //            }

    //        }

    //    }

    //    protected virtual void UpdatePos()
    //    {
    //        if (waypoints != null)
    //        {
    //            if (waypoints.Count > 0)
    //            {
    //                if (DistanceToWaypoint < 1.5f)
    //                {
    //                    position = waypoints.Peek();
    //                    previous = new Point((int)waypoints.Peek().X / mySize, (int)waypoints.Peek().Y / mySize);
    //                    waypoints.Dequeue();
    //                }
    //                else
    //                {
    //                    Vector2 direction = waypoints.Peek() - position;
    //                    direction.Normalize();
    //                    velocity = Vector2.Multiply(direction, speed);
    //                }
    //            }
    //            else
    //                velocity = Vector2.Zero;
    //        }
    //    }

    //    private int FoundPlayer(Point TP)
    //    {
    //        if (Range(TP) < aggroRange)
    //        {
    //            return 1;
    //        }

    //        else if (Range(TP) > aggroRange && myPoint != pr)
    //        {
    //            return 2;
    //        }

    //        return 0;
    //    }

    //    protected float Range(Point point)
    //    {
    //        Vector2 range = new Vector2(point.X * size, point.Y * size);
    //        return Vector2.Distance(this.position, range);
    //    }
    }


}
