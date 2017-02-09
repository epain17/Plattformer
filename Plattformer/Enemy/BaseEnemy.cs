using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Plattformer.Enemy
{
    class BaseEnemy
    {
        protected Vector2 position;
        protected Point sleepPoint;

        protected Rectangle hitbox;
        protected Texture2D texture;
        protected int aggroRange;
        protected int energy;
        protected float speed;


        //Pathifinding realterat
        protected Pathfinder pathfinder;
        protected Queue<Vector2> waypoints = new Queue<Vector2>();
        protected Point startPoint, endPoint, previous;
        protected Point pr, p1;
        protected Random random;

        protected double searchTimer, searchTimerReset;
        protected double patrolTimer, patrolTimerReset;
        protected double energyTimer, energyTimerReset;

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

        }

        public virtual void Update(GameTime gameTime, TileGrid grid, Point target)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Rectangle Hitbox
        {
            get { return hitbox = new Rectangle((int)position.X, (int)position.Y, 40, 40); }
        }

        protected Vector2 MyPosition
        {
            get { return position; }
        }

        protected Point myGridPoint
        {
            get { return new Point((int)position.X / 40, (int)position.Y / 40); }
        }

        protected Point SetPatroll(Point patrolPoint, TileGrid tileGrid)
        {
            patrolPointX = 0;
            patrolPointY = 0;
            while(tileGrid.Check(patrolPointX, patrolPointY) == 1)
            {             
                patrolPointX = random.Next(1, tileGrid.width);
                patrolPointY = random.Next(1, tileGrid.height);
            }
           

            return p1 = new Point(patrolPointX, patrolPointY);
        }

        protected float DistanceToWayPoint
        {
            get { return Vector2.Distance(new Vector2(position.X, position.Y), new Vector2(waypoints.Peek().X, waypoints.Peek().Y)); }

        }

        protected void FindPath(Point targetPoint, TileGrid tileGrid)
        {
            if (targetPoint.Y == 14)
            {
                Console.WriteLine();
            }
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

        protected bool UpdatePostion(Vector2 goal, float elapsed)
        {
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


            return position == goal;
        }

        protected float Range(Point point)
        {
            Vector2 range = new Vector2(point.X * 40, point.Y * 40);
            return Vector2.Distance(this.position, range);
        }

        protected int FoundPlayer(Point TP)
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
