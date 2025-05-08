public class BurnEnemy : AttackMethodEnemy
{
    protected override void StartAttack()
    {
        BulletPool.Instance.GetBullet<FollowBullet>(enemyData.bulletPrefab , transform.position , target:  target.position)
            .SetFollowBullet(enemyData.bulletSpeed , enemyData.damage,0 , enemyData.element  , enemyData.bulletBuffs , target ,5);
        ResetTimeToAttack();
    }
}