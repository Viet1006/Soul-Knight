
using System.Collections;
using UnityEngine;

public class EnemyUseGun : EnemyWithWeapon
{
    public float timeAttack;
    float timeAttackRemain;
    protected override void StartAttack()
    {
        StartCoroutine(AttackIEnum());
    }
    IEnumerator AttackIEnum()
    {
        while (timeAttackRemain > 0)
        {
            timeAttackRemain -= Time.deltaTime;
            weapon.Attack(target);
            yield return null;
        }
        ResetTimeToAttack();
        timeAttackRemain = timeAttack;
    }
}
