using TMPro;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-99)]
public class WeaponShop : WeaponUIManager
{
    public static WeaponShop instance;
    public TextMeshProUGUI priceText;
    [SerializeField] Button buyButton;
    protected void Awake()
    {
        instance = this;
    }
    protected override void Start()
    {
        base.Start();
        for(int i=0;i< ObjectHolder.instance.allWeaponPrefab.Count;i++) 
        {
            AddNewSlot(ObjectHolder.instance.allWeaponPrefab[i]);
        } 
        DeleteSlot(InventoryController.instance.usingWeapon.name);
    }
    public override void SetSelectingSlot(GameObject weaponPrefab)
    {
        base.SetSelectingSlot(weaponPrefab);
        priceText.text = weaponPrefab.GetComponent<BaseWeapon>().weaponData.price.ToString();
        buyButton.interactable = true;
    }
    public void BuyWeapon()
    {
        int price = selectingWeaponPrefab.GetComponent<BaseWeapon>().weaponData.price;
        if(ManagerCoin.instance.targetValue < price) 
        {
            NotificationSystem.instance.ShowNotification("Not engough coin",1f);
            return;
        }
        DeleteSlot(selectingWeaponPrefab.name); // Mua xong thì xóa weapon slot
        InventoryController.instance.AddNewSlot(selectingWeaponPrefab); // thêm slot ở inventory 
        SetSelectingWeapon(null); // Mua xong thì đặt selecting weapon về null
        buyButton.interactable = false; // Mua xong thì tắt tương tác với nút mua
        ManagerCoin.instance.AddCoin(-price);
    }
    public override void Interact()
    {
        base.Interact();
        buyButton.interactable = false; // Không cho ấn nút khi mới bật UI
    }
}