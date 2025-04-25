using UnityEngine;
using UnityEngine.UI;

public class WeaponInventoryManager : WeaponUIManager
{
    public static WeaponInventoryManager instance;
    [HideInInspector] public BaseWeapon usingWeapon; // Vũ khí đang được sử dụng
    [SerializeField] Button equipButton;
    public event System.Action<GameObject> OnWeaponEquipped;
    protected void Awake()
    {
        instance = this;
    }
    public override void SetSelectingSlot(BaseWeapon baseWeapon)
    {
        base.SetSelectingSlot(baseWeapon);
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
                Destroy(child.gameObject);
                break;
            }
        }
    }
    public override void Interact()
    {
        equipButton.interactable = false;
        base.Interact();
    }
    public void StoreWeapon(GameObject weponObject) // Lưu vũ khí vào kho
    {
        weponObject.transform.SetParent(transform); // Đặt cha là Inventory table
        weponObject.SetActive(false); // tắt gameObject
        Instantiate(itemBorder,content).GetComponent<WeaponInventorySlot>().SetWeaponSlot(weponObject.GetComponent<BaseWeapon>(),this); // Tạo slot cho vũ khí mới này
    }
}