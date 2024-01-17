using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dette script bliver brugt til at spawne playerens attack
// -Morgan

public class PlayerAttack : MonoBehaviour
{
    public CombatPlayer player = new CombatPlayer();
    private bool isAttacking = false;
    private float attackTime = 0;

    public GameObject physicalAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                attackTime = 0;
                Instantiate(physicalAttack, transform.position, transform.rotation);
            }
            if (isAttacking && attackTime >= player.attackSpeed)
            {
                isAttacking = false;
            }
        }
        attackTime += Time.deltaTime;
    }
}
