// Sử dụng vũ khí tấn công 1 lần 1 đợt
using System.Collections;
using UnityEngine;

public class EnemyUseSingleWeapon : EnemyWithWeapon
{
    protected override void StartAttack()
    {
        weapon.timeToNextFire = 0f; // Để tấn công ngay lập tức
        weapon.Attack(target);
        ResetTimeToAttack();
    }
}