using DG.Tweening;
using UnityEngine;

public class MachineGun : BaseTower
{
    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack(Transform target)
    {
        transform.right = target.position - transform.position; // Quay về phía Enemy trước khi bắn
        BulletPool.instance.GetBullet(towerData.bulletPrefab , spawnPoint.transform.position + transform.up* 0.25f, transform.rotation)
            .GetComponent<BaseBullet>()
            .SetBullet(towerData.speed , towerData.damage , towerData.critChance , towerData.element , towerData.bulletBuffs , 3); // Tạo bullet
            
        BulletPool.instance.GetBullet(towerData.bulletPrefab , spawnPoint.transform.position - transform.up* 0.25f , transform.rotation)
            .GetComponent<BaseBullet>()
            .SetBullet(towerData.speed , towerData.damage , towerData.critChance , towerData.element , towerData.bulletBuffs , 3); // Tạo bullet 2

        transform.DOLocalMove(-0.1f*transform.right, 0.05f).SetLoops(2, LoopType.Yoyo);
        spawnPoint.SetActive(true);
        DOVirtual.DelayedCall(0.05f, () => spawnPoint.SetActive(false) );
    }
}
