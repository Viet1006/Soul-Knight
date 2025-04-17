using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemies No Weapon Data")]
public class BulletEnemyNoWeapon : ScriptableObject
{
    public GameObject bulletPrefab;
    public float speed;
    public int damage;
    public List<BulletBuff> bulletBuffs;
    public BulletElement element;
}
