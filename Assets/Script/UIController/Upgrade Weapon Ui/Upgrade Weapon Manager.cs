using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWeaponManager : MonoBehaviour , ICanInteract
{
    public static UpgradeWeaponManager instance;
    [SerializeField] Image panel;
    BoardShopAnim boardShopAnim;
    GameObject selectedName;
    [SerializeField] UpgradeWeaponStats currentWeapon;
    [SerializeField] UpgradeWeaponStats upgradedWeapon;
    [SerializeField] TMPro.TextMeshProUGUI priceText;
    Button upgradeButton;
    BaseWeapon selectedWeapon;
    public void Awake()
    {
        instance = this;
        boardShopAnim = panel.GetComponentInChildren<BoardShopAnim>(true);
        boardShopAnim.gameObject.SetActive(true);
        selectedName = transform.GetChild(0).gameObject;
        upgradeButton = boardShopAnim.GetComponentInChildren<Button>();
    }
    public void Interact()
    {
        UIManageShowAndHide.Instance().OpenShop();
        boardShopAnim.ShowBoardShop();
        DOVirtual.DelayedCall(0.1f,() => panel.enabled = true); // Bật panel sau 0,1s
        selectedWeapon = WeaponInventoryManager.instance.usingWeapon;
        currentWeapon.SetWeaponStats(selectedWeapon.weaponData,selectedWeapon.level,selectedWeapon.name); // Set chỉ số cho vũ khí hiện tại
        upgradedWeapon.SetWeaponStats(selectedWeapon.weaponData,selectedWeapon.level+1,selectedWeapon.name); // Set chỉ số cho vũ khí sau khi nâng cấp
        int upgradePrice = UpgradePrice.GetUpgradePrice(selectedWeapon.weaponData.rareColor,selectedWeapon.level);
        if(upgradePrice == -1) // Vũ khí đã hết cấp độ nâng cấp
        {
            upgradeButton.interactable = false;
            upgradedWeapon.gameObject.SetActive(false);
            priceText.enabled = false;
        }else
        {
            upgradeButton.interactable = true;
            upgradedWeapon.gameObject.SetActive(true);
            priceText.enabled = true;
            priceText.text = upgradePrice.ToString();
        }
    }
    public void BuyButton()
    {
        if(CoinManager.instance.TryBuy(UpgradePrice.GetUpgradePrice(selectedWeapon.weaponData.rareColor,selectedWeapon.level)) )
        {
            selectedWeapon.Upgrade();
            Close();
            NotificationSystem.instance.ShowNotification("Nâng cấp "+ selectedWeapon.name + " lên +" + selectedWeapon.level +" thành công",1);
        }else{
            NotificationSystem.instance.ShowNotification("Không đủ tiền",1);
        }
    }
    public void Close()
    {
        boardShopAnim.HideBoardShop();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false); // Tắt panel sau 0.4s
        UIManageShowAndHide.Instance().CloseShop();
    }
    public virtual void HideSelectObject() => selectedName.SetActive(false);
    public virtual void ShowSelectObject() => selectedName.SetActive(true);
}