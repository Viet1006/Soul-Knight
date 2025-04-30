using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public abstract class WeaponUIManager : ItemManagement 
{
    [SerializeField] Image panel;
    [SerializeField] WeaponShopStats weaponShopStats;
    protected BaseWeapon selectedWeapon;
    protected override void Start()
    {
        base.Start();
        panel.gameObject.SetActive(true);
    }
    public virtual void SetSelectingSlot(BaseWeapon baseWeapon) // Set các thuộc tính cho ô đang chọn
    {
        weaponShopStats.gameObject.SetActive(true);
        weaponShopStats.SetWeaponStats(baseWeapon);
    }
    public override void Interact()
    {
        boardShopAnim.ShowBoardShop();
        UIManageShowAndHide.Instance().OpenShop();
        DOVirtual.DelayedCall(0.1f,() => panel.enabled = true); // Bật panel sau 0,3s
        SetInterractButtons( true); // bật tất cả các button
        weaponShopStats.gameObject.SetActive(false);
    }
    public void Close()
    {
        boardShopAnim.HideBoardShop();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false); // Tắt panel sau 0.4s
        UIManageShowAndHide.Instance().CloseShop();
    }
}
