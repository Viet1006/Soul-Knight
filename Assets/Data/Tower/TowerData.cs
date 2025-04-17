
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewTower", menuName = "Tower Data", order = 4)]
public class TowerData : ScriptableObject
{
    public int health;
    public float radiusAttack;
    public float fireRate;
    public GameObject bulletPrefab;
    public int damage;
    public float speed;
    public int critChance;
    public List<BulletBuff> bulletBuffs;
    public BulletElement element;
    public GameObject towerPrefab;
    public string towerDescription;
    public int price;
}
