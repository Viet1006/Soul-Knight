using System.Collections;
using UnityEngine;

public class EnemyAttackWithBow : EnemyWithWeapon
{
    public float chargeTime;
    float chargeTimeRemain;
    protected override void StartAttack()
    {
        StartCoroutine(AttackIEnum());
    }
    IEnumerator AttackIEnum()
    {
        while(chargeTimeRemain > 0)
        {
            weapon.Attack(target);
            chargeTimeRemain -= Time.deltaTime;
            yield return null;
        }
        weapon.StopAttack();
        chargeTimeRemain = chargeTime;
        ResetTimeToAttack();
    }
}
