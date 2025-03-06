using UnityEngine;
public abstract class BaseTower : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    public TowerData towerData;
    protected float timeToNextFire;
    protected GameObject nearestEnemy;
    protected virtual void Update()
    {
        nearestEnemy = FindTarget.GetNearestObject(transform.position,towerData.radiusAttack,LayerMask.GetMask("Enemy"));
        timeToNextFire -= Time.deltaTime;
        if(timeToNextFire < 0 && nearestEnemy)
        {
            Attack(nearestEnemy.transform);
            timeToNextFire = 1/towerData.fireRate;
        }
    }
    protected abstract void Attack(Transform target);
}