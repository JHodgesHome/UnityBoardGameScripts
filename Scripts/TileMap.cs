using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour
{
    public GameObject selectedUnit;

    List<Node> currentPath = null;

    public TileType[] tileTypes;
    public bool isCurrentRoom;

    int[,] tiles;
    Node[,] graph;

    void Start()
    {
        //selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        //selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().tileX = 1;
        selectedUnit.GetComponent<Unit>().tileY = 1;
        selectedUnit.GetComponent<Unit>().map = this;

        CreateEntranceMap();
        //GenerateMapData(4, 2);
        //GeneratePathfindingGraph(4, 2);
        //GenerateMapVisual(4, 2);
        //CreateAbandonedNest();
    }

    void CreateEntranceMap()
    {
        int mapSizeX = 4;
        int mapSizeY = 2;

        GenerateMapData(mapSizeX, mapSizeY);

        tiles[0, 1] = 1;
        tiles[3, 1] = 1;
        tiles[1, 1] = 3;
        tiles[2, 1] = 3;

        GeneratePathfindingGraph(mapSizeX, mapSizeY);
        GenerateMapVisual(mapSizeX, mapSizeY);
    }

    public void ExploreNextRoom()
    {

    }

    void CreateAbandonedNest()
    {
        int mapSizeX = 4;
        int mapSizeY = 4;

        GenerateMapData(mapSizeX, mapSizeY);

        tiles[0, 0] = 1;
        tiles[3, 0] = 1;
        tiles[2, 2] = 1;
        tiles[1, 0] = 3;
        tiles[2, 0] = 3;
        tiles[3, 2] = 3;
        tiles[3, 3] = 3;

        GeneratePathfindingGraph(mapSizeX, mapSizeY);
        GenerateMapVisual(mapSizeX, mapSizeY);
    }

    void GenerateMapData(int mapSizeX, int mapSizeY)
    {
        tiles = new int[mapSizeX, mapSizeY];

        int x, y;

        for (x = 0; x < mapSizeX; x++)
        {
            for (y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }
    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {
        TileType tt = tileTypes[tiles[targetX, targetY]];

        float cost = tt.movementCost;

        //Check for diagonal movement
        if (sourceX != targetX && sourceY != targetY)
        {
            //Increase preference for straight line movement over diagonals
            cost += 0.001f;
        }

        return cost;
    }

    bool UnitCanWalkOnTile(int x, int y)
    {
        //TileType tt = tileTypes[tiles[x, y]];
        //return tt.isWalkable;
        return tileTypes[tiles[x, y]].isWalkable;
    }

    void GeneratePathfindingGraph(int mapSizeX, int mapSizeY)
    {
        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        //Adding neighbours to connect to current tile
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++) {

                ////four way connection
                //if (x > 0)
                //    //Add to the left
                //    graph[x, y].neighbours.Add(graph[x - 1, y]);
                //if (x < mapSizeX - 1)
                //    //Add to the right
                //    graph[x, y].neighbours.Add(graph[x + 1, y]);
                //if (y > 0)
                //    //Add to the bottom
                //    graph[x, y].neighbours.Add(graph[x, y - 1]);
                //if (y < mapSizeY - 1)
                //    //Add to the top
                //    graph[x, y].neighbours.Add(graph[x, y + 1]);

                //eight way connection
                if (x > 0)
                {
                    //Add to the left
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        //Add to the bottom left
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1)
                        //Add to the top left
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                if (x < mapSizeX - 1)
                {
                    //Add to the right
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        //Add to the bottom right
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1)
                        //Add to the top right
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }

                if (y > 0)
                    //Add to the bottom
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    //Add to the top
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
            }
        }
    }
    void GenerateMapVisual(int mapSizeX, int mapSizeY)
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                TileClickHandler ch = go.GetComponent<TileClickHandler>();
                ch.tileX = x;
                ch.tileY = y;
                ch.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    public void GeneratePathTo(int x, int y)
    {
        selectedUnit.GetComponent<Unit>().currentPath = null;
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.GetComponent<Unit>().tileX, 
                            selectedUnit.GetComponent<Unit>().tileY
                            ];

        Node target = graph[x, y];

        if (!UnitCanWalkOnTile(target.x, target.y))
        {
            Debug.Log("Unit unable to walk onto tile");
            return;
        }

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        while(unvisited.Count > 0)
        {
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if ((u == null || dist[possibleU] < dist[u]) && UnitCanWalkOnTile(possibleU.x,possibleU.y))
                    u = possibleU;
            }

            if (u == target)
            {
                break;
            }
            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while(curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
