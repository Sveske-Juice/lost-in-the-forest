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

    private List<Door> AllDoors = new List<Door>();

    private void Start()
    {
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        InitializeGeneration();
    }

    private void InitializeGeneration(int _maxRooms = 40)
    {
        // Debug.Log("Init start");
        
        Room room = CreateRandomRoom(new Vector3(0,0,0));
        ActivateRandomDoors(room);

        List<Door> enabledDoorsList = AddEnabledDoorsToList(room);
        // Debug.Log("TotalDoorsList: " + room.doors.Count);
        // Debug.Log("EnabledDoorsList: " + enabledDoorsList.Count);

        // Debug.Log("Before shuffle:");
        // Debug.Log(enabledDoorsList[0].name);
        // Debug.Log(enabledDoorsList[1].name);

        while (enabledDoorsList.Count > 1)
        {
            ShuffleEnabledDoorsList(enabledDoorsList);
            Door startDoor = enabledDoorsList[0];

            // Debug.Log("After shuffle:");
            // Debug.Log(enabledDoorsList[0].name);
            // Debug.Log(enabledDoorsList[1].name);

            float chanceForNewRoom = 1 - (currentRooms / _maxRooms);

            float randomValue = UnityEngine.Random.Range(0.0f, 1.0f);
            if (randomValue < chanceForNewRoom)  // Create new room
            {
                // Create a new room from a specific door
                Room newRoom = CreateRandomRoom(startDoor.transform.position);

                // Randomize door-states in new room
                ActivateRandomDoors(newRoom);
                List<Door> newEnabledDoorsList = AddEnabledDoorsToList(newRoom);
                ShuffleEnabledDoorsList(newEnabledDoorsList);

                Door connectionDoor = null;

                foreach (Door door in newRoom.doors) {
                    if (door.direction == startDoor.OppositeDirection())
                    {
                        connectionDoor = door;
                        break;
                    }
                }
                if (connectionDoor == null)
                    Debug.LogError("oppositeDoor not found");
                
                if (connectionDoor.enabled == false)
                {
                    connectionDoor.enabled = true;
                    connectionDoor.gameObject.SetActive(true);
                }

                // Connect the specific door to a random and enabled door in the new room
                ConnectDoors(startDoor, connectionDoor);
            } 
            // Not creating a new room; connecting existing door to another
            else 
            {
                bool found = false;
                foreach (Door door in AllDoors)
                {
                    if (door.direction == startDoor.OppositeDirection())
                    {
                        ConnectDoors(startDoor, door);
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    Debug.LogError("The map is shit");
                }
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
            // Debug.Log("roomPrefabsList count: " + roomPrefabList.Count);
            int randomIndex = UnityEngine.Random.Range(0, roomPrefabList.Count);
            GameObject randomRoomPrefab = Instantiate(roomPrefabList[randomIndex], _spawnPosition, Quaternion.identity);
            Room randomRoomComponent = randomRoomPrefab.GetComponent<Room>();
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

            float chanceForEnablingDoor = 1 - ((float)currentDoorsInRoom / (float)doorLimitInRooms);
            // Debug.Log("ChanceForDoor: " + chanceForEnablingDoor);

            float randomValue = UnityEngine.Random.Range(0.0f,1.0f);
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
