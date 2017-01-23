using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.IO;

namespace Plattformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Song backgroundMusic;
        private SoundEffect died;
        //Textfile read & Lists       
        List<GameObject> gameObjects = new List<GameObject>();
        List<String> strings = new List<String>();
        //Queue<Tile> tilesToGrid = new Queue<Tile>();
        List<Tile> tilesToGrid = new List<Tile>();

        Pathfinder pathfinder;

        Vector2 camPos;

        //GameObjects, Camera, Background
        Player player;
        Wolf wolf;
        Camera camera;
        BackGround backGround;
        Spike spike;
        Block block;
        Bonfire bon;
        Decoration dec;
        Teleport teleport;
        Tile tile;
        TileGrid tilegrid;

        //Textures 
        Texture2D playerTex, spikeTex, wolfTex, bonTex;
        Texture2D blockTex, blockTex2, blockTex3;
        Texture2D backDrop, vic;
        public static Texture2D temptex;

        //Victory bool
        bool victory = false;

        //Camera Realated ints
        int blockCounter = 0;
        int lastElement = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Window Size
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundMusic = Content.Load<Song>("Bloodborne OST - Hunter's Dream");
            MediaPlayer.Play(backgroundMusic);


            //Still Textures 
            blockTex = Content.Load<Texture2D>("wall1");
            blockTex2 = Content.Load<Texture2D>("wallG");
            blockTex3 = Content.Load<Texture2D>("wallCracked");
            backDrop = Content.Load<Texture2D>("backDrop");
            spikeTex = Content.Load<Texture2D>("spike");

            //Moving Textures
            playerTex = Content.Load<Texture2D>("darkSheet");
            wolfTex = Content.Load<Texture2D>("wolf1");
            bonTex = Content.Load<Texture2D>("bonfire");

            temptex = Content.Load<Texture2D>("vit");
            vic = Content.Load<Texture2D>("victory");
            //Loading Camera, Level, Background
            camera = new Camera(GraphicsDevice.Viewport);
            LoadLevel();
            pathfinder = new Pathfinder(tilegrid);
            backGround = new BackGround(Content, Window);

        }

        private void LoadLevel()
        {
            //Positions in Level
            StreamReader sr = new StreamReader(@"Content\Map.txt");
            while (!sr.EndOfStream)
            {
                strings.Add(sr.ReadLine());
            }
            sr.Close();

            lastElement = strings.Count;
            for (int i = 0; i < strings.Count; i++)
            {
                int counter = 0;
                for (int j = 0; j < strings[i].Length; j++)
                {
                    //Player
                    if (strings[i][j] == 'p')
                    {
                        player = new Player(playerTex, (j * 40), i * 40);
                        camPos.X = Window.ClientBounds.Width / 2;
                        camPos.Y = player.Pos.Y;
                        gameObjects.Add(player);
                    }
                    //Block
                    if (strings[i][j] == 'b')
                    {
                        block = new Block(blockTex, j * 40, i * 40);
                        gameObjects.Add(block);
                        counter++;
                    }

                    //Greenblock
                    if (strings[i][j] == 's')
                    {
                        dec = new Decoration(blockTex2, j * 40, i * 40);
                        gameObjects.Add(dec);
                    }

                    //Crackedblock
                    if (strings[i][j] == 'r')
                    {
                        dec = new Decoration(blockTex3, j * 40, i * 40);
                        gameObjects.Add(dec);
                    }

                    //Killblock
                    if (strings[i][j] == 'v')
                    {
                        spike = new Spike(blockTex3, j * 40, i * 40);
                        gameObjects.Add(spike);
                    }

                    //Spike
                    if (strings[i][j] == 'w')
                    {
                        spike = new Spike(spikeTex, j * 40, i * 40);
                        gameObjects.Add(spike);
                    }
                    //Wolf
                    if (strings[i][j] == 'e')
                    {
                        wolf = new Wolf(wolfTex, blockTex, j * 40, i * 40);
                        //gameObjects.Add(wolf);
                    }
                    //Bonfire
                    if (strings[i][j] == 'f')
                    {
                        bon = new Bonfire(bonTex, j * 40, i * 40);
                        gameObjects.Add(bon);
                    }
                    //Teleport
                    if (strings[i][j] == 't')
                    {
                        teleport = new Teleport(bonTex, j * 40, i * 40);
                        gameObjects.Add(teleport);
                    }
                    //TileGrid
                    if(strings[i][j] == 'g')
                    {
                        
                        tilegrid = new TileGrid(temptex, j, i, 40, 71, 20);
                    }
                }
                //
                if (counter > blockCounter)
                {
                    blockCounter = counter;
                }
            }
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update Background
            backGround.Update();

            //Collision Blocks            
            foreach (GameObject g in gameObjects)
            {
                g.Update(gameTime);
                if (g is Block)
                {
                    int n = player.Collision(g);
                    //int o = wolf.Collision(g);
                    if (n > 0)
                    {
                        player.HandelCollision(g, n);
                    }
                    //if (o > 0)
                    //{
                    //    wolf.HandelCollision(g, o);
                    //}
                }
                //Spike Collision
                if (g is Spike)
                {
                    if (player.PixelCollision(g as Spike))
                    {
                        player.SpikeHit();
                    }
                }
                //Bonfire Collision
                if (g is Bonfire)
                {

                    if (player.HitBox().Intersects(g.HitBox()))
                    {
                        victory = true;
                    }
                }
                //Teleport Collision
                if (g is Teleport)
                {
                    if (player.HitBox().Intersects(g.HitBox()))
                    {
                        player.Teleport();
                    }
                }
                //Wolf collision
                //if (g is Wolf)
                //{
                //    if (player.HitBox().Intersects(g.HitBox()))
                //    {
                //        if (player.Pos.Y < g.Pos.Y || player.run == true)
                //        {
                //            (g as Wolf).WolfKill();
                //        }
                //        else
                //        {
                //            player.SpikeHit();
                //        }
                //    }
                //}
            }

            wolf.Update(gameTime, player.myPoint, tilegrid);

            // Camera Update
            if (player.Pos.X > 0 + Window.ClientBounds.Width / 2 && player.Pos.X < blockCounter * 40)
                camPos.X = player.Pos.X;

            if (player.Pos.Y < -(Window.ClientBounds.Height / 2) + 520)
            {
                camPos.Y = player.Pos.Y + Window.ClientBounds.Height / 2;
            }
            else
            {
                camPos.Y = 520;
            }

            camera.Update(camPos, Window.ClientBounds.Width, Window.ClientBounds.Height);

            //Reseting of camera
            if (player.alive == true)
            {
                camPos.X = Window.ClientBounds.Width / 2;
                camPos.Y = player.Pos.Y;
            }

            KeyMouseReader.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //Draw Background
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backDrop, Vector2.Zero, Color.White);
            backGround.Draw(spriteBatch);
            spriteBatch.End();

            //Draw Camera and Gameobjects
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            tilegrid.Draw(spriteBatch);
            wolf.Draw(spriteBatch);
            foreach (GameObject g in gameObjects)
            {
                g.Draw(spriteBatch);
            }

            if (victory == true)
            {
                spriteBatch.Draw(vic, new Vector2(player.Pos.X - 300, player.Pos.Y - 100), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}

