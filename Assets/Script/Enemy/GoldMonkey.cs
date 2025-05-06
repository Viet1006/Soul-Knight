using UnityEngine;

public class GoldMonkey : EnemyUseContinuousWeapon
{
    readonly Vector2 scale = new(1.3f,1.3f);
    readonly int appltChance = 15; // tỉ lệ bắn thêm đạn 
    [SerializeField] GameObject fireWork;
    readonly int fireWorkDamage = 3;
    readonly int fireWorkSpeed = 15;
    public override void CreateBullet()
    {
        BulletPool.Instance
            .GetBullet<BaseBullet>(enemyData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,scale
                ,weapon.rotation * Quaternion.Euler(0,0,Random.Range(-inaccuracy,inaccuracy))
                )
            .SetBullet(enemyData.bulletSpeed // Set các giá trị
                ,enemyData.damage
                ,critChance: 0
                ,enemyData.element
                ,enemyData.bulletBuffs ,enemyData.bulletTimeLife);
        if(RandomChance.RollChance(appltChance))
        {
            BulletPool.Instance
            .GetBullet<Firework>(fireWork
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,scale
                ,weapon.rotation
                )
            .SetFireWork(fireWorkSpeed // Set các giá trị
                ,fireWorkDamage
                ,critChance: 0
                ,BulletElement.Fire
                ,enemyData.bulletBuffs ,LayerMask.GetMask("Player"),8);
        }
    }
}
