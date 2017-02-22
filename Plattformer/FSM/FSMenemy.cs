using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Enemy
{
    class FSMenemy : BaseEnemy
    {
        enum State
        {
            attack,
            sleep,
            patrol,
            flee,
        }
        State currentState = State.patrol;


        public FSMenemy(Texture2D texture, int startPositionX, int startPositionY, Point sleep) :
            base(texture, startPositionX, startPositionY, sleep)
        {
            this.texture = texture;
            this.position = new Vector2(startPositionX, startPositionY);
            this.sleepPoint = sleep;

            this.searchTimer = 0;
            this.searchTimerReset = 200;

            this.patrolTimer = 0;
            this.patrolTimerReset = 200;

            this.energyTimer = 5000;
            this.energyTimerReset = 5000;
            this.energy = 15;

            this.aggroRange = 200;
            this.speed = 0;
            this.random = new Random();

            this.patrolPointX = 0;
            this.patrolPointY = 0;

            this.enemyHP = 10;
            this.energy = 15;
        }

        public override void Update(GameTime gameTime, TileGrid grid, Point target)
        {
            HP();
            Console.WriteLine("FSMEnemy" + GetHP);
            switch (currentState)
            {
                case State.attack:
                    if (FoundPlayer(target) == 1 && searchTimer < 0 && GetHP > 5)
                    {

                        FindPath(target, grid);
                        searchTimer = searchTimerReset;
                    }

                    else if (FoundPlayer(target) == 2)
                    {
                        patrolTimer = patrolTimerReset;

                        currentState = State.patrol;
                    }

                    else if (FoundPlayer(target) != 1 && energy < 0)
                    {
                        waypoints.Clear();
                        FindPath(sleepPoint, grid);
                        currentState = State.sleep;
                    }
                    else if(GetHP < 5)
                    {
                        currentState = State.flee;
                    }


                    if (waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }

                    searchTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    speed = 110;

                    break;

                case State.patrol:

                    if (FoundPlayer(target) != 1 && energy <= 0|| GetHP < 2)
                    {
                        waypoints.Clear();
                        FindPath(sleepPoint, grid);
                        currentState = State.sleep;
                    }

                    else if (FoundPlayer(target) != 1 && patrolTimer <= 0)
                    {
                        SetPatroll(p1, grid);
                        FindPath(p1, grid);
                        patrolTimer = patrolTimerReset;
                    }

                    if (FoundPlayer(target) == 1 && GetHP > 5)
                    {
                        waypoints.Clear();
                        currentState = State.attack;
                    }

                    else if(FoundPlayer(target) == 1 && GetHP < 5)
                    {
                        waypoints.Clear();
                        currentState = State.flee;
                    }


                    patrolTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    speed = 55f;
                    break;

                case State.sleep:

                    if (FoundPlayer(target) == 1 && GetHP >5)
                    {
                        energy = 15;
                        energyTimer = energyTimerReset;
                        currentState = State.attack;
                    }
                    else if((FoundPlayer(target) == 1 && GetHP < 5))
                    {
                        energy = 10;
                        energyTimer = energyTimerReset;
                        currentState = State.flee;
                    }

                    if (waypoints.Count() == 0)
                    {
                        energyTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (energyTimer < 0)
                        {
                            energy = 15;
                            this.enemyHP = 10;
                            energyTimer = energyTimerReset;
                            patrolTimer = patrolTimerReset;

                            currentState = State.patrol;
                        }
                    }
                    speed = 90f;
                    if (waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }


                    break;

                case State.flee:

                    Point flee = EscapePoint(myGridPoint, target, grid);
                    FindPath(flee, grid);
                    speed = 110f;
                    if (FoundPlayer(target) == 2 || GetHP > 5)
                    {
                        patrolTimer = patrolTimerReset;

                        currentState = State.patrol;
                    }
                    if (waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }


                    break;
            }
            base.Update(gameTime, grid, target);

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
       
            spriteBatch.Draw(texture, position, Color.Blue);
            base.Draw(spriteBatch);
        }

      
    }
}
