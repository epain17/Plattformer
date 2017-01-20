using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    public class Camera
    {
        private Matrix transform;
        public Matrix Transform
        {
            get { return transform; }
        }

        private Vector2 centre;
        private Viewport viewport;

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        public void Update(Vector2 position, int xOffset, int yOffset)
        {
            centre = position;

            transform = Matrix.CreateTranslation(new Vector3(-centre.X + (viewport.Width / 2), -centre.Y - 80 + (viewport.Width / 2), 0));
        }
    }
}
