using UnityEngine;
public class WeaponShopSlot : SlotUI
{
    public TMPro.TextMeshProUGUI itemPrice;
    public void SetWeaponSlot(BaseWeapon weapon,WeaponUIManager uIManager) // WeaponUIManage l là WeaponShop hoặc InventoryController
    {
        itemName.text = weapon.name; // Lấy tên để display lên UI
        uIManager.buttons.Add(button); // Thêm vào danh sách các buttons của Ui đang quản lý weaponSlot này
        WeaponData weaponData = weapon.weaponData; // Lấy thông tin của WeaponData để hiển thị lên Ui
        itemName.color = SetColor.SetRareColor(weaponData.rareColor); // Đặt màu cho tên vũ khí
        itemImage.sprite = weapon.GetComponentInChildren<SpriteRenderer>().sprite; // Lấy ảnh vũ khí để hiển thị lên UI
        itemPrice.text = weaponData.price.ToString();
        button.onClick.AddListener(() => {
            uIManager.SetInterractButtons(true); // Bật các nút
            button.interactable = false; // Tắt tương tác với nút sau khi ấn
            uIManager.SetSelectingSlot(weapon.GetComponent<BaseWeapon>());
        });
    }
}