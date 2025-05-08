using UnityEngine;

public class FrozenEnemy : AttackMethodEnemy
{
    protected override void StartAttack()
    {
        for (int i = 0; i < 12; i++)
        {
            float angle = i * 30f; // 360 degrees divided by 8 directions
            BulletPool.Instance.GetBullet<BaseBullet>(enemyData.bulletPrefab , transform.position , Quaternion.Euler(0,0,angle))
                .SetBullet(enemyData.bulletSpeed, enemyData.damage , 0 ,BulletElement.Frozen , enemyData.bulletBuffs , 3f);
        }
        ResetTimeToAttack();
    }
}
