using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected GameObject textDamage;
    public abstract void GetHit(float damage,Color colorDamage);
    [SerializeField] protected BaseWeapon currentWeapon;
    [SerializeField] GameObject selectionCircle;
}
