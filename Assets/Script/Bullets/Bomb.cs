using System.Collections.Generic;
using DG.Tweening;

public class Bomb : BaseBullet
{
    public void SetBomb(int damage,int critChance ,BulletElement element, List<BulletBuff> bulletBuffs )
    {
        base.SetBullet(0, damage, critChance,element,bulletBuffs);
        transform.DOMoveY(transform.position.y -3f, 0.2f)
            .SetEase(Ease.InQuad)
            .OnComplete( Explode);;
        bulletCollider.enabled = false;
    }
    void Explode()
    {
        new BulletBuff(BulletBuffType.Explode,0,damage).ApplyBuff(null , transform.position);
        foreach(BulletBuff bulletBuff in bulletBuffs) 
        {
            bulletBuff.ApplyBuff(null, transform.position);
        }
        DOVirtual.DelayedCall(0.1f, () => ReturnToPool()); // Delay để có thời gian Ontrigger
    }
}
