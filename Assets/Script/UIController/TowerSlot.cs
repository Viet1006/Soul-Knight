using UnityEngine;

public class TowerSlot : SlotUI
{
    public void SetTowerSlot(TowerData towerData)
    {
        itemName.text = towerData.towerPrefab.name;
        TowerShopManage.instance.buttons.Add(button);
        itemImage.sprite = towerData.towerPrefab.GetComponent<SpriteRenderer>().sprite;
        itemPrice.text = towerData.price.ToString();
        button.onClick.AddListener(() => {
            TowerShopManage.instance.SetInterractButtons(true); // Bật các nút
            button.interactable = false; // Tắt tương tác với nút sau khi ấn
            TowerShopManage.instance.SelectTower(towerData); // Khi được ấn thì gọi SelectTower
        });
    }
}
