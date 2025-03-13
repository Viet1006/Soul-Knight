using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    [SerializeField] protected Animator animator;
    protected bool isAttacking;
    public override void RotateToTarget(Transform target)
    {
        if(isAttacking) return;
        base.RotateToTarget(target);
    }
}
