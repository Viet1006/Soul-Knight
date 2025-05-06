using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Quản lý giao diện các Item như súng , tháp
public abstract class ItemManagement : MonoBehaviour
{
    [HideInInspector] public List<Button> buttons; // List các button để kiểm soát
    [SerializeField] protected Transform content; // Nơi chứa các item Border
    [SerializeField] protected GameObject itemBorder; // Nơi chứa các Item
    [SerializeField] protected BoardShopAnim boardShopAnim;
    public void SetInterractButtons(bool enabled) // Bật tắt interractable cho tất cả các button
    { 
        buttons.RemoveAll(button => button == null); // Xóa button null ra khỏi list
        foreach (Button button in buttons)
        {
            button.interactable = enabled;
        }
    }
}
