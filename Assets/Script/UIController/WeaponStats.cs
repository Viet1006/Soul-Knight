using TMPro;
using UnityEngine;

public abstract class WeaponStats : MonoBehaviour
{
    [SerializeField] protected UnityEngine.UI.Image iconWeapon;
    protected TextMeshProUGUI damage;
    protected TextMeshProUGUI fireRate;
    protected TextMeshProUGUI critChance;
    protected TextMeshProUGUI inaccuracy;
    protected virtual void Awake()
    {
        damage = transform.Find("Damage Line").GetComponentInChildren<TextMeshProUGUI>(true);
        fireRate = transform.Find("Fire Rate Line").GetComponentInChildren<TextMeshProUGUI>(true);
        critChance = transform.Find("Critchance Line").GetComponentInChildren<TextMeshProUGUI>(true);
        inaccuracy = transform.Find("Inaccuracy Line").GetComponentInChildren<TextMeshProUGUI>(true);
        
    }
    public virtual void SetWeaponStats(BaseWeapon baseWeapon)
    {
        WeaponData weaponData = baseWeapon.weaponData;
        int level = baseWeapon.level;
        damage.text = weaponData.Damage(level).ToString();
        fireRate.text = weaponData.FireRate(level).ToString();
        critChance.text = weaponData.CritChance(level).ToString();
        inaccuracy.text = weaponData.inaccuracy.ToString();
        iconWeapon.sprite = baseWeapon.GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
