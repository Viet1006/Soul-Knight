using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseBorder : MonoBehaviour
{
    Tween appearTween;
    [SerializeField] Image pausePanel;
    [SerializeField] RectTransform pauseBorder;
    readonly float delayShowTime = 0.3f;
    readonly float delayHideTime = 0.6f;
    void Start()
    {
        UIManageShowAndHide.instance.OnPauseGame += ShowBorder; // Đăng ký sự kiện tạm dừng game
        UIManageShowAndHide.instance.OnResumeGame += HideBorder; // Đăng ký sự kiện tiếp tục game
        pauseBorder.gameObject.SetActive(true);
        appearTween = pauseBorder.DOAnchorPosX(pauseBorder.anchoredPosition.x,0.5f)
            .SetEase(Ease.OutBack)
            .SetAutoKill(false)
            .Pause(); // Tạo tween cho border
        pauseBorder.anchoredPosition = new Vector2(-pauseBorder.anchoredPosition.x, pauseBorder.anchoredPosition.y); // Đặt vị trí ban đầu của border
    }
    void ShowBorder() // Hiện border khi game tạm dừng
    {
        DOVirtual.DelayedCall(delayShowTime, () => // Delay chờ các Ui khác ẩn đi
        {
            appearTween.PlayForward();
            pausePanel.enabled =true ;
        });
    }
    void HideBorder() // Ẩn border khi game tiếp tục
    {
        DOVirtual.DelayedCall(delayHideTime, () =>
        {
            appearTween.PlayBackwards();
            DOVirtual.DelayedCall(0.3f, () => pausePanel.enabled = false); // Ẩn panel sau khi border đã chạy được 1 đoạn
        });
    }
    void OnDestroy()
    {
        UIManageShowAndHide.instance.OnPauseGame -= ShowBorder;
        UIManageShowAndHide.instance.OnResumeGame -= HideBorder;
    }
}
