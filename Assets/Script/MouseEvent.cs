using UnityEngine;
using UnityEngine.InputSystem;

public class MouseEvent : MonoBehaviour
{
    public static MouseEvent instance;
    public event System.Action<Vector2> OnLeftMousePerformed;
    void Awake() => instance = this;

    public void OnLeftPerformed(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            OnLeftMousePerformed?.Invoke(Camera.main.ScreenToWorldPoint(Pointer.current.position.ReadValue())); // Lấy vị trí chuột để gửi tin nhắn tới subscriber
        }
    }
}
