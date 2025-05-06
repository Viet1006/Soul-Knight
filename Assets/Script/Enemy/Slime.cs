using UnityEngine;

public class Slime : AttackMethodEnemy
{
    readonly int numberOfBullet =5;
    protected override void StartAttack()
    {
        for(int i=0 ; i<numberOfBullet ; i++)
        {
            BulletPool.Instance // Lấy đạn từ pool
                .GetBullet<BaseBullet>(enemyData.bulletPrefab, transform.position, Quaternion.Euler(0,0,Random.Range(0,360))) // Đặt vị trí và hướng cho đạn 1 cách random
                .SetBullet(enemyData.bulletSpeed, enemyData.damage, 0,enemyData.element,enemyData.bulletBuffs,enemyData.bulletTimeLife); // set các thuộc tính cho đạn
            ResetTimeToAttack();
        }
    }
}