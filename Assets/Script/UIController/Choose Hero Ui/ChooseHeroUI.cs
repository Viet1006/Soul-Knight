using DG.Tweening;
using UnityEngine;

public class ChooseHeroUI : MonoBehaviour
{
    Tween showTween;
    void Start()
    {
        UIManageShowAndHide.Instance.OnChooseHero += ShowBorder; 
        UIManageShowAndHide.Instance.OnCloseChooseHero += HideBorder;
        UIManageShowAndHide.Instance.OnCompleteChoose += HideBorder;
        RectTransform rectTransform = GetComponent<RectTransform>(); // Lấy RectTransform của đối tượng
        rectTransform.anchoredPosition = -rectTransform.anchoredPosition; 
        showTween = rectTransform.DOAnchorPos(-rectTransform.anchoredPosition, 0.5f)
            .SetEase(Ease.InOutSine)
            .SetAutoKill(false)
            .Pause(); // Tạo tween cho border
    }
    void ShowBorder() // Hiện border khi game chạy
    {
        showTween.PlayForward(); // Delay chờ PauseBorder ẩn đi
    }
    void HideBorder() // Ẩn border khi game dừng hoặc mở Shop
    {
        showTween.PlayBackwards();
    }
    void OnDestroy()
    {
        UIManageShowAndHide.Instance.OnCloseChooseHero -= HideBorder;
        UIManageShowAndHide.Instance.OnChooseHero -= ShowBorder;
    }
}
