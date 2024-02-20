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


    // Har givet Enemysne liv
    // Ville gerne have at den nedarvet fra IDamageable men kunne ikke få det til at virke
    // Har ikke arbejdet med Interfaces før
    // -Morgan
    [SerializeField]
    public int strength = 1;

    [SerializeField]
    public int health = 10;
    [SerializeField]
    public int maxHealth = 10;
}
