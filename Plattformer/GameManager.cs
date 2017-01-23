using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        //Camera Realated 
        Camera camera;
        Vector2 camPos;

        //Textfile read & Lists       
       
        List<GameObject> gameObjects = new List<GameObject>();
        List<String> strings = new List<String>();
        List<Tile> tilesToGrid = new List<Tile>();

        //GameObjects, Camera, Background
        Player player;
        Wolf wolf;
        BackGround backGround;
        Spike spike;
        Block block;
        Bonfire bon;
        Decoration dec;
        Teleport teleport;
        Tile tile;

        int blockCounter = 0;
        int lastElement = 0;


        public GameManager(GraphicsDevice gd, SpriteBatch sb, GraphicsDeviceManager graphics)
        {
            this.gd = gd;
            this.graphics = graphics;

            spriteBatch = sb;

        }

        public void Load(ContentManager Content)
        {




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
                        player = new Player(TextureManager.playerTex, (j * 40), i * 40);
                        camPos.X = 1000 / 2;
                        camPos.Y = player.Pos.Y;
                        gameObjects.Add(player);
                    }
                    //Block
                    if (strings[i][j] == 'b')
                    {
                        block = new Block(TextureManager.blockTex, j * 40, i * 40);
                        gameObjects.Add(block);
                        counter++;
                    }

                    //Greenblock
                    if (strings[i][j] == 's')
                    {
                        dec = new Decoration(TextureManager.blockTex2, j * 40, i * 40);
                        gameObjects.Add(dec);
                    }

                    //Crackedblock
                    if (strings[i][j] == 'r')
                    {
                        dec = new Decoration(TextureManager.blockTex3, j * 40, i * 40);
                        gameObjects.Add(dec);
                    }

                    //Killblock
                    if (strings[i][j] == 'v')
                    {
                        spike = new Spike(TextureManager.blockTex3, j * 40, i * 40);
                        gameObjects.Add(spike);
                    }

                    //Spike
                    if (strings[i][j] == 'w')
                    {
                        spike = new Spike(TextureManager.spikeTex, j * 40, i * 40);
                        gameObjects.Add(spike);
                    }
                    //Wolf
                    //if (strings[i][j] == 'e')
                    //{
                    //    wolf = new Wolf(TextureManager.wolfTex, j * 40, i * 40);
                    //   // gameObjects.Add(wolf);
                    //}
                    //Bonfire
                    if (strings[i][j] == 'f')
                    {
                        bon = new Bonfire(TextureManager.bonTex, j * 40, i * 40);
                        gameObjects.Add(bon);
                    }
                    //Teleport
                    if (strings[i][j] == 't')
                    {
                        teleport = new Teleport(TextureManager.bonTex, j * 40, i * 40);
                        gameObjects.Add(teleport);
                    }
                }
                //
                if (counter > blockCounter)
                {
                    blockCounter = counter;
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sb)
        {

        }
    }
}
