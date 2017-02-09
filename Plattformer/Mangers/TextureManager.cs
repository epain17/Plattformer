using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    public class TextureManager
    {
        public static Texture2D playerTex { get; private set; }
        public static Texture2D spikeTex { get; private set; }
        public static Texture2D wolfTex { get; private set; }
        public static Texture2D bonTex { get; private set; }
        public static Texture2D blockTex { get; private set; }
        public static Texture2D blockTex2 { get; private set; }
        public static Texture2D blockTex3 { get; private set; }
        public static Texture2D backDrop { get; private set; }
        public static Texture2D vic { get; private set; }
        public static Texture2D temptex { get; private set; }



        public void Load(ContentManager Content)
        {
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

        }
    }
}
