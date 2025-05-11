using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseBorder : MonoBehaviour
{
    Tween appearTween;
    [SerializeField] Image pausePanel;
    [SerializeField] RectTransform pauseBorder;
    readonly float delayShowTime = 0.3f;
    readonly float delayHideTime = 0.5f;
    void Start()
    {
        //UIManageShowAndHide.Instance.OnPauseGame += ShowBorder; // Đăng ký sự kiện tạm dừng game
        UIManageShowAndHide.Instance.OnResumeGame += HideBorder; // Đăng ký sự kiện tiếp tục game
        pauseBorder.gameObject.SetActive(true);
        appearTween = pauseBorder.DOAnchorPosX(pauseBorder.anchoredPosition.x,0.5f)
            .SetEase(Ease.OutBack)
            .SetAutoKill(false)
            .SetUpdate(true)
            .Pause(); // Tạo tween cho border
        pauseBorder.anchoredPosition = new Vector2(-pauseBorder.anchoredPosition.x, pauseBorder.anchoredPosition.y); // Đặt vị trí ban đầu của border
    }
    void ShowBorder() // Hiện border khi game tạm dừng
    {
        DOVirtual.DelayedCall(delayShowTime, () => // Delay chờ các Ui khác ẩn đi
        {
            appearTween.PlayForward();
            pausePanel.enabled =true ;
        }).SetUpdate(true);
    }
    void HideBorder() // Ẩn border khi game tiếp tục
    {
        DOVirtual.DelayedCall(delayHideTime, () =>
        {
            appearTween.PlayBackwards();
            DOVirtual.DelayedCall(0.3f, () => pausePanel.enabled = false); // Ẩn panel sau khi border đã chạy được 1 đoạn
        }).SetUpdate(true);
    }
    void OnDestroy()
    {
        UIManageShowAndHide.Instance.OnPauseGame -= ShowBorder;
        UIManageShowAndHide.Instance.OnResumeGame -= HideBorder;
    }
    public void PauseGame()
    {
        UIManageShowAndHide.Instance.PauseGame();
        UIManageShowAndHide.Instance.ShowPausePanel(); // Gọi sự kiện hiện pause panel
        ShowBorder();
    }
    public void ContinueGame()
    {
        UIManageShowAndHide.Instance.ResumeGame();
    }
    public void ExitGame()
    {
        Application.Quit(); // Thoát game
    }
}
