using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Room Room { get; private set; }
    public Door ConnectedDoor { get; private set; }
    public bool enabled = false;
    public Vector3 positionInRoom { get; private set; }
}
