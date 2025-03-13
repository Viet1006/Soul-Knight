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
                timeToNextFire = 1/weaponData.fireRate;
                animator.SetTrigger(Parameters.attack + "1");
                currentAttack += 1;
            }else if(currentAttack == 2)
            {
                timeToNextFire = 1/weaponData.fireRate;
                animator.SetTrigger(Parameters.attack + "2");
                currentAttack = 1;
            }
        }
    }
    public void CreateAttackZone1()
    {
        Instantiate(weaponData.bullet,spawnBulletPos.position,transform.rotation).GetComponent<BaseBullet>().SetBullet(weaponData.damage,weaponData.bulletSpeed);
    }
    public void CreateAttackZone2()
    {
        Instantiate(weaponData.bullet,spawnBulletPos.position,transform.rotation * Quaternion.Euler(180,0,0)).GetComponent<BaseBullet>().SetBullet(weaponData.damage,weaponData.bulletSpeed);
    }
}
