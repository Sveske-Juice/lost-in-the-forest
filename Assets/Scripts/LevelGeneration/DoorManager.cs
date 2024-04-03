using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public DoorManager Instance { get; private set; }
    public Room Create { get; private set; }

    // First room passed as argument
    public UnityEvent<Vector3> LevelGenerated;

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

    private List<Door> AllDoors = new List<Door>();

    public List<int> SeedList = new List<int>();

    private void Start()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);

        if (SeedList.Count > 0) { // We running a fixed seed
            int randomValue = UnityEngine.Random.Range(0, SeedList.Count);

            UnityEngine.Random.InitState(SeedList[randomValue]);
        }
        
        var startRoom = InitializeGeneration(roomLimit).gameObject;

        foreach (var room in GameObject.FindObjectsByType<Room>(FindObjectsSortMode.None))
        {
            room.gameObject.SetActive(false);
        }

        startRoom.SetActive(true);
        LevelGenerated?.Invoke(startRoom.transform.position);
    }

    private Room InitializeGeneration(int _maxRooms = 40)
    {
        // Debug.Log("Init start");

        // roomLimit = _maxRooms;

        // Start room
        Room room = Instantiate(startRoom).GetComponent<Room>();
        ActivateRandomDoors(room);

        //                            This adds the doors to AllDoors as well lmao
        List<Door> enabledDoorsList = AddEnabledDoorsToList(room);
        // Debug.Log("TotalDoorsList: " + room.doors.Count);
        // Debug.Log("EnabledDoorsList: " + enabledDoorsList.Count);

        // Debug.Log("Before shuffle:");
        // Debug.Log(enabledDoorsList[0].name);
        // Debug.Log(enabledDoorsList[1].name);

        while (AllDoors.Count > 1)
        {
            ShuffleEnabledDoorsList(AllDoors);
            Door startDoor = AllDoors[0];

            // Debug.Log("After shuffle:");
            // Debug.Log(enabledDoorsList[0].name);
            // Debug.Log(enabledDoorsList[1].name);

            float chanceForNewRoom = 1.0f - ((float)currentRooms / (float)_maxRooms);

            Debug.Log("Chance for new room: " + chanceForNewRoom);

            float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            if (randomValue < chanceForNewRoom)  // Create new room
            {
                GenerateRoom(startDoor);
            }
            // Not creating a new room; connecting existing door to another
            else
            {
                // bool found = false;
                // foreach (Door door in AllDoors)
                // {
                //     if (door.direction == startDoor.OppositeDirection())
                //     {
                //         ConnectDoors(startDoor, door);
                //         found = true;
                //         break;
                //     }
                // }

                Door door = FindDoorNotInRoom(startDoor.room, startDoor.OppositeDirection());
                
                if (door == null)
                {
                    // fuck itt, connect to a random door
                    // ConnectDoors(startDoor, AllDoors[0]);
                    GenerateRoom(startDoor);
                    Debug.LogError("The map is shit");
                    return room;
                } else {
                    ConnectDoors(startDoor, door);
                }
            }
        }

        if (AllDoors.Count == 1)
        { // 1 door left to connect
            Room room_shadowed = CreateRandomRoom(Vector3.zero);

            Door doorToConnect = null;
            Door startDoor = AllDoors[0];

            foreach (Door door in room_shadowed.doors)
            {
                if (door.direction == startDoor.OppositeDirection())
                {
                    door.enabled = true;
                    door.gameObject.SetActive(true);

                    doorToConnect = door;
                }
                else
                {
                    door.enabled = false;
                    door.gameObject.SetActive(false);
                }
            }

            room_shadowed.gameObject.transform.position = startDoor.gameObject.transform.position + (startDoor.gameObject.transform.position - doorToConnect.gameObject.transform.position);

            ConnectDoors(doorToConnect, startDoor);

            Debug.Log("Created boss room");
        }
        return room;
    }

    private Room GenerateRoom(Door startDoor)
    {
        // Create a new room from a specific door
        Room newRoom = CreateRandomRoom(startDoor.transform.position);

        // Randomize door-states in new room
        ActivateRandomDoors(newRoom);
        List<Door> newEnabledDoorsList = AddEnabledDoorsToList(newRoom);
        ShuffleEnabledDoorsList(newEnabledDoorsList);

        Door connectionDoor = null;

        foreach (Door door in newRoom.doors)
        {
            if (door.direction == startDoor.OppositeDirection())
            {
                connectionDoor = door;
                break;
            }
        }

        if (connectionDoor == null)
        {
            // No other door that isnt connected has an opposite direction...
            // TODO: Find out a waý to avoid this, or generate a new room when you cant find a door to connect
            Debug.LogError("oppositeDoor not found");
            return null;
        }

        if (connectionDoor.enabled == false)
        {
            connectionDoor.enabled = true;
            connectionDoor.gameObject.SetActive(true);
        }

        // Connect the specific door to a random and enabled door in the new room
        ConnectDoors(startDoor, connectionDoor);

        // Move room so doors overlap
        newRoom.gameObject.transform.position = startDoor.gameObject.transform.position + (startDoor.gameObject.transform.position - connectionDoor.gameObject.transform.position);

        return newRoom;
    }

    [SerializeField] private string roomPrefabPath = "RoomPrefabs";
    [SerializeField] private List<GameObject> roomPrefabList;
    [SerializeField] private GameObject startRoom;

    // Instantiates a random room-prefab with premade door-spawn-position
    private Room CreateRandomRoom(Vector3 _spawnPosition)
    {
        if (roomPrefabList.Count > 0)
        {
            // Debug.Log("roomPrefabsList count: " + roomPrefabList.Count);
            int randomIndex = UnityEngine.Random.Range(0, roomPrefabList.Count);
            GameObject randomRoomPrefab = Instantiate(roomPrefabList[randomIndex], _spawnPosition, Quaternion.identity);
            Room randomRoomComponent = randomRoomPrefab.GetComponent<Room>();

            randomRoomComponent.setup();

            foreach (Door door in randomRoomComponent.doors) {
                door.room = randomRoomComponent;
                door.setup();
            }

            currentRooms++;
            return randomRoomComponent;
        }
        else
        {
            Debug.LogError("No room-prefabs found in Rooms-list");
            return null;
        }
    }

    // Randomizes the states of each door in premade room (enabled/disabled)
    private void ActivateRandomDoors(Room _targetRoom)
    {
        // List<Door> DoorsInRoom = new List<Door>();
        // Debug.Log("doors.Count: " + _targetRoom.doors.Count);
        // Debug.Log("InitCDoors: " + currentDoorsInRoom);

        foreach (Door door in _targetRoom.doors)
        {
            // Debug.Log("Inside foreach loop");
            // Debug.Log(door.name);

            float chanceForEnablingDoor = 1 - ((float)AllDoors.Count / (float)roomLimit);
            // Debug.Log("ChanceForDoor: " + chanceForEnablingDoor);

            float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            // Debug.Log("RandVal: " + randomValue);

            if (randomValue < chanceForEnablingDoor)
            {
                door.enabled = true;
                door.gameObject.SetActive(true);
                currentDoorsInRoom++;
            }
            else
            {
                door.enabled = false;
                door.gameObject.SetActive(false);
            }
            // Debug.Log("CurrDoors: " + currentDoorsInRoom);
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
        // lmao
        AllDoors.AddRange(enabledDoors);

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
    private void ConnectDoors(Door _currentDoor, Door _targetDoor)
    {
        _currentDoor.ConnectToDoor(_targetDoor);
        _targetDoor.ConnectToDoor(_currentDoor);

        AllDoors.Remove(_targetDoor);
        AllDoors.Remove(_currentDoor);
    }

    // Picks a random (enabled and not connected to another door) door in a specific _targetRoom:
    private void RandomRoomDoor(Room _randomTargetRoom)
    {
        // if no other door: return null
    }

    // Finds a random door from the list of doors that is not in the _targetRoom:
    private Door FindDoorNotInRoom(Room _targetRoom, Direction dir)   // Auuugh
    {
        String roomId = _targetRoom.gameObject.name.Split(" ")[1];
        foreach (Door door in AllDoors) {
            String doorRoom = door.gameObject.name.Split(" ")[1].Split("_")[0];

            if (doorRoom != roomId && door.direction == dir) {
                return door;
            }
        }

        return null;
    }
}
