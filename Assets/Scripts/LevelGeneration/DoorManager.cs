using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public DoorManager Instance { get; private set; }
    public Room Create { get; private set; }

    private void Awake()    
    {
        if (Instance = null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Door algorithm:
    [SerializeField] private int roomLimit = 40;
    private int currentRooms;

    [SerializeField] private int doorLimitInRooms = 4;
    private int currentDoorsInRoom;

    private void Start()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        InitializeGeneration();
    }

    private void InitializeGeneration(int _maxRooms = 40)
    {
        Debug.Log("Init start");
        
        Room room = CreateRandomRoom(new Vector3(0,0,0));
        RandomizeDoorStates(room);

        List<Door> enabledDoorsList = AddEnabledDoorsToList(room);
        // Debug.Log("enabledDoorsList: " + enabledDoorsList.Count);

        while (enabledDoorsList.Count > 1)
        {
            ShuffleEnabledDoorsList(enabledDoorsList);
            break;

            float chanceForNewRoom = 1 - (currentRooms / _maxRooms);

            float randomValue = UnityEngine.Random.value; // Random float [0.0; 1.0]
            if (randomValue < chanceForNewRoom)
            {
                // Create a new room from a specific door
                // Randomize door-states in new room
                // Connect the specific door to a random and enabled door in the new room
                // Add the new enabled and connect door to the doorList
                // Add one to currentRooms-count
                // currentRooms++;
            } 
            else 
            {
            }
        }
    }

    [SerializeField] private string roomPrefabPath = "RoomPrefabs";
    [SerializeField] private List<GameObject> roomPrefabList; 

    // Instantiates a random room-prefab with premade door-spawn-position
    private Room CreateRandomRoom(Vector3 _spawnPosition)
    {
        if (roomPrefabList.Count > 0)
        {
            Debug.Log("roomPrefabsList count: " + roomPrefabList.Count);
            int randomIndex = UnityEngine.Random.Range(0, roomPrefabList.Count);
            GameObject randomRoomPrefab = Instantiate(roomPrefabList[randomIndex], _spawnPosition, Quaternion.identity);
            Room randomRoomComponent = randomRoomPrefab.GetComponent<Room>();
            return randomRoomComponent;
        }
        else
        {
            Debug.LogError("No room-prefabs found in Rooms-list");
            return null;
        }
    }

    // Randomizes the states of each door in premade room (enabled/disabled)
    private void RandomizeDoorStates(Room _targetRoom)
    {
        // List<Door> DoorsInRoom = new List<Door>();
        Debug.Log("doors.Count: " + _targetRoom.doors.Count);
        foreach (Door door in _targetRoom.doors)
        {
            // Debug.Log("Inside foreach loop");
            float chanceForEnablingDoor = 1 - (currentDoorsInRoom / doorLimitInRooms);
            float randomValue = UnityEngine.Random.value;

            Debug.Log("ChanceForDoor: " + chanceForEnablingDoor);
            Debug.Log("RandVal: " + randomValue);

            if (randomValue < chanceForEnablingDoor)
            {
                door.enabled = true;
                // door.SetActive(true);
                currentDoorsInRoom++;
            }
            else
            {
                door.enabled = false;
            }
        }
    }

    // Adds enaabled doors from RandomizeDoorStates to a list
    // Loop through all doors in room, check state and add to enabledDoorList if door is enabled
    private List<Door> AddEnabledDoorsToList(Room _targetRoom)
    {
        List<Door> enabledDoors = new List<Door>();

        foreach (Door door in _targetRoom.doors)
        {
            if (door.enabled)
                enabledDoors.Add(door);
        }
        return enabledDoors;
    }

    // Shuffles the enabled doors added to list in AddEnabledDoorsToList
    private void ShuffleEnabledDoorsList(List<Door> _doorList)
    {
        System.Random rng = new System.Random();

        int n = _doorList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Door value = _doorList[k];
            _doorList[k] = _doorList[n];
            _doorList[n] = value;
        }
    }

    // Coonects _currentDoor in a room to _targetDoor in another room:
    private void ConnectDoor(Door _currentDoor, Door _targetDoor)
    {
    }

    // Picks a random (enabled and not connected to another door) door in a specific _targetRoom:
    private void RandomRoomDoor(Room _randomTargetRoom)
    {
        // if no other door: return null
    }

    // Finds a random door from the list of doors that is not in the _targetRoom:
    private void FindDoorNotInRoom(Room _targetRoom)   // Auuugh
    {
    }
}
