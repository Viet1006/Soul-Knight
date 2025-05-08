
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(fileName = "NewTower", menuName = "Tower Data")]
public class TowerData : SerializedScriptableObject
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine, KeyLabel = "Level", ValueLabel = "Stats")]
    public Dictionary<int , TowerStatsPerlevel> statsPerLevel;

    [DictionaryDrawerSettings(KeyLabel = "Level", ValueLabel = "Upgrade Price")]
    [SerializeField] Dictionary<int, int> upgradePricePerLevel = new();
    public float speed;
    public int price;
    public GameObject bulletPrefab;
    public BulletElement element;
    public  List<BulletBuff> bulletBuffs;
    public float FireRate(int level)
    {
        if(level == -1) return 0.1f; // Nếu chưa có level thì trả về 1
        if( !statsPerLevel.ContainsKey(level)|| statsPerLevel == null) return FireRate(level -1); // Dùng đệ quy để tìm key nhỏ nhất nếu chưa có
        return statsPerLevel[level].fireRate;
    }
    public int Damage(int level)
    {
        if(level == -1) return 1;
        if( !statsPerLevel.ContainsKey(level) || statsPerLevel == null) return Damage(level -1); // Dùng đệ quy để tìm key nhỏ nhất nếu chưa có
        return statsPerLevel[level].damage;
    }
    public float RadiusAttack(int level)
    {
        if(level == -1) return 1;
        if( !statsPerLevel.ContainsKey(level) || statsPerLevel == null) return RadiusAttack(level -1); // Dùng đệ quy để tìm key nhỏ nhất nếu chưa có
        return statsPerLevel[level].radiusAttack;
    }
    public int UpgradePrice(int level)
    {
        if(!upgradePricePerLevel.ContainsKey(level) )
        {
            return -1; // Level chưa được mở
        }
        return upgradePricePerLevel[level];
    }
}
[System.Serializable]
public class TowerStatsPerlevel // Nhóm các chỉ số mỗi cấp độ của tower
{
    public int damage;
    public float fireRate;
    public int radiusAttack;
}
