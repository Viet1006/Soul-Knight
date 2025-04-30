using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(AIDestinationSetter))]
public class MoveToStatus : AIPath
{
    protected AIDestinationSetter destinationSetter;
    Animator animator;
    public event System.Action OnTarget;
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
        canMove = false; // Dừng di chuyển
        if(animator)animator.speed = 0; // Dừng animation
    }
    public void ContinueMove()
    {
        canMove = true;
        if(animator)animator.speed = 1;
    }
    public override void OnTargetReached()
    {
        if(destinationSetter.target == Status.instance.transform)
        {
            Status.instance.GetDamage(1);
            OnTarget?.Invoke();
        }
    }
}