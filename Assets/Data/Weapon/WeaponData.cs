using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public float fireRate;
    public float damage;
    [Range(0,100)]
    public float critChance;
    public float energyCost;
    [Tooltip("Độ lệch của đạn theo độ")]
    public float inaccuracy;
    public RareColor rareColor;
    public GameObject bullet;
    public float bulletSpeed;
}
