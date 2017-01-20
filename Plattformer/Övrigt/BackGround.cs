using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    class BackGround
    {
        List<Vector2> foreground, middleground, background;
        int fgSpacing, mgSpacing, bgSpacing;
        float fgSpeed, mgSpeed, bgSpeed;
        Texture2D[] tex;
        GameWindow window;

        public BackGround(ContentManager Content, GameWindow window)
        {
            this.tex = new Texture2D[3];
            this.window = window;

            tex[0] = Content.Load<Texture2D>("cloud_black");
            tex[1] = Content.Load<Texture2D>("cloud_black");
            tex[2] = Content.Load<Texture2D>("cloud_black");

            foreground = new List<Vector2>();
            fgSpacing = tex[0].Width;
            fgSpeed = 0.75f;

            for (int i = 0; i < (window.ClientBounds.Width / fgSpacing) + 2; i++)
            {
                foreground.Add(new Vector2(i * fgSpacing, window.ClientBounds.Height - tex[0].Height));
            }

            middleground = new List<Vector2>();
            mgSpacing = window.ClientBounds.Width / 3;
            mgSpeed = 0.5f;

            for (int i = 0; i < (window.ClientBounds.Width / mgSpacing) + 2; i++)
            {
                middleground.Add(new Vector2(i * mgSpacing, window.ClientBounds.Height - tex[0].Height - tex[1].Height));
            }

            background = new List<Vector2>();
            bgSpacing = window.ClientBounds.Width / 2;
            bgSpeed = 0.25f;
            for (int i = 0; i < (window.ClientBounds.Width / bgSpacing) + 2; i++)
            {
                background.Add(new Vector2(i * bgSpacing, window.ClientBounds.Height - tex[0].Height - (int)(tex[1].Height * 1.5)));
            }
        }

        public void Update()
        {
            for (int i = 0; i < foreground.Count; i++)
            {
                foreground[i] = new Vector2(foreground[i].X - fgSpeed, foreground[i].Y);
                if (foreground[i].X <= -fgSpacing)
                {
                    int j = i - 1; if (j < 0)
                    {
                        j = foreground.Count - 1;

                    }

                    foreground[i] = new Vector2(foreground[j].X + fgSpacing - 1, foreground[i].Y);
                }
            }

            for (int i = 0; i < middleground.Count; i++)
            {
                middleground[i] = new Vector2(middleground[i].X - mgSpeed, middleground[i].Y);
                if (middleground[i].X <= -mgSpacing)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = middleground.Count - 1;
                    }
                    middleground[i] = new Vector2(middleground[j].X + mgSpacing - 1, middleground[i].Y);
                }
            }

            for (int i = 0; i < background.Count; i++)
            {
                background[i] = new Vector2(background[i].X - bgSpeed, background[i].Y);
                if (background[i].X <= -bgSpacing)
                {
                    int j = i - 1;
                    if (j < 0)
                    {
                        j = background.Count - 1;
                    }
                    background[i] = new Vector2(background[j].X + bgSpacing - 1, background[i].Y);
                }
            }




        }

        public void Draw(SpriteBatch sb)
        {

            foreach (Vector2 v in background)
            {
                sb.Draw(tex[2], v, Color.White);
            }
            foreach (Vector2 v in middleground)
            {
                sb.Draw(tex[1], v, Color.White);
            }
            foreach (Vector2 v in foreground)
            {
                sb.Draw(tex[0], v, Color.White);
            }
        }
    }
}
