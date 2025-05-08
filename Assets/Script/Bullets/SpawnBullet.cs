using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class SpawnBullet : BulletPierceAndBounce
{
    [SerializeField] GameObject basicBullet;
    Tween spawnTween;
    public override BaseBullet SetBullet(float speed, int damage, int critChance, BulletElement element, List<BulletBuff> bulletBuffs, float timeLife = 0)
    {
        spawnTween = DOVirtual.DelayedCall(0.2f , () => 
        {
            BulletPool.Instance.GetBullet<BaseBullet>(basicBullet , transform.position,transform.rotation)
                .SetBullet(0.5f,1,0,BulletElement.NoElement , null , 3);
        }, false).SetLoops(-1);;
        return base.SetBullet(speed, damage, critChance, element, bulletBuffs, timeLife);
    }
    public override void ReturnToPool()
    {
        spawnTween.Kill();
        base.ReturnToPool();
    }
}
