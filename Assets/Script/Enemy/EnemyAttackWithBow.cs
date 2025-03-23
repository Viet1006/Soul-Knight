using System.Collections;
using UnityEngine;

public class EnemyAttackWithBow : EnemyWithWeapon
{
    float chargeTime = 2;
    float chargeTimeRemain;
    protected override void StartAttack()
    {
        chargeTimeRemain = chargeTime;
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
    }
}
