using UnityEngine;

public class FlyingEyes : EnemyWithoutWeapon
{
    [SerializeField] GameObject childBullet;
    [SerializeField] float childBulletSpeed;
    [SerializeField] int numberOfBullet;
    protected override void StartAttack()
    {
        BulletPool.instance.GetBullet(bulletData.bullet, transform.position, target.position).GetComponent<SurroundedBullet>()
        .SetSurroundedBullet(bulletData.speedBullet,bulletData.damageBullet,childBulletSpeed,childBullet,numberOfBullet,3f);
        ResetTimeToAttack();
    }
}