public class ElectricMan : EnemyWithoutWeapon
{
    protected override void StartAttack()
    {
        BulletPool.instance.GetBullet(bulletData.bulletPrefab, transform.position , target.position)
            .GetComponent<BaseBullet>()
            .SetBullet(bulletData.speed, bulletData.damage,0,BulletElement.Lightning,bulletData.bulletBuffs,3); // Timelife là 3 và ko có chí mạng
        ResetTimeToAttack();
    }
}