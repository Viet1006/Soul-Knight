using System.Collections.Generic;

using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon Data")]
public class WeaponData : SerializedScriptableObject
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine, KeyLabel = "Level", ValueLabel = "Stats")]
    public Dictionary<int , WeaponStatsPerlevel> statsPerLevel = new();
    public float speed;
    public float inaccuracy;
    [Tooltip("Độ lệch của đạn theo độ")]
    public RareColor rareColor;
    public int price;
    public GameObject bulletPrefab;
    public BulletElement element;
    [SerializeReference]
    public  List<BulletBuff> bulletBuffs = new();
    public float bulletTimeLife;
    public int Damage(int level)
    {
        if(level <= -1) return 1;
        if( !statsPerLevel.ContainsKey(level)) return Damage(level -1); // Dùng đệ quy để tìm key nhỏ nhất nếu chưa có
        return statsPerLevel[level].damage;
    }

    public float FireRate(int level)
    {
        if(level <= -1) return 0.1f; // Nếu chưa có level thì trả về 1
        if( !statsPerLevel.ContainsKey(level)) return FireRate(level -1); // Dùng đệ quy để tìm key nhỏ nhất nếu chưa có
        return statsPerLevel[level].fireRate;
    }
    public int CritChance(int level)
    {
        if(level <= -1) return 0;
        if( !statsPerLevel.ContainsKey(level)) return CritChance(level -1); // Dùng đệ quy để tìm key nhỏ nhất nếu chưa có
        return statsPerLevel[level].critChance;
    }
}
[System.Serializable]
public class WeaponStatsPerlevel // Nhóm các chỉ số mỗi cấp độ của weapon
{
    public int damage;
    public float fireRate;
    public int critChance;
}
