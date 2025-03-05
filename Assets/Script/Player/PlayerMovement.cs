using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Animator animator;
    public Vector2 moveInputValue;
    public Vector2 getMoveDirection()
    {
        return moveInputValue;
    }
    private void FixedUpdate()
    {
        rb2D.velocity = moveInputValue * speed;
        if(moveInputValue == Vector2.zero)
        {
            animator.SetInteger(Parameters.state, StateEnum.IDLE);
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInputValue = context.ReadValue<Vector2>();
        animator.SetInteger(Parameters.state,StateEnum.RUN);
    }
}
