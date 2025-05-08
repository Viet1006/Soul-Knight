
using Cinemachine.Utility;
using DG.Tweening;
using UnityEngine;

public class CharingHealth : BaseTower
{
    protected override void Attack(Transform target)
    {
        
    }
    void Awake()
    {
        DOVirtual.DelayedCall(1/towerData.FireRate(level) , () =>
        {
            BulletPool.Instance.GetBullet<HealthController>(towerData.bulletPrefab , spawnPoint.transform.position)
                .SetHealth(towerData.Damage(level));
        },false).SetLoops(-1);
    }
}
