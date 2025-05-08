using DG.Tweening;
using UnityEngine;

public class BoardShopAnim : MonoBehaviour
{
    Tween appearTween;
    RectTransform boardShop;
    public void Awake()
    {
        boardShop = GetComponent<RectTransform>();
        appearTween = boardShop.DOAnchorPos(boardShop.anchoredPosition,0.6f)
            .SetEase(Ease.OutBack)
            .SetAutoKill(false)
            .Pause()
            .SetUpdate(true); // Tạo tween cho board
        boardShop.anchoredPosition = new Vector2(-boardShop.anchoredPosition.x,-boardShop.anchoredPosition.y); // Đặt vị trí ban đầu của boardShop
    }
    public void ShowBoardShop()
    {
        appearTween.PlayForward();
    }
    public void HideBoardShop()
    {
        appearTween.PlayBackwards();
    }
}
