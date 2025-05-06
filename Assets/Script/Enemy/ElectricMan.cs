public class ElectricMan : AttackMethodEnemy
{
    HandleEffectOnEnemy handleEffect;
    protected override void StartAttack()
    {
        if(!handleEffect) handleEffect = GetComponent<HandleEffectOnEnemy>();
        handleEffect.StartPush((transform.position - target.position).normalized,1);
        BulletPool.Instance.GetBullet<BaseBullet>(enemyData.bulletPrefab, transform.position , target.position)
            .SetBullet(enemyData.bulletSpeed, enemyData.damage,0,BulletElement.Lightning,enemyData.bulletBuffs,enemyData.bulletTimeLife); // Timelife là 3 và ko có chí mạng
        ResetTimeToAttack();
    }
}