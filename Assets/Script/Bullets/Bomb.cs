using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bomb : BaseBullet
{
    public void SetBomb(float speed, int damage,int critChance ,BulletElement element, List<BulletBuff> bulletBuffs  ,float timeLife = 0)
    {
        base.SetBullet(speed, damage, critChance,element,bulletBuffs, timeLife);
        transform.DOMoveY(transform.position.y -3f, 0.2f)
            .SetEase(Ease.InQuad)
            .OnComplete( Explode);;
        bulletCollider.enabled = false;
    }
    void Explode()
    {
        animator.SetTrigger(Parameters.explode);
        bulletCollider.enabled = true;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        HandleOnObject(collider);
    }
}
