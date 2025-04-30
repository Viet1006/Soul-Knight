using TMPro;
using UnityEngine;
// Setup các thông số trong upgrade weapon manager
public class UpgradeWeaponStats : WeaponStats
{
    [SerializeField] TextMeshProUGUI weaponName;
    public void SetWeaponStats(BaseWeapon baseWeapon, string weaponName)
    {
        base.SetWeaponStats(baseWeapon);
        this.weaponName.text = weaponName;
        this.weaponName.color = SetColor.SetRareColor(baseWeapon.weaponData.rareColor); // Đặt màu cho tên vũ khí
    }
}
