using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public float fireRate;
    
    [Range(0,100)]
    public int critChance;
    public float energyCost;
    [Tooltip("Độ lệch của đạn theo độ")]
    public float inaccuracy;
    public RareColor rareColor;
    public float bulletSpeed;
    public float damage;
    public GameObject bullet;
    public BulletElements elements;
}
