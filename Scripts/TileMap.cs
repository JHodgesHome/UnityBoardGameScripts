using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour
{
    public GameObject selectedUnit;

    List<Node> currentPath = null;

    public TileType[] tileTypes;
    public Room[] rooms;
    public GameObject nextRoomTemplate;
    public bool isCurrentRoom;
    public Vector3 nextRoomPosition;
    public int placementGatewayX, placementGatewayY;
    public Facing placementGatewayFacing;

    readonly int mapSize = 100;
    int[,] tiles = new int [100, 100];
    //int[,] tiles;
    Node[,] graph = new Node[100, 100];
    //Node[,] graph;

    void Start()
    {
        //selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        //selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().tileX = 50;
        selectedUnit.GetComponent<Unit>().tileY = 50;
        selectedUnit.GetComponent<Unit>().map = this;

        CreateWorldMap();
        CreateEntranceRoom();
        //GenerateMapData(4, 2);
        //GeneratePathfindingGraph(4, 2);
        //GenerateMapVisual(4, 2);
        //CreateAbandonedNest();
    }

    void CreateWorldMap()
    {
        GenerateMapData(mapSize);
        //GeneratePathfindingGraph(mapSize, mapSize);
        //GenerateMapVisual(mapSize, mapSize);
    }

    void CreateEntranceRoom()
    {
        int mapSizeX = 4;
        int mapSizeY = 2;
        int startingPointX = 49;
        int startingPointY = 49;

        GenerateMapData(mapSizeX, mapSizeY, startingPointX, startingPointY);

        tiles[startingPointX, startingPointY + 1] = 1;
        tiles[startingPointX + 3, startingPointY + 1] = 1;
        tiles[startingPointX + 1,startingPointY + 1] = 3;
        tiles[startingPointX + 2, startingPointY + 1] = 3;

        GeneratePathfindingGraph(mapSizeX, mapSizeY, startingPointX, startingPointY);
        GenerateMapVisual(mapSizeX, mapSizeY, startingPointX, startingPointY);
    }

    public string GetTileTypeName(int x, int y)
    {
        TileType tt = tileTypes[tiles[x, y]];
        return tt.name;
    }

    public void ExploreNextRoom()
    {
        int x = (int)selectedUnit.transform.position.x;
        int y = (int)selectedUnit.transform.position.y;
        Debug.Log("Selected Unit position: " + x + ", " + y);

        //TODO: Implement this so that it detects valid placement positions for the gateways
        //Change tint of room template when placement is valid or invalid
        //Change so that gateway tiles are held on mouseposition
        //Create tiles adjacent to existing gateways to allow placement on these


        //nextRoomTemplate = Instantiate(Resources.Load("Prefabs/NextRoom", typeof(GameObject))) as GameObject;
        //GameObject prefab;
        nextRoomTemplate = Resources.Load<GameObject>("Prefabs/NestRoom_EastSouth");
        RoomTemplate nextRoom = new RoomTemplate("Nest", 4, 4, 1, 0, Facing.South, 3, 2, Facing.East);
        Debug.Log("Gateway One x Position " + nextRoom.GatewayOneX);
        //Instantiate(nextRoomTemplate, new Vector3(x, y, 0), Quaternion.identity);
        //Debug.Log(prefab.transform);
        //GameObject nextRoomTemplate = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);

        //TODO: Put this into a separate method, so it can be called by the RoomPlacement Class

        if (tiles[x, y] == 3)
        {
            Debug.Log("Unit on open gateway");
            if (tiles[x - 1, y] == 3)
            {
                Debug.Log("Left adjacent square is also open gateway");
                if (tiles[x, y + 1] == 5)
                {
                    Debug.Log("Above tiles are room entrances");
                    placementGatewayX = x - 1;
                    placementGatewayY = y + 1;
                    placementGatewayFacing = Facing.South;
                    Instantiate(nextRoomTemplate, new Vector3(placementGatewayX - nextRoom.GatewayOneX, placementGatewayY, 0), Quaternion.identity);

                    //TODO: Add code to handle Room placement
                    //Check that the new room gateways are sitting on acceptable tiles
                    //Allow room rotation and check for collision with existing room
                    //Room will be placed with left click - then call generation code again to generate the room and its map data
                } 
                else if (tiles[x, y - 1] == 5)
                {
                    Debug.Log("Below tiles are room entrances");
                    placementGatewayX = x - 1;
                    placementGatewayY = y - 1;
                    placementGatewayFacing = Facing.North;
                    Instantiate(nextRoomTemplate, new Vector3((x - 1) - nextRoom.GatewayOneX, y - 1, 0), Quaternion.identity);
                }
            } 
            else if (tiles[x + 1, y] == 3)
            {
                Debug.Log("Right adjacent square is also open gateway");
                if (tiles[x, y + 1] == 5)
                {
                    Debug.Log("Above tiles are room entrances");
                    placementGatewayX = x;
                    placementGatewayY = y + 1;
                    placementGatewayFacing = Facing.South;
                    //Instantiate(nextRoomTemplate, new Vector3(x, y + 1, 0), Quaternion.identity);
                    Instantiate(nextRoomTemplate, new Vector3(x - nextRoom.GatewayOneX, y + 1, 0), Quaternion.identity);
                }
                else if (tiles[x, y - 1] == 5)
                {
                    Debug.Log("Below tiles are room entrances");
                    placementGatewayX = x;
                    placementGatewayY = y - 1;
                    placementGatewayFacing = Facing.North;
                    Instantiate(nextRoomTemplate, new Vector3(x - nextRoom.GatewayOneX, y - 1, 0), Quaternion.identity);
                }
            }
            else if (tiles[x, y - 1] == 3)
            {
                Debug.Log("Bottom adjacent square is also open gateway");
                if (tiles[x + 1, y] == 5)
                {
                    Debug.Log("Right tiles are room entrances");
                    placementGatewayX = x + 1;
                    placementGatewayY = y - 1;
                    placementGatewayFacing = Facing.West;
                    Instantiate(nextRoomTemplate, new Vector3(x + 1, (y - 1) - nextRoom.GatewayOneY, 0), Quaternion.identity);
                }
                else if (tiles[x - 1, y] == 5)
                {
                    Debug.Log("Left tiles are room entrances");
                    placementGatewayX = x - 1;
                    placementGatewayY = y - 1;
                    placementGatewayFacing = Facing.East;
                    Instantiate(nextRoomTemplate, new Vector3(x - 1, (y - 1) - nextRoom.GatewayOneY, 0), Quaternion.identity);
                }
            }
            else if (tiles[x, y + 1] == 3)
            {
                Debug.Log("Top adjacent square is also open gateway");
                if (tiles[x + 1, y] == 5)
                {
                    Debug.Log("Right tiles are room entrances");
                    placementGatewayX = x + 1;
                    placementGatewayY = y;
                    placementGatewayFacing = Facing.West;
                    Instantiate(nextRoomTemplate, new Vector3(x + 1, y - nextRoom.GatewayOneY, 0), Quaternion.identity);
                }
                else if (tiles[x - 1, y] == 5)
                {
                    Debug.Log("Left tiles are room entrances");
                    placementGatewayX = x - 1;
                    placementGatewayY = y;
                    placementGatewayFacing = Facing.East;
                    Instantiate(nextRoomTemplate, new Vector3(x - 1, y - nextRoom.GatewayOneY, 0), Quaternion.identity);
                }
            }
        }
        else if ((tiles[x, y] == 4))
        {
            Debug.Log("Unit on connected gateway");
        }
        else
        {
            Debug.Log("Unit NOT on gateway");
        }
    }

    void CheckIfGatewayConnected(int x, int y)
    {
        //GameObject
    }

    //void CreateAbandonedNest()
    //{
    //    int mapSizeX = 4;
    //    int mapSizeY = 4;

    //    GenerateMapData(mapSizeX, mapSizeY);

    //    tiles[0, 0] = 1;
    //    tiles[3, 0] = 1;
    //    tiles[2, 2] = 1;
    //    tiles[1, 0] = 3;
    //    tiles[2, 0] = 3;
    //    tiles[3, 2] = 3;
    //    tiles[3, 3] = 3;

    //    GeneratePathfindingGraph(mapSizeX, mapSizeY);
    //    GenerateMapVisual(mapSizeX, mapSizeY);
    //}

    //void GenerateMapData(int mapSizeX, int mapSizeY)
    //{
    //    tiles = new int[mapSizeX, mapSizeY];

    //    int x, y;

    //    for (x = 0; x < mapSizeX; x++)
    //    {
    //        for (y = 0; y < mapSizeY; y++)
    //        {
    //            tiles[x, y] = 0;
    //        }
    //    }
    //}

    void GenerateMapData(int mapSizeX, int mapSizeY, int startingPointX, int startingPointY)
    {
        //tiles = new int[mapSizeX, mapSizeY];

        for (int x = startingPointX; x < (startingPointX + mapSizeX); x++)
        {
            for (int y = startingPointY; y < (startingPointY + mapSizeY); y++)
            {
                tiles[x, y] = 0;
            }
        }
    }

    void GenerateMapData(int mapSize)
    {
        //tiles = new int[mapSize, mapSize];

        int x, y;

        for (x = 0; x < mapSize; x++)
        {
            for (y = 0; y < mapSize; y++)
            {
                tiles[x, y] = 5;
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
        //Debug.Log("DEBUG FUNCTION UnitCanWalkOnTile");
        //TileType tt = tileTypes[tiles[x, y]];
        //return tt.isWalkable;
        return tileTypes[tiles[x, y]].isWalkable;
    }

    void GeneratePathfindingGraph(int mapSizeX, int mapSizeY)
    {
        //graph = new Node[mapSizeX, mapSizeY];

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
    void GeneratePathfindingGraph(int mapSizeX, int mapSizeY, int startingPointX, int startingPointY)
    {
        //graph = new Node[mapSizeX, mapSizeY];

        for (int x = startingPointX; x < (startingPointX + mapSizeX); x++)
        {
            for (int y = startingPointY; y < (startingPointY + mapSizeY); y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
                //Debug.Log("Pathfinding - Created Node at position: " + x + ", " + y);
            }
        }

        //Adding neighbours to connect to current tile
        for (int x = startingPointX; x < (startingPointX + mapSizeX); x++)
        {
            for (int y = startingPointY; y < (startingPointY + mapSizeY); y++)
            {

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

                ////eight way connection - Whole map
                //if (x > 0)
                //{
                //    //Add to the left
                //    graph[x, y].neighbours.Add(graph[x - 1, y]);
                //    if (y > 0)
                //        //Add to the bottom left
                //        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                //    if (y < mapSizeY - 1)
                //        //Add to the top left
                //        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                //}

                //eight way connection - Room specific
                if (x > startingPointX)
                {
                    //Add to the left
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > startingPointY)
                        //Add to the bottom left
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < (startingPointY + mapSizeY - 1))
                        //Add to the top left
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                ////Whole map
                //if (x < mapSizeX - 1)
                //{
                //    //Add to the right
                //    graph[x, y].neighbours.Add(graph[x + 1, y]);
                //    if (y > 0)
                //        //Add to the bottom right
                //        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                //    if (y < mapSizeY - 1)
                //        //Add to the top right
                //        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                //}

                //Room specific
                if (x < (startingPointX + mapSizeX - 1))
                {
                    //Add to the right
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > startingPointY)
                        //Add to the bottom right
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < (startingPointY + mapSizeY - 1))
                        //Add to the top right
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }

                ////Whole map
                //if (y > 0)
                //    //Add to the bottom
                //    graph[x, y].neighbours.Add(graph[x, y - 1]);
                //if (y < mapSizeY - 1)
                //    //Add to the top
                //    graph[x, y].neighbours.Add(graph[x, y + 1]);

                //Room specific
                if (y > startingPointY)
                    //Add to the bottom
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < (startingPointY + mapSizeY - 1))
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
                //Debug.Log("Value of tt.VisualPrefab: " + tt.tileVisualPrefab.ToString());
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                TileClickHandler ch = go.GetComponent<TileClickHandler>();
                ch.tileX = x;
                ch.tileY = y;
                ch.map = this;
            }
        }
    }
    //TODO: Edit this to handle overwriting old rooms - Placing a new room and destroying the one which is overlapping
    void GenerateMapVisual(int mapSizeX, int mapSizeY, int startingPointX, int startingPointY)
    {
        int roomSizeX = startingPointX + mapSizeX;
        int roomSizeY = startingPointY + mapSizeY;
        Room entrance = new Room("Entrance", startingPointX, startingPointY, roomSizeX, roomSizeY, 1, 1);
        //TODO: Test this room object - work out why a second object is being created
        GameObject room = Instantiate(entrance.room, new Vector3(startingPointX, startingPointY, 0), Quaternion.identity);

        for (int x = startingPointX; x < roomSizeX; x++)
        {
            for (int y = startingPointY; y < roomSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject tile = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity, room.transform);
                //GameObject tile = tt.tileVisualPrefab;
                //Debug.Log("tile type = " + tt.name.ToString());

                TileClickHandler ch = tile.GetComponent<TileClickHandler>();
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

    public void GeneratePathTo(int targetX, int targetY)
    {
        selectedUnit.GetComponent<Unit>().currentPath = null;
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[
                            selectedUnit.GetComponent<Unit>().tileX, 
                            selectedUnit.GetComponent<Unit>().tileY
                            ];

        Node target = graph[targetX, targetY];

        if (!UnitCanWalkOnTile(target.x, target.y))
        {
            Debug.Log("Unit unable to walk onto tile");
            return;
        }

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            //Skip nodes if they are null
            if (v == null)
                continue;

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
