﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Plattformer.Enemy;
using Microsoft.Xna.Framework.Input;

namespace Plattformer
{
    class BaseEnemy
    {
        protected Vector2 position;
        public Point sleepPoint;

        protected Rectangle hitbox;
        protected Texture2D texture;
        protected int aggroRange;
        public int energy;
        protected int playerHP;
        protected float speed;
        public int enemyHP;

        //Pathifinding realterat
        protected Pathfinder pathfinder;
        public Queue<Vector2> waypoints = new Queue<Vector2>();
        protected Point startPoint, endPoint, previous, target;
        public Point pr, p1;
        protected Random random;

        //Timer
        protected double searchTimer, searchTimerReset;
        public double patrolTimer, patrolTimerReset;
        public double energyTimer, energyTimerReset;

        protected int patrolPointX, patrolPointY;

        public BaseEnemy(Texture2D texture, int startPositionX, int startPositionY, Point sleep)
        {
            this.texture = texture;
            position = new Vector2(startPositionX, startPositionY);
            sleepPoint = sleep;

            searchTimer = 200;
            searchTimerReset = 200;

            patrolTimer = 1000;
            patrolTimerReset = 1000;

            energyTimer = 5000;
            energyTimerReset = 5000;
            energy = 30;

            speed = 0;
            random = new Random();

            patrolPointX = 0;
            patrolPointY = 0;
            playerHP = 0;

        }
        public virtual void Update(GameTime gameTime, TileGrid grid, Point target)
        {
            this.target = target;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        //Gets and Set
        public Vector2 myPosition
        {
            get { return position; }
        }
        public Point myGridPoint
        {
            get { return new Point((int)position.X / 40, (int)position.Y / 40); }
        }
        public Point GetTarget
        {
            get { return target; }
        }
        public Rectangle Hitbox
        {
            get { return hitbox = new Rectangle((int)position.X, (int)position.Y, 40, 40); }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        
        public int PlayerHP
        {
            get { return playerHP; }
        }
        protected float DistanceToWayPoint
        {
            get { return Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(waypoints.Peek().X, waypoints.Peek().Y)); }

        }
        public int GetHP
        {
            get { return enemyHP; }
        }

        protected int HP()
        {
            if (KeyMouseReader.KeyPressed(Keys.O) && KeyMouseReader.oldKeyState.IsKeyUp(Keys.O))
            {
                return enemyHP--;
            }

            else if (KeyMouseReader.KeyPressed(Keys.P) && KeyMouseReader.oldKeyState.IsKeyUp(Keys.P))
            {

                return enemyHP++;
            }

            return enemyHP;
        }
        public int GetRange
        {
            get { return aggroRange; }
        }


        //Pathfinding
        public void FindPath(Point targetPoint, TileGrid tileGrid)
        {
            if (waypoints != null)
            {
                if (waypoints.Count() == 0 && targetPoint != myGridPoint)
                {
                    pathfinder = new Pathfinder(tileGrid);
                    waypoints.Clear();
                    startPoint = myGridPoint;
                    endPoint = targetPoint;
                    waypoints = pathfinder.FindPath(startPoint, endPoint, previous);
                }
            }
        }
        public bool UpdatePostion(float elapsed)
        {
            //ändrade här med goal
            Vector2 goal = Vector2.Zero;
            if (waypoints.Count() != 0 && waypoints != null)
            {
                goal = waypoints.Peek();
                if (position == goal)
                {
                    waypoints.Dequeue();
                    return true;
                }
                Vector2 direction = Vector2.Normalize(goal - position);
                position += direction * speed * elapsed;
                if (Math.Abs(Vector2.Dot(direction, Vector2.Normalize(goal - position)) + 1) < 0.1f)
                {
                    position = goal;
                    waypoints.Dequeue();
                    energy--;

                }

            }

            return position == goal;
        }

        //Avstånds mättningar
        protected float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }
        public float DistanceTo(Point target, Point currentPoint)
        {
            return Vector2.Distance(new Vector2(target.X, target.Y), new Vector2(currentPoint.X, currentPoint.Y));
        }

        /// <summary>
        /// För tile som flyttar ´längst ifrån spelaren 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public Point EscapePoint(Point point1, Point point2, TileGrid tileGrid)
        {
            Point r = Point.Zero;
            float weight = 0;


            if (tileGrid.Check(point1.X + 1, point1.Y) == 0)
            {
                if (weight < Heuristic(new Point(point1.X + 1, point1.Y), point2) + DistanceTo(new Point(point1.X + 1, point1.Y), point2))
                {
                    weight = Heuristic(new Point(point1.X + 1, point1.Y), point2) + DistanceTo(new Point(point1.X + 1, point1.Y), point2);
                    r = new Point(point1.X + 1, point1.Y);
                }

            }
            if (tileGrid.Check(point1.X - 1, point1.Y) == 0)
            {
                if (weight < Heuristic(new Point(point1.X - 1, point1.Y), point2) + DistanceTo(new Point(point1.X - 1, point1.Y), point2))
                {
                    weight = Heuristic(new Point(point1.X - 1, point1.Y), point2) + DistanceTo(new Point(point1.X - 1, point1.Y), point2);
                    r = new Point(point1.X - 1, point1.Y);
                }

            }
            if (tileGrid.Check(point1.X, point1.Y + 1) == 0)
            {
                if (weight < Heuristic(new Point(point1.X, point1.Y + 1), point2) + DistanceTo(new Point(point1.X, point1.Y + 1), point2))
                {
                    weight = Heuristic(new Point(point1.X, point1.Y + 1), point2) + DistanceTo(new Point(point1.X, point1.Y + 1), point2);
                    r = new Point(point1.X, point1.Y + 1);
                }

            }
            if (tileGrid.Check(point1.X, point1.Y - 1) == 0)
            {
                if (weight < Heuristic(new Point(point1.X, point1.Y - 1), point2) + DistanceTo(new Point(point1.X, point1.Y - 1), point2))
                {
                    weight = Heuristic(new Point(point1.X, point1.Y - 1), point2) + DistanceTo(new Point(point1.X, point1.Y - 1), point2);
                    r = new Point(point1.X, point1.Y - 1);
                }

            }

            if (r != Point.Zero)
            {
                waypoints.Clear();

            }
            return r;
        }

        //Sätta patrul punkt
        public Point SetPatroll(Point patrolPoint, TileGrid tileGrid)
        {
            patrolPointX = 0;
            patrolPointY = 0;
            while (tileGrid.Check(patrolPointX, patrolPointY) == 1)
            {
                patrolPointX = random.Next(1, tileGrid.width);
                patrolPointY = random.Next(1, tileGrid.height);
            }


            return p1 = new Point(patrolPointX, patrolPointY);
        }

        //Range metoder
        public float Range(Point point)
        {
            Vector2 range = new Vector2(point.X * 40, point.Y * 40);
            return Vector2.Distance(this.position, range);
        }
        public int FoundPlayer(Point TP)
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









    }
}