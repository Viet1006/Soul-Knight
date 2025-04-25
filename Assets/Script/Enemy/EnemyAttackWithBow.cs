using System.Collections;
using UnityEngine;

public class EnemyAttackWithBow : EnemyWithWeapon
{
    float chargeTime = 2;
    float chargeTimeRemain = 2; // Đặt bằng 2 ko cần Start
    Animator bowAnimator;
    void Start()
    {
        bowAnimator = weapon.GetComponentInChildren<Animator>(); // chuyển kiểu weapon sang bow
    }
    protected override void StartAttack()
    {
        weapon.timeToNextFire = 0;
        StartCoroutine(AttackIEnum());
    }
    public IEnumerator AttackIEnum()
    {
        while(chargeTimeRemain > 0)
        {
            weapon.Attack(target);
            chargeTimeRemain -= Time.deltaTime;
            yield return null;
        }
        weapon.StopAttack();
        ResetTimeToAttack();
        chargeTimeRemain = chargeTime;
    }
    public override void StopAttack()
    {
        base.StopAttack();
        bowAnimator.speed = 0;
    }
    public override void ContinueAttack()
    {
        base.ContinueAttack();
        bowAnimator.speed = 1;
    }
}
