public class WeaponShopStats : WeaponStats
{
    TMPro.TextMeshProUGUI price;
    protected override void Awake()
    {
        base.Awake();
        price = transform.Find("Cost Line").GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    public override void SetWeaponStats(WeaponData weaponData , int level)
    {
        base.SetWeaponStats(weaponData, level);
        price.text = weaponData.price.ToString();
    }
}
