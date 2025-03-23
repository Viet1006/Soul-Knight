using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    [SerializeField] protected Animator animator;
    protected bool isAttacking; // Để tránh quay vũ khí trong khi đang đánh
    public override void RotateToTarget(Transform target)
    {
        if(isAttacking) return;
        base.RotateToTarget(target);
    }
}
