
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SkeletonKingBullet : FollowBullet
{
    bool startMove;
    [SerializeField] GameObject childBullet;
    public override BaseBullet SetFollowBullet(float speed, int damage, int critChance, BulletElement element, List<BulletBuff> bulletBuffs, Transform target, float timeLife = 0)
    {
        startMove = false;
        DOVirtual.DelayedCall(1,() => startMove = true , false);
        return base.SetFollowBullet(speed, damage, critChance, element, bulletBuffs, target,  timeLife);
    }
    protected override void FixedUpdate()
    {
        if(!startMove) return;
        base.FixedUpdate();
    }
    public override void ReturnToPool()
    {
        for(int i=0 ; i < 10;i++)
        {
            BulletPool.Instance
            .GetBullet<StraightBullet>(childBullet,transform.position,Quaternion.Euler(0,0,360/10*i))
                .SetBullet(5,1,0,element,null,3);
        }
        base.ReturnToPool();
    }
}
