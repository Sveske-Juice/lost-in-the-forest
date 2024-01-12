// Lader dig lave justerbare enemy statblocks i create-menuen.
// -Gabriel

using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy Statblock", menuName = "ScriptableObjects/Enemy Statblock")]
public class EnemyStats : ScriptableObject
{
    [Header("Enemy Stats")]

    [SerializeField]
    public float moveSpeed = 0;

    [SerializeField]
    public float attackRange = 0;

    [SerializeField]
    public float attackDelay = 0;

    [SerializeField]
    public bool canMoveWhileAttacking = false;
}
