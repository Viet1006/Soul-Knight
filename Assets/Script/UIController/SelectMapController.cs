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
    [SerializeField] GameObject backGround;
    protected void Awake()
    {
        instance = this;
        panel.gameObject.SetActive(true);
    }
    public void Interact()
    {
        boardShopAnim.ShowBoardShop();
        UIManageShowAndHide.Instance.PauseGame();
        DOVirtual.DelayedCall(0.3f,() => panel.enabled = true).SetUpdate(true); // Bật panel sau 0,3s
    }
    public void Close()
    {
        boardShopAnim.HideBoardShop();
        DOVirtual.DelayedCall(0.4f,()=> panel.enabled = false).SetUpdate(true); // Tắt panel sau 0.4s
        UIManageShowAndHide.Instance.CloseShop();
    }
    public void StartMap()
    {
        GameManager.instance.StartLoad(); // Bật background
        DOVirtual.DelayedCall(0.5f , () => // Delay để bật background
        {
            UIManageShowAndHide.Instance.SelectMapComplete();
            DOVirtual.DelayedCall(1f , () => backGround.SetActive(false));
            startMap.SetActive(false);
            map1.SetActive(true);
            transform.parent.gameObject.SetActive(false); // Tắt gameobject cha là canvas của start map
        }).SetUpdate(true);
    }
    
}
