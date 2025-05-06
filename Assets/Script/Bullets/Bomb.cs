using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bomb : BaseBullet
{
    public void SetBomb(int damage,int critChance ,BulletElement element, List<BulletBuff> bulletBuffs)
    {
        base.SetBullet(0, damage, critChance,element,bulletBuffs);
        transform.DOMoveY(transform.position.y -3f, 0.2f)
            .SetEase(Ease.InQuad)
            .OnComplete(Explode);
    }
    void Explode()
    {
        new ExplosiveBuff(damage , LayerMask.GetMask("Enemy")).TryHandleCollision(null , transform.position);
        ReturnToPool();
    }
}
