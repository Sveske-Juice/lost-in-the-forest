using UnityEngine;

// Doesn't really 'spawn' the player but enables and moves it to a location (ie. starting room)
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;

    public void SpawnAt(Vector3 pos)
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        player.position = pos;

        player.gameObject.SetActive(true);
    }

    public void SpawnAtZero()
    {
        SpawnAt(Vector3.zero);
    }
}
