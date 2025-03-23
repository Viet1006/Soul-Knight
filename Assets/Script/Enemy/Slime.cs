using UnityEngine;

public class Slime : EnemyWithoutWeapon
{
    public int numberOfBullet;
    protected override void StartAttack()
    {
        for(int i=0 ; i<numberOfBullet ; i++)
        {
            BaseBullet baseBullet = BulletPool.instance.GetBullet(bulletData.bullet, transform.position, Quaternion.Euler(0,0,Random.Range(0,360))).GetComponent<BaseBullet>(); // Spawn đạn lung tung
            baseBullet.SetBullet(bulletData.speedBullet, bulletData.damageBullet, false,BulletElements.NoElement,3);
            ResetTimeToAttack();
        }
    }
}