using UnityEngine;

public class LightningTower : BaseTower
{
    protected override void Attack(Transform target)
    {
        BulletPool.Instance.GetBullet<Cloud>(towerData.bulletPrefab , spawnPoint.transform.position)
            .SetCloud(towerData.speed,towerData.Damage(level), 0 ,towerData.element,towerData.bulletBuffs , target);
    }
}
