using TMPro;
using UnityEngine;

public abstract class WeaponStats : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image iconWeapon;
    TextMeshProUGUI damage;
    TextMeshProUGUI fireRate;
    TextMeshProUGUI critChance;
    TextMeshProUGUI inaccuracy;
    TextMeshProUGUI energyCost;
    protected virtual void Awake()
    {
        damage = transform.Find("Damage Line").GetComponentInChildren<TextMeshProUGUI>();
        fireRate = transform.Find("Fire Rate Line").GetComponentInChildren<TextMeshProUGUI>();
        critChance = transform.Find("Critchance Line").GetComponentInChildren<TextMeshProUGUI>();
        inaccuracy = transform.Find("Inaccuracy Line").GetComponentInChildren<TextMeshProUGUI>();
        energyCost = transform.Find("Energy Cost Line").GetComponentInChildren<TextMeshProUGUI>();
    }
    public virtual void SetWeaponStats(WeaponData weaponData , int level)
    {
        damage.text = weaponData.Damage(level).ToString();
        fireRate.text = weaponData.FireRate(level).ToString();
        critChance.text = weaponData.CritChance(level).ToString();
        inaccuracy.text = weaponData.inaccuracy.ToString();
        energyCost.text = weaponData.energyCost.ToString();
    }
}
