using System.Collections.Generic;
using UnityEngine;

public class MagicBook : BaseWeapon
{
    readonly float radiusCreate = 1.5f; 
    public override BaseBullet CreateBullet(Transform target)
    {
        Vector2 randomOffset = Random.insideUnitCircle * radiusCreate + (Vector2)spawnBulletPos.position;
        List <BulletBuff> finalBuffs = new(weaponData.bulletBuffs);
        if(addedBuff != null) finalBuffs.AddRange( addedBuff) ;
        return BulletPool.Instance
            .GetBullet<MagicBullet>(weaponData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,target.position)
            .SetMagicBullet(weaponData.speed // Set các giá trị
                ,weaponData.Damage(level)
                ,weaponData.CritChance(level)
                ,weaponData.element
                ,finalBuffs ,randomOffset,weaponData.bulletTimeLife);
            // Tạo đạn
    }
    public override void RotateToTarget(Transform target){}
}
