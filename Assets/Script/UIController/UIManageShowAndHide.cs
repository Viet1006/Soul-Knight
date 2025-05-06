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
    public event System.Action OnResumeGame;
    public event System.Action OnCloseShop;
    public event System.Action OnChooseHero; // bắt đầu chọn hero
    public event System.Action OnCloseChooseHero;
    public event System.Action OnCompleteChoose; // chọn xong hero
    public event System.Action OnSelectMap;
    public event System.Action OnCloseMap;
    public event System.Action OnSelectMapComplete;
    public void PauseGame()
    {
        OnPauseGame?.Invoke();
        Time.timeScale = 0;
    } // Gọi sự kiện tạm dừng game
    public void ResumeGame() // Gọi sự kiện tiếp tục game
    {
        Time.timeScale = 1;
        OnResumeGame?.Invoke();
    }  
    //public void OpenShop() => OnOpenShop?.Invoke(); // Gọi sự kiện mở shop
    public void CloseShop() => OnCloseShop?.Invoke(); // Gọi sự kiện mở shop
    public void ChooseHero() => OnChooseHero?.Invoke(); 
    public void CloseChooseHero() => OnCloseChooseHero?.Invoke();
    public void CompleteChoose()=> OnCompleteChoose?.Invoke();
    public void SelectMap()=> OnSelectMap?.Invoke(); 
    public void CloseMap()=> OnCloseMap?.Invoke(); 
    public void SelectMapComplete() => OnSelectMapComplete?.Invoke();
}