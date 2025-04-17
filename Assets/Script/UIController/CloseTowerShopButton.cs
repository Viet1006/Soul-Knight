using DG.Tweening;
using UnityEngine;

public class CloseTowerShopButotn : MonoBehaviour
{
    Tween appearTween;
    public static CloseTowerShopButotn instance;
    void Awake() => instance = this;
    void Start()
    {

        RectTransform rectTransform = GetComponent<RectTransform>(); // Lấy RectTransform của đối tượng
        rectTransform.anchoredPosition = -rectTransform.anchoredPosition; // Đặt vị trí ban đầu cho nút
        appearTween = rectTransform.DOAnchorPos(-rectTransform.anchoredPosition,0.5f) // khi nút xuất hiện
            .SetEase(Ease.InCubic)
            .SetAutoKill(false)
            .Pause(); // Tạo tween cho border
    }
    public void ShowButton() => appearTween.PlayForward();
    public void HideButton() => appearTween.PlayBackwards();
}
