using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Gateway
{
    public Facing facing;
    public int posX, posY;
}


[CreateAssetMenu(fileName = "New Room", menuName = "Room")]
public class RoomTest : ScriptableObject
{
    public string roomName;
    public bool isCurrentRoom;
    public int roomPosX, roomPosY, roomSizeX, roomSizeY;
    public int distanceFromCurrentRoom;
    
    public Gateway[] gateways;
}
