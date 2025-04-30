using DG.Tweening;
using UnityEngine;

public class MachineGun : BaseTower
{
    [SerializeField] Transform gun;
    protected override void Update()
    {
        base.Update();
    }
    protected override void Attack(Transform target)
    {
        gun.right = target.position - transform.position; // Quay về phía Enemy trước khi bắn
        BulletPool.Instance.GetBullet(towerData.bulletPrefab , spawnPoint.transform.position + gun.up* 0.25f, gun.rotation)
            .GetComponent<BaseBullet>()
            .SetBullet(towerData.speed , towerData.Damage(level) , 0 , towerData.element , towerData.bulletBuffs , 3); // Tạo bullet
            
        BulletPool.Instance.GetBullet(towerData.bulletPrefab , spawnPoint.transform.position - gun.up* 0.25f , gun.rotation)
            .GetComponent<BaseBullet>()
            .SetBullet(towerData.speed , towerData.Damage(level) , 0 , towerData.element , towerData.bulletBuffs , 3); // Tạo bullet 2

        gun.DOMove(-0.1f*gun.right+gun.position, 0.05f).SetLoops(2, LoopType.Yoyo);
        spawnPoint.SetActive(true);
        DOVirtual.DelayedCall(0.05f, () => spawnPoint.SetActive(false) );
    }
}
