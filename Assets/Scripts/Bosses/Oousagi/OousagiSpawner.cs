using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OousagiSpawner : MonoBehaviour
{
    [SerializeField] GameObject bunny;
    Vector2 centre;
    Vector2 spawnPos;
    public float spawnDist, spawnCooldown;
    float angle;
    [HideInInspector] public int spawnedBunnies;
    public int requiredBunnies;

    void Awake()
    {
        centre = transform.position;
    }

    IEnumerator SpawnBunny()
    {
        while (true && spawnedBunnies < requiredBunnies)
        {
            CalcSpawnPosition();
            Instantiate(bunny, spawnPos, Quaternion.identity);
            spawnedBunnies++;
            spawnCooldown = spawnCooldown * 0.99f;
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    void CalcSpawnPosition()
    {
        angle = Random.Range(0, 360);
        spawnPos = new Vector2(centre.x + spawnDist * Mathf.Cos(angle), centre.y + spawnDist * Mathf.Sin(angle));
    }

    public void StartSpawn()
    {
        CalcSpawnPosition();
        StartCoroutine(SpawnBunny());
    }
}
