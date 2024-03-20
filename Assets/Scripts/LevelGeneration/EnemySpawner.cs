using System.Collections.Generic;
using UnityEngine;

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
        UnityEngine.Random.InitState(seed);
	}

    public void SpawnEnemies()
    {
        if (variation.enemies == null) return;

        foreach (EnemySpawn spawn in variation.enemies)
        {
            float rng = Random.Range(0f, 1f);
            if (spawn.probability < rng) continue;

            GameObject enemy = Instantiate(spawn.prefab, transform.root);
            enemy.transform.localScale = spawn.prefab.transform.localScale;
        }
    }

	void OnDrawGizmos() {
		Color c = Color.red;
		c.a = 0.5f;
		Gizmos.color = c;
		Gizmos.DrawWireSphere(transform.position, 0.8f);
	}
}
