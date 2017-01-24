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
   
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Song backgroundMusic;
        private SoundEffect died;
       
        GameManager gm;

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

            gm = new GameManager(GraphicsDevice, spriteBatch, graphics, Window);
            gm.Load(Content);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
         
            gm.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
        
            spriteBatch.Begin();
            gm.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}

