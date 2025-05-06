// Sử dụng vũ khí tấn công 1 lần 1 đợt

using UnityEngine;

public class EnemyUseFireWork : EnemyWithWeapon
{
    [SerializeField] LayerMask hitLayer;
    protected override void StartAttack()
    {
        CreateBullet();
        ResetTimeToAttack();
    }
    public override void CreateBullet()
    {
        RotateWeapon(); // Quay vũ khí về phía target trước khi bắn
        BulletPool.Instance
            .GetBullet<Firework>(enemyData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,target.position)
            .SetFireWork(enemyData.bulletSpeed // Set các giá trị
                ,enemyData.damage
                ,critChance : 0
                ,enemyData.element
                ,enemyData.bulletBuffs ,hitLayer,enemyData.bulletTimeLife);
            // Tạo đạn
    }
}