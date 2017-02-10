using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer.Enemy
{
    class TileGrid
    {
        public int width, height, startX, startY;
        Texture2D tileTex;
        Tile[,] tileGrid;
        int size;

        public TileGrid(Texture2D tileTex, int startX, int startY, int size, int columns, int rows)
        {

            this.tileTex = tileTex;
            this.size = size;
            this.startX = startX;
            this.startY = startY;
            this.width = columns;
            this.height = rows;

            CreateTileGrid();
        }

        public void CreateTileGrid()
        {

            tileGrid = new Tile[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tileGrid[i, j] = new Tile(tileTex, new Vector2((startX + i) * size, (startY + j) * size), size);
                }
            }

        }

        public int CheckWalkable(int cellX, int cellY)
        {
            for (int i = cellX; i < width; i++)
            {
                for (int j = cellY; j < height;)
                {
                    if (tileGrid[i, j].iamOccupied == false && tileGrid[i, j] != null)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        public int Check(int cellX, int cellY)
        {
            if (tileGrid[cellX, cellY].iamOccupied == false && tileGrid[cellX, cellY] != null)
            {
                return 0;
            }
            else
            {
                return 1;
            }


        }

        //för Pathfinding söking för punkt längst bort från player
        public Point FindEscapePoint(Point point)
        {
            Point r = Point.Zero;
            float weight = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (tileGrid[i, j].iamOccupied == false && tileGrid[i, j] != null)
                    {
                        if (weight < Heuristic(point, new Point(i, j)) + DistanceTo(point, new Point(i, j)))
                        {
                            weight = Heuristic(point, new Point(i, j)) + DistanceTo(point, new Point(i, j));
                            r = new Point(i, j);
                        }

                    }

                }

            }
            return r;
        }

        /// <summary>
        /// För tile som flyttar ´längst ifrån spelaren 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public Point EscapePoint(Point point1, Point point2)
        {
            Point r = Point.Zero;
            float weight = 0;


            if (tileGrid[point1.X + 1, point1.Y].iamOccupied == false && tileGrid[point1.X + 1, point1.Y] != null)
            {
                if (weight < Heuristic(new Point(point1.X +1, point1.Y), point2) + DistanceTo(new Point(point1.X + 1, point1.Y), point2))
                {
                    weight = Heuristic(new Point(point1.X + 1, point1.Y), point2) + DistanceTo(new Point(point1.X + 1, point1.Y), point2);
                    r = new Point(point1.X + 1, point1.Y);
                }

            }
            if (tileGrid[point1.X - 1, point1.Y].iamOccupied == false && tileGrid[point1.X - 1, point1.Y] != null)
            {
                if (weight < Heuristic(new Point(point1.X - 1, point1.Y), point2) + DistanceTo(new Point(point1.X - 1, point1.Y), point2))
                {
                    weight = Heuristic(new Point(point1.X - 1, point1.Y), point2) + DistanceTo(new Point(point1.X - 1, point1.Y), point2);
                    r = new Point(point1.X - 1, point1.Y);
                }

            }
            if (tileGrid[point1.X, point1.Y + 1].iamOccupied == false && tileGrid[point1.X, point1.Y +1] != null)
            {
                if (weight < Heuristic(new Point(point1.X, point1.Y + 1), point2) + DistanceTo(new Point(point1.X, point1.Y + 1), point2))
                {
                    weight = Heuristic(new Point(point1.X, point1.Y + 1), point2) + DistanceTo(new Point(point1.X, point1.Y + 1), point2);
                    r = new Point(point1.X, point1.Y + 1);
                }

            }
            if (tileGrid[point1.X, point1.Y - 1].iamOccupied == false && tileGrid[point1.X, point1.Y - 1] != null)
            {
                if (weight < Heuristic(new Point(point1.X, point1.Y - 1), point2) + DistanceTo(new Point(point1.X, point1.Y - 1), point2))
                {
                    weight = Heuristic(new Point(point1.X, point1.Y - 1), point2) + DistanceTo(new Point(point1.X, point1.Y - 1), point2);
                    r = new Point(point1.X, point1.Y - 1);
                }

            }

           
            return r;
        }

        private float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }
        
        private float DistanceTo(Point target, Point currentPoint)
        {
            return Vector2.Distance(new Vector2(target.X, target.Y), new Vector2(currentPoint.X, currentPoint.Y));
        }

        public void SetOccupiedGrid(Tile target)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (tileGrid[i, j].amIOccupied(target))
                        tileGrid[i, j].iamOccupied = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (tileGrid != null)
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        tileGrid[i, j].Draw(spriteBatch);
                    }
                }
        }
    }
}
