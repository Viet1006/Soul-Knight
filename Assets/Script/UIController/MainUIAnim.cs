using DG.Tweening;
using UnityEngine;
// animation cho các UI chính khi game tạm dừng và tiếp tục
public class MainUIAnim : MonoBehaviour
{   
    readonly float delayShowTime = 0.7f; // Thời gian delay trước khi hiện border
    readonly float delayOpenShop = 0.3f; // Delay trước khi mở Shop
    Tween hideTween;
    void Start()
    {
        UIManageShowAndHide.Instance().OnPauseGame += HideBorder; // Đăng ký sự kiện tạm dừng game
        UIManageShowAndHide.Instance().OnResumeGame += ShowBorder; // Đăng ký sự kiện tiếp tục game
        UIManageShowAndHide.Instance().OnOpenShop += HideBorder;
        UIManageShowAndHide.Instance().OnCloseShop += ShowOpenShopBorder;
        RectTransform rectTransform = GetComponent<RectTransform>(); // Lấy RectTransform của đối tượng
        hideTween = rectTransform.DOAnchorPos(-rectTransform.anchoredPosition,0.5f)
            .SetEase(Ease.InBack)
            .SetAutoKill(false)
            .Pause(); // Tạo tween cho border
    }
    void ShowBorder() // Hiện border khi game chạy
    {
        DOVirtual.DelayedCall(delayShowTime, () => hideTween.PlayBackwards() ); // Delay chờ PauseBorder ẩn đi
    }
    void ShowOpenShopBorder() // Hiện border khi đóng shop
    {
        DOVirtual.DelayedCall(delayOpenShop, () => hideTween.PlayBackwards() ); // Delay chờ PauseBorder ẩn đi
    }
    void HideBorder() // Ẩn border khi game dừng hoặc mở Shop
    {
        hideTween.PlayForward();
    }
    void OnDestroy()
    {
        UIManageShowAndHide.Instance().OnPauseGame -= HideBorder;
        UIManageShowAndHide.Instance().OnResumeGame -= ShowBorder;
    }
}
