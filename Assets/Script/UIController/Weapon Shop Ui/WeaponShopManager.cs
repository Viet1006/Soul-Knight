using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-99)] // Setup cho các shop sẽ chạy thứ 2 sau player behaviour
public class WeaponShopManager : ItemManagement
{
    public static WeaponShopManager instance;
    [SerializeField] Button buyButton;
    GameObject[] allWeaponPrefab;
    [SerializeField] Image panel;
    [SerializeField] WeaponShopStats weaponShopStats;
    protected BaseWeapon selectedWeapon;
    protected void Awake()
    {
        instance = this;
        allWeaponPrefab = Resources.LoadAll<GameObject>("Player Weapon");
    }
    protected void Start()
    {
        panel.gameObject.SetActive(true);
        for(int i=0;i< allWeaponPrefab.Length;i++)
        {
            AddNewSlot(allWeaponPrefab[i].GetComponent<BaseWeapon>());
        }
        
    }
    public void SetSelectingSlot(BaseWeapon baseWeapon)
    {
        weaponShopStats.gameObject.SetActive(true);
        weaponShopStats.SetWeaponStats(baseWeapon);
        buyButton.interactable = true;
        selectedWeapon = baseWeapon;
    }
    public void BuyWeapon()
    {
        int price = selectedWeapon.weaponData.price;
        if(! CoinManager.instance.TryBuy(price)) // Kiểm tra xem đủ tiền không
        {
            NotificationSystem.instance.ShowNotification("Not engough coin",1f);
            return;
        }
        DeleteSlot(selectedWeapon); // Mua xong thì xóa weapon slot
        GameObject boughtWeapon =  Object.Instantiate(selectedWeapon.gameObject); // Tạo instante cho vũ khí vừa được mua
        boughtWeapon.name = selectedWeapon.name; // Chỉnh về đúng tên
        WeaponInventoryManager.instance.StoreWeapon(boughtWeapon); // Tạo 1 wepon mới từ prefab đang chọn và thêm vào inventry
        buyButton.interactable = false; // Mua xong thì tắt tương tác với nút mua
        Close();
    }
    public void Interact()
    {
        boardShopAnim.ShowBoardShop();
        //UIManageShowAndHide.Instance.OpenShop();
        UIManageShowAndHide.Instance.PauseGame();
        DOVirtual.DelayedCall(0.1f,() => panel.enabled = true); // Bật panel sau 0,1s
        SetInterractButtons( true); // bật tất cả các button
        weaponShopStats.gameObject.SetActive(false);
        buyButton.interactable = false; // Không cho ấn nút khi mới bật UI
    }
    public void DeleteSlot(BaseWeapon baseWeapon) // xóa weaponSlot 
    {
        foreach (Transform child in content)
        {
            if (child.GetComponent<WeaponShopSlot>().itemName.text == baseWeapon.name)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
    void AddNewSlot(BaseWeapon baseWeapon) // Thêm weaponSlot mới với các weapon tham chiếu đến prefab
    {
        Instantiate(itemBorder,content).GetComponent<WeaponShopSlot>().SetWeaponSlot(baseWeapon,this);
    }
    public void Close()
    {
        boardShopAnim.HideBoardShop();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false); // Tắt panel sau 0.4s
        UIManageShowAndHide.Instance.CloseShop();
    }
}