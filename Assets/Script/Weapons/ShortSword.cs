using UnityEngine;

public class ShortSword : MeleeWeapon
{
    float currentAttack = 1;
    public override void Attack(Transform target)
    {
        if(timeToNextFire<0)
        {
            if(currentAttack == 1)
            {
                animator.SetTrigger(Parameters.attack + "1");
                currentAttack += 1;
            }else if(currentAttack == 2)
            {
                animator.SetTrigger(Parameters.attack + "2");
                currentAttack = 1;
            }
            timeToNextFire = 1/weaponData.FireRate(level);
        }
    }
    public void CreateAttackZone1()
    {
        BulletPool.Instance.GetBullet(weaponData.bulletPrefab,spawnBulletPos.position,transform.rotation)
            .GetComponent<BaseBullet>()
                .SetBullet(0,weaponData.Damage(level),weaponData.CritChance(level),weaponData.element,weaponData.bulletBuffs,0.2f);
    }
    public void CreateAttackZone2()
    {
        BulletPool.Instance.GetBullet(weaponData.bulletPrefab,spawnBulletPos.position,transform.rotation * Quaternion.Euler(180,0,0))
            .GetComponent<BaseBullet>()
            .SetBullet(0,weaponData.Damage(level),weaponData.CritChance(level),weaponData.element,weaponData.bulletBuffs,0.2f);
    }
}