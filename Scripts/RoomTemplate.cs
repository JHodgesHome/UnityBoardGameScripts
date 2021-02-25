using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Facing
{
    None,
    North,
    East,
    South,
    West
};

public class RoomTemplate
{
    public string Name;
    public int RoomSizeX, RoomSizeY;
    public int GatewayOneX, GatewayOneY, GatewayTwoX, GatewayTwoY, GatewayThreeX, GatewayThreeY;
    public readonly Facing GatewayOneFacing, GatewayTwoFacing, GatewayThreeFacing;

    public RoomTemplate(string name, int roomSizeX, int roomSizeY, 
        int gatewayOneX, int gatewayOneY, Facing gatewayOneFacing)
    {
        Name = name;
        RoomSizeX = roomSizeX;
        RoomSizeY = roomSizeY;
        GatewayOneX = gatewayOneX;
        GatewayOneY = gatewayOneY;
        GatewayTwoX = -1;
        GatewayTwoY = -1;
        GatewayThreeX = -1;
        GatewayThreeY = -1;
        GatewayOneFacing = gatewayOneFacing;
    }

    public RoomTemplate(string name, int roomSizeX, int roomSizeY, 
        int gatewayOneX, int gatewayOneY, Facing gatewayOneFacing, 
        int gatewayTwoX, int gatewayTwoY, Facing gatewayTwoFacing)
    {
        Name = name;
        RoomSizeX = roomSizeX;
        RoomSizeY = roomSizeY;
        GatewayOneX = gatewayOneX;
        GatewayOneY = gatewayOneY;
        GatewayTwoX = gatewayTwoX;
        GatewayTwoY = gatewayTwoY;
        GatewayThreeX = -1;
        GatewayThreeY = -1;
        GatewayOneFacing = gatewayOneFacing;
        GatewayTwoFacing = gatewayTwoFacing;
    }

    public RoomTemplate(string name, int roomSizeX, int roomSizeY, 
        int gatewayOneX, int gatewayOneY, Facing gatewayOneFacing, 
        int gatewayTwoX, int gatewayTwoY, Facing gatewayTwoFacing, 
        int gatewayThreeX, int gatewayThreeY, Facing gatewayThreeFacing)
    {
        Name = name;
        RoomSizeX = roomSizeX;
        RoomSizeY = roomSizeY;
        GatewayOneX = gatewayOneX;
        GatewayOneY = gatewayOneY;
        GatewayTwoX = gatewayTwoX;
        GatewayTwoY = gatewayTwoY;
        GatewayThreeX = gatewayThreeX;
        GatewayThreeY = gatewayThreeY;
        GatewayOneFacing = gatewayOneFacing;
        GatewayTwoFacing = gatewayTwoFacing;
        GatewayThreeFacing = gatewayThreeFacing;
    }
}
