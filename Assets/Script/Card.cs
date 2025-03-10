using UnityEngine;

public class Card : MonoBehaviour
{
    public void OnClick(GameObject tower)
    {
        TowerShopManage.instance.PickUpCard(tower);
    }
}
