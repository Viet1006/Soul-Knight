using Pathfinding;

public class ElectricMan : EnemyWithoutWeapon
{
    protected override void StartAttack()
    {
        BaseBullet baseBullet = BulletPool.instance.GetBullet(bulletData.bullet, transform.position , target.position).GetComponent<BaseBullet>();
        baseBullet.SetBullet(bulletData.speedBullet, bulletData.damageBullet, false,BulletElements.Lightning,3);
        ResetTimeToAttack();
    }
}