﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int tileX;
    public int tileY;
    public TileMap map;

    public List<Node> currentPath = null;

    float moveSpeed = 2;
    void Update()
    {
        if (currentPath != null)
        {
            int currNode = 0;

            while (currNode < currentPath.Count - 1)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode+1].x, currentPath[currNode+1].y);
                Debug.DrawLine(start, end, Color.red);

                currNode++;
            }
        }
    }

    public void MoveNextTile()
    {
        float remainingMovement = moveSpeed;
        while(remainingMovement > 0)
        {
            if (currentPath == null)
            {
                //CheckIfOnGateway();
                return;
            }

            remainingMovement -= map.CostToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

            tileX = currentPath[1].x;
            tileY = currentPath[1].y;

            transform.position = map.TileCoordToWorldCoord(currentPath[1].x, currentPath[1].y);

            currentPath.RemoveAt(0);

            if (currentPath.Count == 1)
            {
                CheckIfOnGateway();
                currentPath = null;
            }
        }        
    }

    public void CheckIfOnGateway()
    {
        Vector3 currentPosition = transform.position;
        Debug.Log("Current position = " + currentPosition.x + ", " + currentPosition.y);
    }
}