using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Plattformer.Enemy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class GameManager
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice gd;
        SpriteBatch spriteBatch;
        GameWindow gameWindow;

        //GameObjects, Background
        Player player;
        Wolf wolf;
        FSMenemy FsmEnemy;
        BackGround backGround;
        Spike spike;
        Block block;
        Bonfire bon;
        Decoration dec;
        Teleport teleport;
        TileGrid tilegrid;
        Tile blockTile;
       
        //Textures 
        Texture2D playerTex, spikeTex, wolfTex, bonTex;
        Texture2D blockTex, blockTex2, blockTex3;
        Texture2D backDrop, vic;
        public static Texture2D temptex;
     
        //Camera Realated & pathfinder
        Camera camera;
        Vector2 camPos;
        Pathfinder pathfinder;

        //Textfile read & Lists        
        List<Tile> tiles = new List<Tile>();
        List<GameObject> gameObjects = new List<GameObject>();
        List<String> strings = new List<String>();
        List<Tile> tilesToGrid = new List<Tile>();

  

        int blockCounter = 0;
        int lastElement = 0;
   

        public GameManager(GraphicsDevice gd, SpriteBatch sb, GraphicsDeviceManager graphics, GameWindow gameWindow)
        {
            this.gd = gd;
            this.graphics = graphics;
            this.gameWindow = gameWindow;
            spriteBatch = sb;

        }

        public void Load(ContentManager Content)
        {
           
            //Still Textures 
            blockTex = Content.Load<Texture2D>("wall1");
            blockTex2 = Content.Load<Texture2D>("wallG");
            blockTex3 = Content.Load<Texture2D>("wallCracked");
            backDrop = Content.Load<Texture2D>("backDrop");
            spikeTex = Content.Load<Texture2D>("spike");

            //Moving Textures
            playerTex = Content.Load<Texture2D>("darkSheet");
            //wolfTex = Content.Load<Texture2D>("wolf1");
            bonTex = Content.Load<Texture2D>("bonfire");

            temptex = Content.Load<Texture2D>("vit");
            vic = Content.Load<Texture2D>("victory");

            //Loading Camera, Level, Background
            camera = new Camera(gd.Viewport);
            LoadLevel();
            tilegrid = new TileGrid(temptex, 0, 0, 40, 20, 13);
            SetGrid(tilegrid);
            pathfinder = new Pathfinder(tilegrid);
            backGround = new BackGround(Content, gameWindow);

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
                        player = new Player(blockTex, (j * 40), i * 40);
                        camPos.X = 1000 / 2;
                        camPos.Y = player.Pos.Y;
                        gameObjects.Add(player);
                    }
                    //Block
                    if (strings[i][j] == 'b')
                    {
                        block = new Block(blockTex, j * 40, i * 40);
                        blockTile = new Plattformer.Tile(blockTex, new Vector2(j * 40, i * 40), 40);
                        blockTile.iamOccupied = true;
                        gameObjects.Add(block);
                        tiles.Add(blockTile);
                        counter++;
                    }
            
                    //Wolf
                    if (strings[i][j] == 'e')
                    {
                        wolf = new Wolf(wolfTex, blockTex, j * 40, i * 40);
                        FsmEnemy = new FSMenemy(blockTex, j * 40, i * 40, new Point(j, i));
                    }                      
                }
                
                if (counter > blockCounter)
                {
                    blockCounter = counter;
                }
            }
        }

        private void SetGrid(TileGrid grid)
        {
            foreach(Tile t in tiles)
            {

                grid.SetOccupiedGrid(t);
            }
        }

        public void Update(GameTime gameTime)
        {
            //Update Background
            backGround.Update();
            player.Update(gameTime, tilegrid);
            //wolf.Update(gameTime, player.myPosition, new Point(3, 10), tilegrid);
            FsmEnemy.Update(gameTime, tilegrid, player.myPosition);

            // Camera Update
            if (player.Pos.X > 0 + gameWindow.ClientBounds.Width / 2 && player.Pos.X < blockCounter * 40)
                camPos.X = player.Pos.X;

            if (player.Pos.Y < -(gameWindow.ClientBounds.Height / 2) + 520)
            {
                camPos.Y = player.Pos.Y + gameWindow.ClientBounds.Height / 2;
            }
            else
            {
                camPos.Y = 520;
            }

            camera.Update(camPos, gameWindow.ClientBounds.Width, gameWindow.ClientBounds.Height);

            //Reseting of camera
            if (player.alive == true)
            {
                camPos.X = gameWindow.ClientBounds.Width / 2;
                camPos.Y = player.Pos.Y;
            }

            KeyMouseReader.Update();
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw Background
            spriteBatch.Draw(backDrop, Vector2.Zero, Color.White);
            backGround.Draw(spriteBatch);
            spriteBatch.End();

            //Draw Camera and Gameobjects
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
            tilegrid.Draw(spriteBatch);
            //wolf.Draw(spriteBatch);
            FsmEnemy.Draw(spriteBatch);
            foreach (GameObject g in gameObjects)
            {
                g.Draw(spriteBatch);
            }
        }
    }
}
