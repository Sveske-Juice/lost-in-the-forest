using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    [SerializeField] public List<Door> doors;

    public uint id = 0;

    public UnityEvent OnRoomEntered;

    public bool isCleared = false;

    public void setup() {
        id = (uint)UnityEngine.Random.RandomRange(0, 99999999);
        gameObject.name = "Room " + id;
    }

    public void enter() {
        if (!isCleared) {
            OnRoomEntered.Invoke();

            foreach (Door doo in doors) {
                if (doo.enabled) {
                    doo.close();
                }
            }
        }
    }
}
