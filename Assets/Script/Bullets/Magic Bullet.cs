using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MagicBullet : StraightBullet
{
    Transform target;
    readonly float findRadius = 3;
    bool isDone; // Kiểm tra xem di chuyển xong chưa
    Tween startMove;
    public BaseBullet SetMagicBullet(float speed, int damage, int critChance, BulletElement element, List<BulletBuff> bulletBuffs,Vector2 startPos, float timeLife = 0)
    {
        isDone = false;
        target = null;
        startMove =  transform.DOMove (startPos , 0.5f).OnComplete(() => isDone = true); 
        return base.SetBullet(speed, damage, critChance, element, bulletBuffs, timeLife);
    }
    protected override void FixedUpdate()
    {
        if(!isDone) return;
        if(!target) 
        {
            target = FindTarget.GetNearestTransform(transform.position,findRadius , LayerMask.GetMask("Enemy"));
        }
        else 
        {
            transform.right = (target.transform.position - transform.position).normalized;
        }
        base.FixedUpdate();
    }
    void OnDisable()
    {
        startMove.Kill(false);
    }
}
