using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] private Rigidbody2D rb2D;
    public Animator animator;
    public bool canMove = true;
    public Vector2 moveInputValue;
    void Start()
    {
        UIManageShowAndHide.Instance.OnCloseShop += () => moveInputValue = Vector2.zero;
        UIManageShowAndHide.Instance.OnResumeGame += () => moveInputValue = Vector2.zero;
    }
    private void FixedUpdate()
    {
        // Áp dụng gia tốc , V new = V current + (V target - V current) * accleration * time
        if (canMove)
        {
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, moveInputValue * speed, HeroData.acceleration * Time.fixedDeltaTime);
        } 
        // Nếu không thể di chuyển, giảm tốc dần về 0
        else
        {
            rb2D.velocity = Vector2.Lerp(rb2D.velocity, Vector2.zero, HeroData.acceleration * Time.fixedDeltaTime);
        } 

        // Chuyển đổi trạng thái nhân vật
        if (rb2D.velocity.magnitude < 0.1f)  animator.SetInteger(Parameters.state, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if(!canMove || Time.timeScale == 0) return;
        moveInputValue = context.ReadValue<Vector2>();
        animator.SetInteger(Parameters.state, 1);
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb2D.velocity = velocity;
    }
}
