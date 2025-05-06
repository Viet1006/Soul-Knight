using System.Collections.Generic;
using DG.Tweening;
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
        new ExplosiveBuff(damage,hitLayer).TryHandleCollision(collider , transform.position);
        base.OnTriggerEnter2D(collider);
        ReturnToPool();
    }
    public override void ReturnToPool()
    {
        lifeTimer.Kill();
        BulletPool.Instance.ReturnBullet(this);
    }
}