using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemies Data")]
public class EnemyData : ScriptableObject
{
    public int health;
    public float speed;
    public float attackRange;
    public float AttackRate;
    public int cost;
}