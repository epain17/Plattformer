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

            this.patrolTimer = 200;
            this.patrolTimerReset = 200;

            this.energyTimer = 5000;
            this.energyTimerReset = 5000;
            this.energy = 30;

            this.aggroRange = 300;
            this.speed = 0;
            this.random = new Random();

            this.patrolPointX = 0;
            this.patrolPointY = 0;
        }

        public override void Update(GameTime gameTime, TileGrid grid, Point target)
        {

            switch (currentState)
            {
                case State.attack:
                    if (FoundPlayer(target) == 1 && searchTimer < 0)
                    {

                        FindPath(target, grid);
                        searchTimer = searchTimerReset;
                    }

                    else if (FoundPlayer(target) == 2)
                    {
                        currentState = State.patrol;
                    }

                    else if (FoundPlayer(target) != 1 && energy < 0)
                    {
                        waypoints.Clear();
                        FindPath(sleepPoint, grid);
                        currentState = State.sleep;
                    }

                    if(KeyMouseReader.KeyPressed(Microsoft.Xna.Framework.Input.Keys.A))
                    {
                        waypoints.Clear();
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

                    if (FoundPlayer(target) != 1 && energy <= 0)
                    {
                        waypoints.Clear();
                        FindPath(sleepPoint, grid);
                        currentState = State.sleep;
                    }

                    else if (FoundPlayer(target) == 1)
                    {
                        waypoints.Clear();
                        currentState = State.attack;
                    }

                    if (FoundPlayer(target) == 2 && patrolTimer <= 0)
                    {
                        SetPatroll(p1, grid);
                        FindPath(p1, grid);
                    }

                    patrolTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (waypoints.Count() != 0 && waypoints != null)
                    {
                        UpdatePostion((float)gameTime.ElapsedGameTime.TotalSeconds);
                    }
                    speed = 60f;
                    break;

                case State.sleep:

                    if (FoundPlayer(target) == 1)
                    {
                        energy = 30;
                        energyTimer = energyTimerReset;
                        currentState = State.attack;
                    }

                    if (waypoints.Count() == 0)
                    {
                        energyTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                        if (energyTimer < 0)
                        {
                            energy = 30;
                            energyTimer = energyTimerReset;
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
                    if(FoundPlayer(target) == 2)
                    {
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
