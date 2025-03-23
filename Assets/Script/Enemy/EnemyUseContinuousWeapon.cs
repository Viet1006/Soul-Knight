using System.Collections;
using UnityEngine;
// Để cho những enemy sử dụng vũ khí tấn công nhiều lần trong 1 đợt tấn công
public class EnemyUseContinuousWeapon : EnemyWithWeapon
{
    float timeAttack = 1;
    float timeAttackRemain;
    protected override void StartAttack()
    {
        timeAttackRemain = timeAttack;
        StartCoroutine(AttackIEnum());
    }
    IEnumerator AttackIEnum()
    {
        while (timeAttackRemain > 0)
        {
            weapon.Attack(target);
            timeAttackRemain -= Time.deltaTime;
            yield return null;
        }
        ResetTimeToAttack();
    }
}
