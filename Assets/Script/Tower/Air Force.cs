using DG.Tweening;
using UnityEngine;

public class AirForce : BaseTower
{
    Animator animator;
    int droneCount = 1;
    float openTime = 0;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    protected override void Attack(Transform target)
    {
        FactoryDrone(target);
        GetDrone();
    }
    public void FactoryDrone(Transform target) //  Tạo Drone
    {
        DOVirtual.DelayedCall(0.4f , () => {
            GetDrone();
            BulletPool.instance
                .GetBullet(towerData.bulletPrefab , spawnPoint.transform.position) 
                .GetComponent<Drone>()
                .SetDrone(towerData.speed,towerData.damage,towerData.critChance ,towerData.element, towerData.bulletBuffs, target , this);
        }).SetLoops(droneCount , LoopType.Incremental);
    }
    public void GetDrone() // Mở cửa đón Drone về
    {
        if(openTime <= 0) 
        {
            openTime = 0.61f;
            StartCoroutine(OpenDoorCoroutine());
        }
        openTime = 0.61f;
    }
    System.Collections.IEnumerator OpenDoorCoroutine()
    {
        animator.SetTrigger(Parameters.openDoor);
        while (openTime > 0)
        {
            openTime -= Time.deltaTime;
            yield return null;
        }
        animator.SetTrigger(Parameters.closeDoor);
    }
}