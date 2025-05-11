
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : FollowBullet
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public override BaseBullet SetFollowBullet(float speed, int damage, int critChance, BulletElement element, List<BulletBuff> bulletBuffs, Transform target, float timeLife = 0)
    {
        StartCoroutine(MoveToTarget());
        useUpdate =  false;
        return base.SetFollowBullet(speed, damage, critChance, element, bulletBuffs, target, timeLife);
    }
    IEnumerator MoveToTarget()
    {
        yield return new WaitUntil(() => target != null);
        while((Vector2.Distance(transform.position , target.position + Vector3.up * 2) > 0.1f && !isTargetDie) // Nếu target chưa chết thì kiểm tra theo transform
        || (Vector2.Distance(transform.position , targetPos + Vector2.up *2) > 0.1f && isTargetDie)) // Nếu chết r thì kiểm tra theo pos
        {
            if(! isTargetDie) // Nếu target chưa chết
            {
                // Di chuyển theo transform
                transform.position = Vector2.MoveTowards(transform.position , target.position + Vector3.up * 2 , speed * Time.deltaTime); // Di chuyển theo transform của target
            } else 
            {
                // Nếu chết rồi di chuyển lại nơi chết
                transform.position = Vector2.MoveTowards(transform.position , targetPos + Vector2.up * 2 , speed * Time.deltaTime); // Di chuyển theo transform của target
            }
            yield return null;
        }
        animator.SetTrigger(Parameters.explode);
        if(!isTargetDie) HandleOnObject(target.GetComponent<Collider2D>()); // Nếu targert chưa chết thì mới gọi getDame
    } 
}