using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryManager : ItemManagement
{
    public static WeaponInventoryManager instance;
    [HideInInspector] public BaseWeapon usingWeapon; // Vũ khí đang được sử dụng
    [SerializeField] Button equipButton;
    public event System.Action<GameObject> OnWeaponEquipped;
    [SerializeField] Image panel;
    [SerializeField] WeaponShopStats weaponShopStats;
    protected BaseWeapon selectedWeapon;
    protected void Awake()
    {
        instance = this;
        panel.gameObject.SetActive(true);
    }
    public void SetSelectingSlot(BaseWeapon baseWeapon) // Được gọi khi có vũ khí được chọn từ content
    {
        weaponShopStats.gameObject.SetActive(true);
        weaponShopStats.SetWeaponStats(baseWeapon);
        selectedWeapon = baseWeapon; // Đặt vũ khí đang được chọn
        equipButton.interactable = true;
    }
    public void EquipWeapon()
    {
        StoreWeapon(usingWeapon.gameObject); // Thêm trang bị đang dùng vào inventory
        DeleteSlot(selectedWeapon); // xóa weaponSlot của trang bị được chọn
        selectedWeapon.gameObject.SetActive(true); // Bật vũ khí lên
        OnWeaponEquipped?.Invoke(selectedWeapon.gameObject); // Thực hiện trang bị
        Close(); // Đóng Ui
        usingWeapon = selectedWeapon; // Đổi usingWeapon
    }
    void DeleteSlot(BaseWeapon baseWeapon) // xóa weaponSlot 
    {
        foreach (Transform child in content)
        {
            if (child.GetComponent<WeaponInventorySlot>().itemName.text == baseWeapon.name + " +" + baseWeapon.level)
            {
                Object.Destroy(child.gameObject);
                break;
            }
        }
    }
    public void Interact()
    {
        equipButton.interactable = false;
        boardShopAnim.ShowBoardShop();
        //UIManageShowAndHide.Instance.OpenShop();
        UIManageShowAndHide.Instance.PauseGame();
        DOVirtual.DelayedCall(0.1f,() => panel.enabled = true); // Bật panel sau 0,3s
        SetInterractButtons( true); // bật tất cả các button
        weaponShopStats.gameObject.SetActive(false);
    }
    public void StoreWeapon(GameObject weponObject) // Lưu vũ khí vào kho
    {
        weponObject.SetActive(false); // tắt gameObject
        Object.Instantiate(itemBorder,content).GetComponent<WeaponInventorySlot>().SetWeaponSlot(weponObject.GetComponent<BaseWeapon>(),this); // Tạo slot cho vũ khí mới này
    }
    public void Close() // Đóng inventory
    {
        boardShopAnim.HideBoardShop();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false); // Tắt panel sau 0.4s
        UIManageShowAndHide.Instance.CloseShop();
    }
}