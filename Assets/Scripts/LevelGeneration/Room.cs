using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public List<Door> doors;
    public List<EnemySpawner> spawners;

    public uint id = 0;

    public void setup() {
        id = (uint)Random.RandomRange(0, 99999999);
        gameObject.name = "Room " + id;
    }
}
