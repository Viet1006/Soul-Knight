
using UnityEngine;

public class EnemyUseshotgun : EnemyWithWeapon
{
    public int numberOfBullet;
    public float angleBetweenDirections;
    protected override void StartAttack()
    {
        for(int i= 0 ; i < numberOfBullet ; i++)
        {
            float angle = i * angleBetweenDirections;
            BulletPool.Instance.GetBullet<StraightBullet>(enemyData.bulletPrefab , spawnBulletPos.transform.position , weapon.transform.rotation * Quaternion.Euler(0, 0, angle))
                .SetBullet(enemyData.bulletSpeed, enemyData.damage, 0, enemyData.element, enemyData.bulletBuffs, 5);
            BulletPool.Instance.GetBullet<StraightBullet>(enemyData.bulletPrefab , spawnBulletPos.transform.position , weapon.transform.rotation * Quaternion.Euler(0, 0, -angle))
                .SetBullet(enemyData.bulletSpeed, enemyData.damage, 0, enemyData.element, enemyData.bulletBuffs, 5);
        }
    }   
}
