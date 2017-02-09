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
                    tileGrid[i, j] = new Tile(tileTex, new Vector2((startX+i) * size, (startY+j) * size), size);
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
