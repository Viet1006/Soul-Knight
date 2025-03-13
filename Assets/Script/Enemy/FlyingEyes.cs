using UnityEngine;

public class FlyingEyes : EnemyWithoutWeapon
{
    [SerializeField] GameObject childBullet;
    [SerializeField] float childBulletSpeed;
    [SerializeField] int numberOfBullet;
    protected override void StartAttack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<SurroundedBullet>()
        .SetSurroundedBullet(speedBullet,damageBullet,childBulletSpeed,childBullet,numberOfBullet,target.position - transform.position);
        ResetTimeToAttack();
    }
}