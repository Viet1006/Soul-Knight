using DG.Tweening;
using UnityEngine;

public class CloseTowerShopButton : MonoBehaviour
{
    Tween appearTween;
    Tween hideTween;
    public static CloseTowerShopButton instance;
    
    [SerializeField] Vector2 hiddenPosition; // Vị trí khi ẩn
    [SerializeField] Vector2 shownPosition; // Vị trí khi hiện
    RectTransform rectTransform;

    void Awake() 
    {
        instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        rectTransform.anchoredPosition = hiddenPosition;
        
        appearTween = rectTransform.DOAnchorPos(shownPosition, 0.5f)
            .SetEase(Ease.InCubic)
            .SetAutoKill(false)
            .Pause()
            .SetUpdate(true);
            
        hideTween = rectTransform.DOAnchorPos(hiddenPosition, 0.5f)
            .SetEase(Ease.OutCubic)
            .SetAutoKill(false)
            .Pause()
            .SetUpdate(true);
    }

    public void ShowButton() 
    {
        if(rectTransform.anchoredPosition != hiddenPosition) rectTransform.anchoredPosition = hiddenPosition;
        appearTween.Restart(); // Bắt đầu lại tween hiện
    }
    
    public void HideButton() 
    {
        if(rectTransform.anchoredPosition != shownPosition) rectTransform.anchoredPosition = shownPosition;
        hideTween.Restart(); // Bắt đầu lại tween ẩn
    }
}