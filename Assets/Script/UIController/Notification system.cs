using DG.Tweening;
using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance; // Singleton instance
    void Awake()
    {
        Instance = this;
        textMeshPro = GetComponent<TMPro.TextMeshProUGUI>(); // Lấy TextMeshPro từ GameObject này
        gameObject.SetActive(false);
        ShowNotification("Chào mừng bạn tới game của tôi! Ấn vào anh hùng để xem thông tin", 2f); // Hiển thị thông báo chào mừng
    }
    Tween delayTween;
    TMPro.TextMeshProUGUI textMeshPro; // TextMeshPro để hiển thị thông báo
    public void ShowNotification(string message, float duration)
    {
        delayTween?.Kill(); // Hủy tween trước đó nếu có
        gameObject.SetActive(true); // Bật thông báo
        textMeshPro.text = message; // Đặt nội dung thông báo
        delayTween =DOVirtual.DelayedCall(duration , () => {})
            .OnKill( () => gameObject.SetActive(false));
    }
}