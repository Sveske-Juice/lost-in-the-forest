using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { 
    North, South, East, West
};

public class Door : MonoBehaviour
{
    public Room room = null;

    [SerializeField]
    private Door ConnectedDoor;

    public bool enabled = false;
    public Vector3 positionInRoom { get; private set; }

    public bool TempDisabled = false;

    public string name;

    public Direction direction;

    public uint id = 0;

    public void setup() {
        id = (uint)Random.RandomRange(0, 99999999);
        gameObject.name = "Door " + room.id + "_" + id;
    }

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

    public Door GetConnectedDoor() {
        return ConnectedDoor;
    }

    public void close() {
        TempDisabled = true;
    }

    public void open() {
        TempDisabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"hit {collider.name}");;
        if (!collider.CompareTag("Player")) return;

        // go to connected room
        if (this.ConnectedDoor == null)
        {
            Debug.LogError("We fucked up. Why dis mf door not connected!?");
            return;
        }
        room.gameObject.SetActive(false);
        collider.transform.position = this.ConnectedDoor.transform.position + this.ConnectedDoor.transform.right * 2f;
        this.ConnectedDoor.room.gameObject.SetActive(true);
    }
}
