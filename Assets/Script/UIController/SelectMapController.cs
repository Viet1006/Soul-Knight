using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapController : MonoBehaviour
{
    public static SelectMapController instance;
    [SerializeField] BoardShopAnim boardShopAnim;
    [SerializeField] Image panel;
    [SerializeField] GameObject startMap;
    [SerializeField] GameObject map1; 
    protected void Awake()
    {
        instance = this;
        panel.gameObject.SetActive(true);
    }
    public void Interact()
    {
        boardShopAnim.ShowBoardShop();
        UIManageShowAndHide.Instance.SelectMap();
        DOVirtual.DelayedCall(0.2f,() => panel.enabled = true); // Bật panel sau 0,1s
    }
    public void Close()
    {
        boardShopAnim.HideBoardShop();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false); // Tắt panel sau 0.4s
        UIManageShowAndHide.Instance.CloseMap();
    }
    public void StartMap()
    {
        UIManageShowAndHide.Instance.SelectMapComplete();
        startMap.SetActive(false); // cho thành false trước khi hủy vì destroy hủy cuối frame làm A* ở map 1 phát hiện start map
        Destroy(startMap);
        map1.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
    
}
