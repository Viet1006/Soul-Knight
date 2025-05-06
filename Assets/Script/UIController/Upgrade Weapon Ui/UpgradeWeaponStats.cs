using TMPro;
using UnityEngine;
// Setup các thông số trong upgrade weapon manager
public class UpgradeWeaponStats : WeaponStats
{
    [SerializeField] TextMeshProUGUI weaponName;
    public void SetWeaponStats(BaseWeapon baseWeapon, string weaponName , int level)
    {
        WeaponData weaponData = baseWeapon.weaponData;
        damage.text = weaponData.Damage(level).ToString();
        fireRate.text = weaponData.FireRate(level).ToString();
        critChance.text = weaponData.CritChance(level).ToString();
        inaccuracy.text = weaponData.inaccuracy.ToString();
        iconWeapon.sprite = baseWeapon.GetComponentInChildren<SpriteRenderer>().sprite;
        this.weaponName.text = weaponName;
        this.weaponName.color = SetColor.SetRareColor(baseWeapon.weaponData.rareColor); // Đặt màu cho tên vũ khí
    }
}