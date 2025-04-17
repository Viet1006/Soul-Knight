using UnityEngine;

public abstract class EnemyWithWeapon : AttackMethodEnemy
{
    public BaseWeapon weapon;
    protected override void Update()
    {
        if(target) weapon.RotateToTarget(target);
        else weapon.transform.localRotation = Quaternion.identity;
        base.Update();
    }
    void OnDestroy()
    {
        weapon.ResetToOringin(); // trả vũ khí về pool và reset lại trạng thái
        weapon = null;
    }
}