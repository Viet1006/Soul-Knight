using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController :WeaponUIManager
{
    public static InventoryController instance;
    public TextMeshProUGUI levelText;
    [HideInInspector] public GameObject usingWeapon; // Vũ khí đang được sử dụng
    [SerializeField] Button equipButton;
    public event System.Action<GameObject> OnWeaponEquipped;
    protected void Awake()
    {
        instance = this;
    }
    public override void SetSelectingSlot(GameObject weaponPrefab)
    {
        base.SetSelectingSlot(weaponPrefab);
        equipButton.interactable = true;
    }
    public void EquipWeapon()
    {
        AddNewSlot(FindWeaponPreFab(usingWeapon.name)); // Thêm trang bị đang dùng vào inventory
        DeleteSlot(selectingWeaponPrefab.name); // xóa trang bị được chọn
        OnWeaponEquipped?.Invoke(selectingWeaponPrefab); // Thực hiện trang bị
        Close(); // Đóng Ui
        usingWeapon = selectingWeaponPrefab; // Đổi usingWeapon
    }
    public override void Interact()
    {
        equipButton.interactable = false;
        base.Interact();
    }
}