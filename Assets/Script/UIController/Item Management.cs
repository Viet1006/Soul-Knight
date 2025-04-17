using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
// Quản lý giao diện các Item như súng , tháp
public abstract class ItemManagement : MonoBehaviour , ICanInteract
{
    GameObject selectedName; // Tên hiển thị khi Player đi gần vào
    public List<Button> buttons; // List các button để kiểm soát
    [SerializeField] protected Transform content; // Nơi chứa các item Border
    [SerializeField] protected GameObject itemBorder; // Nơi chứa các Item
    protected Tween appearTween;  // Tween làm bảng xuất hiện
    [SerializeField] protected RectTransform boardShop; // React của board
    protected virtual void Start()
    {
        appearTween = boardShop.DOAnchorPos(boardShop.anchoredPosition,0.7f)
            .SetEase(Ease.OutBack)
            .SetAutoKill(false)
            .Pause(); // Tạo tween cho board
        boardShop.anchoredPosition = new Vector2(-boardShop.anchoredPosition.x,-boardShop.anchoredPosition.y); // Đặt vị trí ban đầu của boardShop
        selectedName = transform.GetChild(0).gameObject;
    }
    public void SetInterractButtons(bool enabled) // Bật tắt interractable cho tất cả các button
    { 
        buttons.RemoveAll(button => button == null); // Xóa button null ra khỏi list
        foreach (Button button in buttons)
        {
            button.interactable = enabled;
        }
    }
    public virtual void HideSelectObject() => selectedName.SetActive(false);

    public abstract void Interact();

    public virtual void ShowSelectObject() => selectedName.SetActive(true);
}
