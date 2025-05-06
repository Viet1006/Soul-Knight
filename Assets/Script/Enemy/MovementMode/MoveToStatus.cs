using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(AIDestinationSetter))]
public class MoveToStatus : AIPath
{
    protected AIDestinationSetter destinationSetter;
    Animator animator;
    public event System.Action OnTarget;
    int blockCount; // Đếm các lệnh block move
    protected override void Awake()
    {
        base.Awake();
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = Status.instance.transform;
        animator = GetComponent<Animator>();
    }
    public void SetSpeed(float speed)
    {
        maxSpeed = speed;
    }
    public void StopMove()
    {
        blockCount += 1;
        canMove = false; // Dừng di chuyển
        if(animator)animator.speed = 0; // Dừng animation
    }
    public void ContinueMove()
    {
        blockCount -= 1;
        if(blockCount == 0)
        {
            canMove = true;
            if(animator)animator.speed = 1;
        }
    }
    public override void OnTargetReached()
    {
        if(destinationSetter.target == Status.instance.transform)
        {
            Status.instance.GetDamage(1);
            OnTarget?.Invoke();
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if(animator)animator.speed = 1;
        canMove = true;
        blockCount = 0;
    }
}