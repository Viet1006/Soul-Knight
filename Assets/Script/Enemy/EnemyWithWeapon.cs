using UnityEngine;

public abstract class EnemyWithWeapon : AttackMethodEnemy
{
    public BaseWeapon weapon;
    protected override void Update()
    {
        base.Update();
        if( !weapon) return;
        if(target) weapon.RotateToTarget(target);
        else weapon.transform.localRotation = Quaternion.identity;
    }
    void OnDestroy()
    {
        weapon.ResetToOringin();
        weapon = null;
    }
}