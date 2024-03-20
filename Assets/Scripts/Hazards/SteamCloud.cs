using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCloud : MonoBehaviour
{
    public int dmg;
    BoxCollider2D coll;
    // Start is called before the first frame update
    void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        Destroy(this.gameObject, 2); // Destroy object after 2 secs
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CombatPlayer.combatPlayer.TakeDamage(dmg, null);
    }
}
