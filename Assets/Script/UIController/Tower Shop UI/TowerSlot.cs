using UnityEngine;

public class TowerSlot : SlotUI
{
    [SerializeField] TMPro.TextMeshProUGUI itemPrice;
    public void SetTowerSlot(BaseTower baseTower)
    {
        itemName.text = baseTower.name;
        TowerShopManage.instance.buttons.Add(button);
        itemImage.sprite = baseTower.GetComponent<SpriteRenderer>().sprite;
        itemPrice.text = baseTower.towerData.price.ToString();
        button.onClick.AddListener(() => {
            TowerShopManage.instance.SetInterractButtons(true); // Bật các nút
            button.interactable = false; // Tắt tương tác với nút sau khi ấn
            TowerShopManage.instance.SelectTower(baseTower); // Khi được ấn thì gọi SelectTower
        });
    }
}
