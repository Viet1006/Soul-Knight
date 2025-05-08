using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWeaponManager : MonoBehaviour
{
    public static UpgradeWeaponManager instance;
    [SerializeField] Image panel;
    BoardShopAnim boardShopAnim;
    [SerializeField] UpgradeWeaponStats currentWeapon;
    [SerializeField] UpgradeWeaponStats upgradedWeapon;
    [SerializeField] TMPro.TextMeshProUGUI priceText;
    Button upgradeButton;
    BaseWeapon selectedWeapon;
    public void Awake()
    {
        instance = this;
        boardShopAnim = panel.GetComponentInChildren<BoardShopAnim>(true);
        upgradeButton = boardShopAnim.GetComponentInChildren<Button>(true);
        panel.gameObject.SetActive(true);
    }
    public void Interact()
    {
        UIManageShowAndHide.Instance.PauseGame();
        boardShopAnim.ShowBoardShop();
        DOVirtual.DelayedCall(0.1f,() => panel.enabled = true).SetUpdate(true); // Bật panel sau 0,1s
        selectedWeapon = InventoryManager.instance.usingWeapon;
        currentWeapon.SetWeaponStats(selectedWeapon,selectedWeapon.name,selectedWeapon.level); // Set chỉ số cho vũ khí hiện tại
        upgradedWeapon.SetWeaponStats(selectedWeapon,selectedWeapon.name,selectedWeapon.level+1); // Set chỉ số cho vũ khí sau khi nâng cấp
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
            NotificationSystem.Instance.ShowNotification("Nâng cấp "+ selectedWeapon.name + " lên +" + selectedWeapon.level +" thành công",1);
        }else{
            NotificationSystem.Instance.ShowNotification("Không đủ tiền",1);
        }
    }
    public void Close()
    {
        boardShopAnim.HideBoardShop();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false).SetUpdate(true); // Tắt panel sau 0.4s
        UIManageShowAndHide.Instance.CloseShop();
    }
}