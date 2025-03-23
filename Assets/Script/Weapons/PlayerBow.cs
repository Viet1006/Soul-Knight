public class PlayerBow : PlayerWeapon
{
    Bow bow;
    void Start()
    {
        bow = GetComponent<Bow>();
    }
    public override void GetWeapon()
    {
        base.GetWeapon();
        bow.chargingBar.GetComponent<ChargingBar>().target = transform;
    }
}
