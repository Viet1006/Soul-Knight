using TMPro;
using UnityEngine;
// Setup các thông số trong upgrade weapon manager
public class UpgradeWeaponStats : WeaponStats
{
    [SerializeField] TextMeshProUGUI weaponName;
    public void SetWeaponStats(WeaponData weaponData , int level , string weaponName)
    {
        base.SetWeaponStats(weaponData,level);
        this.weaponName.text = weaponName;
    }
}
