using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[SerializeField]
public struct EnemySpawnVariation {
	public List<GameObject> enemies;
}

public class EnemySpawner : MonoBehaviour
{
	public List<EnemySpawnVariation> enemySpawnVariations;

	private EnemySpawnVariation variation;
	private int index = 0;

	public void Init(int seed) {
        UnityEngine.Random.InitState(seed);

		int pp = UnityEngine.Random.Range(0, enemySpawnVariations.Count);

		variation = enemySpawnVariations[pp];
	}

	public GameObject SpawnEnemy() {
		if (index < variation.enemies.Count) {
			GameObject g = Instantiate(variation.enemies[index], transform.position, Quaternion.identity);
			index++;
			return g;
		}
		return null;
	}

	void OnDrawGizmos() {
		Color c = Color.red;
		c.a = 0.5f;
		Gizmos.color = c;
		Gizmos.DrawWireSphere(transform.position, 0.8f);
	}
}
