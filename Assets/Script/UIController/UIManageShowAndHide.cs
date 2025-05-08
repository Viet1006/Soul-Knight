using DG.Tweening;
using UnityEngine;
// Quản lý việc ẩn và hiện các Ui
public class UIManageShowAndHide
{
    static UIManageShowAndHide instance;
    public static UIManageShowAndHide Instance
    {
        get
        {
            instance ??= new UIManageShowAndHide();
            return instance;
        }
    }
    public event System.Action OnPauseGame;
    public event System.Action OnShowPausePanel; // Gọi sự kiện hiện pause panel
    public event System.Action OnResumeGame;
    public event System.Action OnCloseShop;
    public event System.Action OnChooseHero; // bắt đầu chọn hero
    public event System.Action OnCloseChooseHero;
    public event System.Action OnCompleteChoose; // chọn xong hero
    public event System.Action OnSelectMapComplete;
    public void PauseGame() // Gọi sự kiện tạm dừng game
    {
        OnPauseGame?.Invoke();
        Time.timeScale = 0;
    } 
    public void ShowPausePanel() // Gọi sự kiện hiện pause panel
    {
        OnShowPausePanel?.Invoke();
    }
    public void ResumeGame() // Gọi sự kiện tiếp tục game
    {
        DOVirtual.DelayedCall(0.7f , () => Time.timeScale = 1); // Delay để đóng pause panel
        OnResumeGame?.Invoke();
    }  
    public void CloseShop() // Gọi sự kiện đóng shop
    {
        OnCloseShop?.Invoke();
        DOVirtual.DelayedCall(0.4f , () => Time.timeScale = 1); // Delay để đóng shop
    } 
    public void ChooseHero() => OnChooseHero?.Invoke(); 
    public void CloseChooseHero() => OnCloseChooseHero?.Invoke();
    public void CompleteChoose()=> OnCompleteChoose?.Invoke();
    public void SelectMapComplete()
    {
        OnSelectMapComplete?.Invoke();
        Time.timeScale = 1;
    }
}