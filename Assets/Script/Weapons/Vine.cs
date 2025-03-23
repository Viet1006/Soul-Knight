using UnityEngine;

public class Vine : MeleeWeapon
{
    public float delayTime;
    public override void Attack(Transform target)
    {
        if(timeToNextFire<=0)
        {
            timeToNextFire = 1/weaponData.fireRate;
            animator.SetTrigger(Parameters.attack);
            Invoke(nameof(CreateAttackZone),delayTime);
            isAttacking = true;
        }
    }
    public void CreateAttackZone()
    {
        BulletPool.instance.GetBullet(weaponData.bullet,spawnBulletPos.position,transform.rotation).GetComponent<BaseBullet>().SetBullet(0,weaponData.damage,RandomChance.TryCrit(weaponData.critChance),weaponData.elements,0.2f);
        isAttacking = false;
    }
}
