using UnityEngine;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem instance; // Singleton instance
    void Awake() => instance = this;
    [SerializeField] GameObject notificationPrefab; // Prefab của thông báo
    public void ShowNotification(string message, float duration)
    {
        GameObject notification = Instantiate(notificationPrefab, transform); // Tạo thông báo mới
        notification.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message; // Đặt nội dung thông báo
        Destroy(notification, duration); // Hủy thông báo sau một khoảng thời gian
    }
}