using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public float fireRate;
    [Range(0,100)]
    public int critChance;
    public float energyCost;
    [Tooltip("Độ lệch của đạn theo độ")]
    public float inaccuracy;
    public RareColor rareColor;
    public float speed;
    public int damage;
    public GameObject bulletPrefab;
    public  BulletElement element;
    public List<BulletBuff> bulletBuffs;
    public int price;
}
