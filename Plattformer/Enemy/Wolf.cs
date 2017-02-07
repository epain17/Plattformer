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
        Movement currentMovement = Movement.patrol;

        Vector2 accelaration;
        Vector2 position;
        Vector2 velocity;

        Rectangle sourceRect;
        Texture2D tex, tex2;

        Queue<Vector2> waypoints = new Queue<Vector2>();
        Point startPoint, endPoint, previous;
        Pathfinder pathfinder;
        Point pr, p1, p2;
        Random random;

        int x, y;

        double searchTimer, searchTimerReset;
        double patrolTimer, patrolTimerReset;
        double energy, energyTimer, energyTimerReset;

        float speed;
        float speedS;
        int aggroRange = 200;
        int frame;
        int startHp;
        bool changeDirection = false;
        bool test = true;

        SpriteEffects wolfFx;

        public Wolf(Texture2D tex, Texture2D tex2, int x, int y)
        {
            this.tex = tex;
            this.tex2 = tex2;

            sourceRect = new Rectangle(0, 50, 96, 37);
            position = new Vector2(x, y);
            accelaration = Vector2.Zero;
            wolfFx = SpriteEffects.None;

            searchTimer = 200;
            searchTimerReset = 200;

            patrolTimer = 1000;
            patrolTimerReset = 1000;

            energyTimer = 5000;
            energyTimerReset = 5000;

            speed = 0f;
            energy = 30;

            x = 0;
            y = 0;

            random = new Random();

        }

        public void Update(GameTime gameTime, Point target, Point sleepPoint, TileGrid grid)
        {

            test = false;


            switch (currentMovement)
            {

                case Movement.attack:
                    if (grid.CheckWalkable(target.X, target.Y) == 0 && searchTimer < 0)
                    {
                        FindPath(target, grid);
                        searchTimer = searchTimerReset;

                    }

                    else if (FoundPlayer(target) == 2)
                    {
                        currentMovement = Movement.patrol;
                    }
                    else if (energy < 0 && FoundPlayer(target) != 1)
                    {
                        FindPath(sleepPoint, grid);
                        currentMovement = Movement.sleep;
                    }
                    searchTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    speed = 60f;
                    if (waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePosition(waypoints.Peek(), (float)gameTime.ElapsedGameTime.TotalSeconds);
                    } 
                    position += (velocity + accelaration) / 2;

                    break;

                case Movement.patrol:

                    if (patrolTimer < 0)
                    {
                        SetPatroll(p1, grid);
                        FindPath(p1, grid);
                    }

                    if (energy <= 0 && FoundPlayer(target) != 1)
                    {
                        FindPath(sleepPoint, grid);
                        currentMovement = Movement.sleep;
                    }

                    else if (FoundPlayer(target) == 1)
                    {
                        waypoints.Clear();
                        currentMovement = Movement.attack;
                    }
                    patrolTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    speed = 40f;
                    if (waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePosition(waypoints.Peek(), (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    position += (velocity + accelaration) / 2;

                    break;

                case Movement.sleep:

                    if (FoundPlayer(target) == 1)
                    {
                        energy = 0;
                        energyTimer = energyTimerReset;
                        currentMovement = Movement.attack;
                    }

                    if (waypoints.Count() == 0)
                    {
                        energyTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (energyTimer < 0)
                        {
                            energy = 30;
                            energyTimer = energyTimerReset;
                            currentMovement = Movement.patrol;
                        }
                    }
                    speed = 40f;
                    if(waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePosition(waypoints.Peek(), (float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                   // UpdatePos();
                    position += (velocity + accelaration) / 2;


                    break;

                case Movement.flee:

                    break;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //spriteBatch.Draw(tex2, position, sourceRect, Color.Red, 0, new Vector2(), 1, SpriteEffects.None, 1);
            spriteBatch.Draw(tex2, position, Color.Red);
            //if (waypoints != null)
            //{

            //    foreach (Vector2 v in waypoints)
            //    {
            //        spriteBatch.Draw(tex2, new Vector2(v.X, v.Y), Color.White);
            //    }
            //}

        }

        public Rectangle HitBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, 60, 40);
        }

        public Vector2 myPosition
        {
            get { return new Vector2(position.X, position.Y); }
        }

        public Point myGridPoint
        {
            get { return new Point((int)position.X / 40, (int)position.Y / 40); }
        }

        private Point SetPatroll(Point point, TileGrid grid)
        {
            x = 0;
            y = 0;
            while (grid.CheckWalkable(x, y) == 1)
            {
                x = random.Next(1, grid.width);
                y = random.Next(1, grid.height);

            }

            return p1 = new Point(x, y);
        }


        float DistanceToWaypoint
        {
            get { return Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(waypoints.Peek().X, waypoints.Peek().Y)); }
        }

        public void FindPath(Point targetPoint, TileGrid grid)
        {
            if (waypoints != null)
            {
                //waypoints.Clear();
                if (waypoints.Count() == 0 && targetPoint != myGridPoint)
                {
                    pathfinder = new Pathfinder(grid);
                    waypoints.Clear();
                    startPoint = myGridPoint;
                    endPoint = targetPoint;
                    if(grid.CheckWalkable(previous.X, previous.Y) != 0 && previous.X != 0 && previous.Y != 0)
                    {
                        Console.WriteLine("fuck");
                    }
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
                        previous = new Point(((int)waypoints.Peek().X) / 40, ((int)waypoints.Peek().Y) / 40);
                        waypoints.Dequeue();
                        if (energy > 0)
                            --energy;
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

        private bool UpdatePosition(Vector2 goal, float elapsed)
        {
            if(position == goal)
            {
                return true;
            }
            Vector2 direction = Vector2.Normalize(goal - position);
            position += direction * speed * elapsed;
            if (Math.Abs(Vector2.Dot(direction, Vector2.Normalize(goal - position)) + 1) < 0.1f)
            {
                position = goal;
                waypoints.Dequeue();
            }


            return position == goal;

        }

        private int FoundPlayer(Point TP)
        {
            if (Range(TP) < aggroRange)
            {
                return 1;
            }

            else if (Range(TP) > aggroRange && myGridPoint != pr)
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

