using UnityEngine;

public class Assualt : BaseWeapon
{
    [SerializeField] GameObject explodeBullet;
    public override BaseBullet CreateBullet(Transform target)
    {
        if(RandomChance.RollChance(20))
        {
            BulletPool.Instance.GetBullet<BaseBullet>(explodeBullet, spawnBulletPos.position,Vector2.one * 1.5f,transform.rotation)
                .SetBullet(weaponData.speed* 0.7f , weaponData.Damage(level) * 2 ,weaponData.CritChance(level) ,BulletElement.Fire ,null ,3);
        }
        return base.CreateBullet(target);
    }
}
