
using UnityEngine;

public class MissleBullet : MiniExploderBullet
{
    Transform target;
    readonly float findRadius = 3;
    protected override void FixedUpdate()
    {
        if(!target) 
        {
            target = FindTarget.GetNearestTransform(transform.position,findRadius , LayerMask.GetMask("Enemy"));
            if(target)
            {
                EnemyController enemyController = target.GetComponent<EnemyController>();
                enemyController.OnReset += () => target = null; // Đăng ký sự kiện khi mục tiêu chết
            }
        }
        else 
        {
            transform.right = (target.transform.position - transform.position).normalized;
        }
        base.FixedUpdate();
    }
    void OnDisable()
    {
        target = null;
    }
}
