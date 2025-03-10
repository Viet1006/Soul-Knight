using UnityEngine;

public class MeleeWeapon : BaseWeapon
{
    [SerializeField] protected Animator animator;
    protected bool isAttacking;
    public override void RotateToTarget()
    {
        if(isAttacking) return;
        base.RotateToTarget();
    }
    
}
