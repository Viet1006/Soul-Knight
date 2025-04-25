using UnityEngine;
// Quản lý việc ẩn và hiện các Ui
public class UIManageShowAndHide
{
    static UIManageShowAndHide instance;
    public event System.Action OnPauseGame;
    public event System.Action OnResumeGame;
    public event System.Action OnOpenShop;
    public event System.Action OnCloseShop;
    void Awake() => instance = this;
    public void PauseGame() => OnPauseGame?.Invoke(); // Gọi sự kiện tạm dừng game
    public void ResumeGame() => OnResumeGame?.Invoke(); // Gọi sự kiện tiếp tục game
    public void OpenShop() => OnOpenShop?.Invoke(); // Gọi sự kiện mở shop
    public void CloseShop() => OnCloseShop?.Invoke(); // Gọi sự kiện mở shop
    public static UIManageShowAndHide Instance()
    {
        instance ??= new UIManageShowAndHide();
        return instance; 
    }
}