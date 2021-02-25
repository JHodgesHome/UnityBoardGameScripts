using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public string Name;
    public bool IsCurrentRoom;
    public int RoomStartX, RoomStartY ,RoomSizeX, RoomSizeY;
    public int GatewayOneX, GatewayOneY, GatewayTwoX, GatewayTwoY, GatewayThreeX, GatewayThreeY;
    public int DistanceFromCurrentRoom;

    public GameObject room = new GameObject();

    public Room(string name, int roomStartX, int roomStartY, int roomSizeX, int roomSizeY, int gatewayOneX, int gatewayOneY)
    {
        Name = name;
        RoomStartX = roomStartX;
        RoomStartY = roomStartY;
        RoomSizeX = roomSizeX;
        RoomSizeY = roomSizeY;
        GatewayOneX = gatewayOneX;
        GatewayOneY = gatewayOneY;
        GatewayTwoX = -1;
        GatewayTwoY = -1;
        GatewayThreeX = -1;
        GatewayThreeY = -1;
    }

    public Room(string name, int roomStartX, int roomStartY, int roomSizeX, int roomSizeY, int gatewayOneX, int gatewayOneY, int gatewayTwoX, int gatewayTwoY)
    {
        Name = name;
        RoomStartX = roomStartX;
        RoomStartY = roomStartY;
        RoomSizeX = roomSizeX;
        RoomSizeY = roomSizeY;
        GatewayOneX = gatewayOneX;
        GatewayOneY = gatewayOneY;
        GatewayTwoX = gatewayTwoX;
        GatewayTwoY = gatewayTwoY;
        GatewayThreeX = -1;
        GatewayThreeY = -1;
    }

    public Room(string name, int roomStartX, int roomStartY, int roomSizeX, int roomSizeY, int gatewayOneX, int gatewayOneY, int gatewayTwoX, int gatewayTwoY, int gatewayThreeX, int gatewayThreeY)
    {
        Name = name;
        RoomStartX = roomStartX;
        RoomStartY = roomStartY;
        RoomSizeX = roomSizeX;
        RoomSizeY = roomSizeY;
        GatewayOneX = gatewayOneX;
        GatewayOneY = gatewayOneY;
        GatewayTwoX = gatewayTwoX;
        GatewayTwoY = gatewayTwoY;
        GatewayThreeX = gatewayThreeX;
        GatewayThreeY = gatewayThreeY;
    }

    public void SetDistance(int distance)
    {
        DistanceFromCurrentRoom = distance;
    }
}
