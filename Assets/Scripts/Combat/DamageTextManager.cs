using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] Transform textParent;

    [SerializeField] GameObject damageTextPrefab;

    [SerializeField] float lifeTime = 3f;

    private void OnEnable()
    {
        HealthComponent.OnHealthChangeAtPos += SpawnDamage;
    }

    private void OnDisable()
    {
        HealthComponent.OnHealthChangeAtPos -= SpawnDamage;
    }
    public void SpawnDamage(Vector3 position, float damage)
    {
        position.z = -1f;
        GameObject go = Instantiate(damageTextPrefab, position, Quaternion.identity, textParent);
        go.GetComponent<DamageText>().Init(damage);
        Destroy(go, lifeTime);
    }
}
