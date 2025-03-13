using UnityEngine;

public abstract class EnemyWithoutWeapon : BaseEnemy
{
    [SerializeField] protected float damageBullet;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float speedBullet;
}
