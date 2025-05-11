using DG.Tweening;
using UnityEngine;

public class EnemyUseMeleeWeapon : EnemyWithWeapon
{
    public float delayTime;
    [SerializeField] Animator animator;
    Tween attackTween;
    protected override void StartAttack()
    {
        animator.SetTrigger(Parameters.prepareAttack);
        attackTween = DOVirtual.DelayedCall(delayTime ,() => 
            {
                CreateBullet();
                animator.SetTrigger(Parameters.attack);
                ResetTimeToAttack();
            },false);
    }
    protected override void OnDisable()
    {
        animator.Rebind();
        attackTween.Kill(false);
    }
    public override void StopAttack()
    {
        base.StopAttack();
        animator.Rebind();
        attackTween.Kill(false);
    }
}
