using UnityEngine;

public class LightningTower : BaseTower
{
    protected override void Attack(Transform target)
    {
        BulletPool.Instance.GetBullet(towerData.bulletPrefab , spawnPoint.transform.position)
            .GetComponent<Cloud>()
            .SetCloud(towerData.speed,towerData.Damage(level), 0 ,towerData.element,towerData.bulletBuffs , target);
    }
}
