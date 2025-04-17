using UnityEngine;
public abstract class BaseTower : MonoBehaviour
{
    public TowerData towerData;
    protected float timeToNextFire;
    protected Transform nearestEnemy;
    [SerializeField] protected GameObject spawnPoint;
    protected virtual void Update()
    {
        nearestEnemy = FindTarget.GetNearestTransform(transform.position,towerData.radiusAttack,LayerMask.GetMask("Enemy"));
        timeToNextFire -= Time.deltaTime;
        if(timeToNextFire < 0 && nearestEnemy != null)
        {
            Attack(nearestEnemy);
            timeToNextFire = 1/towerData.fireRate;
        }
    }
    protected abstract void Attack(Transform target);
}