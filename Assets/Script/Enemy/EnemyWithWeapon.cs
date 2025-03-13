using UnityEngine;

public abstract class EnemyWithWeapon : BaseEnemy
{
    public BaseWeapon weapon;
    protected override void Update()
    {
        base.Update();
        if(target) weapon.RotateToTarget(target);
        else weapon.transform.localRotation = Quaternion.identity;
    }
}
