using System.Collections.Generic;
using UnityEngine;

public class Firework : StraightBullet
{
    LayerMask hitLayer;
    public BaseBullet SetFireWork(float speed, int damage,int critChance,BulletElement element ,List<BulletBuff> bulletBuffs, LayerMask hitLayer,float timeLife = 0)
    {
        this.hitLayer = hitLayer;
        return base.SetBullet(speed,damage,critChance,element , bulletBuffs , timeLife);
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(RandomChance.RollChance(critChance))
        {
            new ExplosiveBuff(damage * 2,hitLayer,100, true).TryHandleCollision(collider , transform.position );
        } else new ExplosiveBuff(damage,hitLayer).TryHandleCollision(collider , transform.position );
        
        HandleCollision(collider);
        ApplyBuffOnObject(collider);
    }
}