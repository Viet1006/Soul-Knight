using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemies Data")]
public class EnemyData : ScriptableObject
{
    public int health;
    public float speed;
    public float AttackRate;
    public int cost;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public int damage;
    [SerializeReference]
    public List<BulletBuff> bulletBuffs;
    public BulletElement element;
    public float bulletTimeLife;
}