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

    private void InitializeGeneration(int _maxRooms = 40)
    {
        Room room = CreateRandomRoom(new Vector3(0,0,0));
        RandomizeDoorStates(room);

        List<Door> doorsList = AddEnabledDoorsToList(room);

        while (doorsList.Count > 1)
        {
            ShuffleEnabledDoorsList();

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

    // Instantiates a random room-prefab with premade door-spawn-position
    private Room CreateRandomRoom(Vector3 _spawnPosition)
    {
        Room[] roomPrefabList = Resources.LoadAll<Room>(roomPrefabPath);
        
        if (roomPrefabList.Length > 0) 
        {
            int randomIndex = UnityEngine.Random.Range(0, roomPrefabList.Length);
            Room randomRoomPrefab = Instantiate(roomPrefabList[randomIndex], _spawnPosition, Quaternion.identity);
            return randomRoomPrefab;
        }
        else
        {
            Debug.LogError("No room-prefabs found in the Resources folder");
            return null;
        }
    }

    // Randomizes the states of each door in premade room (enabled/disabled)
    private void RandomizeDoorStates(Room _targetRoom)
    {
        // List<Door> DoorsInRoom = new List<Door>();
        foreach (Door door in _targetRoom.doors)
        {
            float chanceForEnablingDoor = 1 - (currentDoorsInRoom / doorLimitInRooms);
            float randomValue = UnityEngine.Random.value;

            if (randomValue < chanceForEnablingDoor)
            {
                door.enabled = true;
            }
            else
            {
                door.enabled = false;
            }
        }
    }

    // Adds enaabled doors from RandomizeDoorStates to a list
    // Loop through all doors in room, check state and add to enabledDoorList if door is enabled
    private List<Door> AddEnabledDoorsToList(Room _currentRoom)
    {
        List<Door> enabledDoorsList = new List<Door>();

        foreach (Door door in _currentRoom.doors)
        {
            if (door.enabled)
                enabledDoorsList.Add(door);
        }
        return enabledDoorsList;
    }

    // Shuffles the enabled doors added to list in AddEnabledDoorsToList
    private void ShuffleEnabledDoorsList()
    {
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
