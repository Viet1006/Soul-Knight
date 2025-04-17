using UnityEngine;

public class Slime : EnemyWithoutWeapon
{
    public int numberOfBullet;
    protected override void StartAttack()
    {
        for(int i=0 ; i<numberOfBullet ; i++)
        {
            BulletPool.instance // Lấy đạn từ pool
                .GetBullet(bulletData.bulletPrefab, transform.position, Quaternion.Euler(0,0,Random.Range(0,360))) // Đặt vị trí và hướng cho đạn 1 cách random
                .GetComponent<BaseBullet>() // Lấy basebullet
                .SetBullet(bulletData.speed, bulletData.damage, 0,bulletData.element,bulletData.bulletBuffs,3); // set các thuộc tính cho đạn
            ResetTimeToAttack();
        }
    }
}