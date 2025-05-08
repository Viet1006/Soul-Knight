using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-100)] // Script này sẽ chạy đầu tiên]
public class GameManager : MonoBehaviour
{
    [SerializeField] Image backGround;
    [SerializeField] RectTransform leftPanel;
    [SerializeField] RectTransform rightPanel;
    public static GameManager instance;
    void Awake()
    {
        instance = this;
        DOVirtual.DelayedCall(1 , () =>
        {
            backGround.gameObject.SetActive(false);
            MovePanel(leftPanel);
            MovePanel(rightPanel);
        });
        DOTween.SetTweensCapacity(200 , 1000);
    }
    void MovePanel(RectTransform panel)
    {
        panel.DOAnchorPos( -panel.anchoredPosition , 1);
    }
    public void StartLoad()
    {
        backGround.gameObject.SetActive(true);
        leftPanel.anchoredPosition = - leftPanel.anchoredPosition;
        rightPanel.anchoredPosition = - rightPanel.anchoredPosition;
        DOVirtual.DelayedCall(1 , () =>
        {
            backGround.gameObject.SetActive(false);
            MovePanel(leftPanel);
            MovePanel(rightPanel);
        });
    }
}
