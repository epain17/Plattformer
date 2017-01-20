using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plattformer
{
    internal class SearchNode
    {
        //Node pos
        internal Point Position;
        internal bool Walkable;
        internal SearchNode[] Neighbors;
        internal SearchNode Parent;
        internal bool InOpenList;
        internal bool InClosedList;
        internal float DistanceToGoal;
        internal float DistanceTraveled;
    }
    //class Pathfinder
    //{
    //    private SearchNode[,] searchNodes;
    //    private int levelWidth;
    //    private int levelHeight;
    //    private List<SearchNode> openList = new List<SearchNode>();
    //    private List<SearchNode> closedList = new List<SearchNode>();

    //    public Pathfinder(TileGrid grid)
    //    {
    //        levelWidth = grid.width;
    //        levelHeight = grid.height;
    //        InitializeSearchNodes(grid);
    //    }

    //    private float Heuristic(Point point1, Point point2)
    //    {
    //        return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
    //    }

    //    private void InitializeSearchNodes()
    //    {
    //        //Nodes får aldrig bli null;
    //        searchNodes = new SearchNode[levelWidth, levelHeight];
    //        for (int x = 0; x < levelWidth; x++)
    //        {
    //            for (int y = 0; y < levelHeight; y++)
    //            {
    //                SearchNode node = new SearchNode();
    //                node.Position = new Point(x, y);
    //                node.Walkable = grid.CheckWalkable(x, y) == 0;

    //                if (node.Walkable == true)
    //                {
    //                    node.Neighbors = new SearchNode[8];
    //                    searchNodes[x, y] = node;
    //                }
    //            }
    //        }

    //        for (int x = 0; x < levelWidth; x++)
    //        {
    //            for (int y = 0; y < levelHeight; y++)
    //            {
    //                SearchNode node = searchNodes[x, y];

    //                if (node == null || node.Walkable == false)
    //                {
    //                    continue;
    //                }

    //                Point[] neighbors = new Point[]
    //                {
    //                    new Point(x, y -1),
    //                    new Point(x, y + 1),
    //                    new Point(x -1, y),
    //                    new Point(x +1, y),
    //                    new Point(x +1, y -1),
    //                    new Point(x +1, y +1),
    //                    new Point(x -1, y -1),
    //                    new Point(x -1, y +1),
    //                };

    //                for (int i = 0; i < neighbors.Length; i++)
    //                {
    //                    Point position = neighbors[i];
    //                    if (position.X < 0 || position.X > levelWidth - 1 || position.Y < 0 || position.Y > levelHeight - 1)
    //                    {
    //                        continue;
    //                    }
    //                    SearchNode neighbor = searchNodes[position.X, position.Y];
    //                    if (neighbor == null || neighbor.Walkable == false)
    //                    {
    //                        continue;
    //                    }
    //                    node.Neighbors[i] = neighbor;
    //                }
    //            }
    //        }

    //    }

        //public Queue<Vector2> FindPath(Point startPoint, Point endPoint, Point previous)
        //{
        //    if (startPoint == endPoint)
        //    {
        //        return new Queue<Vector2>();
        //    }

        //    ResetSearchNode();

        //    if (endPoint.X > 0 && endPoint.Y > 0)
        //    {
        //        //sätt dynamiskt
        //        SearchNode startNode = searchNodes[startPoint.X, startPoint.Y];
        //        SearchNode endNode = searchNodes[endPoint.X, endPoint.Y];

        //        if (startNode == null)
        //        {
        //            startNode = searchNodes[previous.X, previous.Y];
        //        }

        //        if (endNode == null)
        //        {
        //            endNode = searchNodes[previous.X, previous.Y];
        //        }

        //        if (startPoint == endPoint)
        //        {
        //            return new Queue<Vector2>();
        //        }

        //        if (startNode != null)
        //        {
        //            startNode.InOpenList = true;

        //            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
        //            startNode.DistanceTraveled = 0;

        //            openList.Add(startNode);

        //            while (openList.Count > 0)
        //            {
        //                SearchNode currentNode = FindBestNode();

        //                if (currentNode == null)
        //                {
        //                    break;
        //                }

        //                if (currentNode == endNode)
        //                {
        //                    // Trace our path back to the start.
        //                    return FindFinalPath(startNode, endNode);
        //                }

        //                for (int i = 0; i < currentNode.Neighbors.Length; i++)
        //                {
        //                    SearchNode neighbor = currentNode.Neighbors[i];

        //                    if (neighbor == null || neighbor.Walkable == false)
        //                    {
        //                        continue;
        //                    }

        //                    float distanceTraveled = currentNode.DistanceTraveled + 1;

        //                    float heuristic = Heuristic(neighbor.Position, endPoint);


        //                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
        //                    {
        //                        neighbor.DistanceTraveled = distanceTraveled;
        //                        neighbor.DistanceToGoal = distanceTraveled + heuristic;
        //                        neighbor.Parent = currentNode;
        //                        neighbor.InOpenList = true;
        //                        openList.Add(neighbor);
        //                    }
        //                    else if (neighbor.InOpenList || neighbor.InClosedList)
        //                    {
        //                        if (neighbor.DistanceTraveled > distanceTraveled)
        //                        {
        //                            neighbor.DistanceTraveled = distanceTraveled;
        //                            neighbor.DistanceToGoal = distanceTraveled + heuristic;
        //                            neighbor.Parent = currentNode;
        //                        }
        //                    }
        //                }
        //                openList.Remove(currentNode);
        //                currentNode.InClosedList = true;
        //            }
        //        }
        //        return new Queue<Vector2>();
        //    }
        //    return null;
        //}



        //private void ResetSearchNode()
        //{
        //    openList.Clear();
        //    closedList.Clear();

        //    for (int x = 0; x < levelWidth; x++)
        //    {
        //        for (int y = 0; y < levelHeight; y++)
        //        {
        //            SearchNode node = searchNodes[x, y];
        //            if (node == null)
        //            {
        //                continue;
        //            }
        //            node.InOpenList = false;
        //            node.InClosedList = false;
        //            node.DistanceTraveled = float.MaxValue;
        //            node.DistanceToGoal = float.MaxValue;
        //        }
        //    }
        //}

        //private SearchNode FindBestNode()
        //{
        //    SearchNode currentTile = openList[0];
        //    float smallestDistanceToGoal = float.MaxValue;

        //    for (int i = 0; i < openList.Count; i++)
        //    {
        //        if (openList[i].DistanceToGoal < smallestDistanceToGoal)
        //        {
        //            currentTile = openList[i];
        //            smallestDistanceToGoal = currentTile.DistanceToGoal;
        //        }
        //    }
        //    return currentTile;
        //}

        //private Queue<Vector2> FindFinalPath(SearchNode startNode, SearchNode endNode)
        //{
        //    closedList.Add(endNode);
        //    SearchNode parentTile = endNode.Parent;

        //    //Senaste
        //    if (parentTile == null)
        //    {
        //        return null;
        //    }

        //    while (parentTile != startNode)
        //    {
        //        closedList.Add(parentTile);
        //        parentTile = parentTile.Parent;
        //    }

        //    Queue<Vector2> finalPath = new Queue<Vector2>();
        //    for (int i = closedList.Count - 1; i >= 0; i--)
        //    {
        //        finalPath.Enqueue(new Vector2((closedList[i].Position.X * 32), (closedList[i].Position.Y * 32)));
        //    }
        //    return finalPath;
        //}


        //private int ManhattanDistance(SearchNode startNode, SearchNode endNode)
        //{
        //    return (Math.Abs(endNode.Position.X - startNode.Position.X) + Math.Abs(endNode.Position.Y - startNode.Position.Y));
        //}

        //List<SearchNode> IdentifySuccessors(SearchNode currentNode, SearchNode startNode, SearchNode endNode)
        //{
        //    foreach (SearchNode node in openList)
        //    {
        //        int dX = (node.Position.X - currentNode.Position.X);
        //        int dY = (node.Position.Y - currentNode.Position.Y);

        //        var jumpPoint = jump(currentNode.Position.X,
        //            currentNode.Position.Y, dX, dY, startNode, endNode);

        //        if (jumpPoint) closedList.Add(jumpPoint);
        //    }

        //    return closedList;

        //}

        //SearchNode Jump(int cnX, int cnY, int dX, int dY, SearchNode start, SearchNode end, TileGrid grid)
        //{
        //    int nextX = cnX + dX;
        //    int nextY = cnY + dY;

        //    if (grid.CheckWalkable(nextX, nextY) == 0)
        //    {
        //        return null;
        //    }

        //    if(next == end.Position.X && )
        //}
    }

