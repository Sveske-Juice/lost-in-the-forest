using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct EnemySpawn
{
    public GameObject prefab;

    [Range(0f, 1f)]
    public float probability;
}

[System.Serializable]
public struct EnemySpawnVariation {
	public List<EnemySpawn> enemies;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private int seed = 6969;

    [SerializeField]
	private EnemySpawnVariation variation;

	public void Awake() {
		// get this fucking piece of shit out of here you mother fucker is breaking my fucking
		// level generatiopn piece of shit FUICL
        // UnityEngine.Random.InitState(seed);
        Door.LevelSwitched += LevelSwitched;
	}

    void OnDestroy()
    {
        Door.LevelSwitched -= LevelSwitched;
    }

    private void LevelSwitched(Room room1, Room room2)
    {
        // lol this bad
        if (room2 == transform.parent.GetComponent<Room>())
            SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if (variation.enemies == null) return;

        foreach (EnemySpawn spawn in variation.enemies)
        {
            float rng = Random.Range(0f, 1f);
            if (spawn.probability < rng) continue;

            GameObject enemy = Instantiate(spawn.prefab, transform.root);
            enemy.transform.position = transform.position;
            enemy.transform.rotation = Quaternion.identity;
            enemy.transform.localScale = spawn.prefab.transform.localScale;
            /* NavMeshHit closestHit;
            if (NavMesh.SamplePosition(transform.root.position, out closestHit, 500, NavMesh.AllAreas))
            {
                enemy.transform.position = closestHit.position;
            }
            else
            {
                Debug.LogError("Sampling enemy pos on nav mesh failed");
            } */
        }
    }

	void OnDrawGizmos() {
		Color c = Color.red;
		c.a = 0.5f;
		Gizmos.color = c;
		Gizmos.DrawWireSphere(transform.position, 0.8f);
	}
}
