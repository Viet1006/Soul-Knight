using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bomb : BaseBullet
{
    public void SetBomb(int damage,int critChance ,BulletElement element, List<BulletBuff> bulletBuffs  ,float timeLife = 0)
    {
        base.SetBullet(0, damage, critChance,element,bulletBuffs, timeLife);
        transform.DOMoveY(transform.position.y -3f, 0.2f)
            .SetEase(Ease.InQuad)
            .OnComplete( Explode);;
        bulletCollider.enabled = false;
    }
    void Explode()
    {
        bulletCollider.enabled = true;
        
        ExplodeEffectPool.Instance.GetExplodeEffect(transform.position);
        DOVirtual.DelayedCall(0.1f, () => ReturnToPool()); // Delay để có thời gian Ontrigger
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        HandleOnObject(collider);
    }
}
