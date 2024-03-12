using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { 
    North, South, East, West
};

public class Door : MonoBehaviour
{
    public Room Room { get; private set; }
    public Door ConnectedDoor { get; private set; }
    public bool enabled = false;
    public Vector3 positionInRoom { get; private set; }

    public string name;

    public Direction direction;

    public Direction OppositeDirection()
    {
        switch (this.direction)
        {
            case Direction.North:
                return Direction.South;
            case Direction.South:
                return Direction.North;
            case Direction.East:
                return Direction.West;
            case Direction.West:
                return Direction.East;
        }

        // literally not possible but microsoft java is fucking stupid
        return Direction.North;
    }

    public void ConnectToDoor(Door _targetDoor)
    {
        ConnectedDoor = _targetDoor;
    }
}
