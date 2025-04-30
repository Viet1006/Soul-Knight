public class WeaponShopStats : WeaponStats
{
    TMPro.TextMeshProUGUI price;
    protected override void Awake()
    {
        base.Awake();
        price = transform.Find("Cost Line").GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    public override void SetWeaponStats(BaseWeapon baseWeapon)
    {
        base.SetWeaponStats(baseWeapon);
        price.text = baseWeapon.weaponData.price.ToString();
    }
}
