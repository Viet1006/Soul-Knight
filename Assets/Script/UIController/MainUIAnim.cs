using DG.Tweening;
using UnityEngine;
// animation cho các UI chính khi game tạm dừng và tiếp tục
public class MainUIAnim : MonoBehaviour
{   
    readonly float delayShowTime = 0.7f; // Thời gian delay trước khi hiện border
    readonly float delayOpenShop = 0.3f; // Delay trước khi mở Shop
    readonly float delayCompleteChooseHero = 0.3f;
    Tween showTween;
    RectTransform rectTransform;
    void Start()
    {
        UIManageShowAndHide.Instance.OnPauseGame += HideBorder; // Đăng ký sự kiện tạm dừng game
        UIManageShowAndHide.Instance.OnResumeGame += ShowBorder; // Đăng ký sự kiện tiếp tục game
        UIManageShowAndHide.Instance.OnCloseShop += ShowOpenShopBorder;
        UIManageShowAndHide.Instance.OnCompleteChoose += ShowCompleteChooseHero;
        UIManageShowAndHide.Instance.OnSelectMapComplete += ()=> showTween.PlayForward();
        rectTransform = GetComponent<RectTransform>(); // Lấy RectTransform của đối tượng
        rectTransform.anchoredPosition = - rectTransform.anchoredPosition;
        showTween = rectTransform.DOAnchorPos(-rectTransform.anchoredPosition,0.5f)
            .SetEase(Ease.OutBack)
            .SetAutoKill(false)
            .Pause()
            .SetUpdate(true); // Tạo tween cho border
    }
    void ShowBorder() // Hiện border khi game chạy
    {
        DOVirtual.DelayedCall(delayShowTime, () => showTween.PlayForward() ).SetUpdate(true); // Delay chờ PauseBorder ẩn đi
    }
    public void ShowOpenShopBorder() // Hiện border khi đóng shop
    {
        DOVirtual.DelayedCall(delayOpenShop, () => showTween.PlayForward() ).SetUpdate(true);
    }
    void ShowCompleteChooseHero() // Hiện border khi chọn hero xong
    {
        DOVirtual.DelayedCall(delayCompleteChooseHero, () => showTween.PlayForward() ).SetUpdate(true);
    }
    void HideBorder() // Ẩn border khi game dừng hoặc mở Shop
    {
        showTween.PlayBackwards();
    }
    void OnDestroy()
    {
        UIManageShowAndHide.Instance.OnPauseGame -= HideBorder;
        UIManageShowAndHide.Instance.OnResumeGame -= ShowBorder;
        UIManageShowAndHide.Instance.OnCloseShop -= ShowOpenShopBorder;
        UIManageShowAndHide.Instance.OnCompleteChoose -= ShowCompleteChooseHero;
    }
}
