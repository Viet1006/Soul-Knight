using UnityEngine;
[CreateAssetMenu(fileName = "NewTower", menuName = "Tower/Tower Data", order = 4)]
public class TowerData : ScriptableObject
{
    public float health;
    public float radiusAttack;
    public float fireRate;
    public float damage;
}
