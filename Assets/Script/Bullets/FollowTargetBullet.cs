
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowTargetBullet : BaseBullet // viên đạn có khả năng theo dõi mục tiêu
{
    protected Transform target;
    protected Vector2 targetPos; // Lưu vị trí đích nếu target chết trong khi mây chưa đánh
    protected bool isTargetDie; // Kiểm tra xem mục tiêu còn tồn tại ko
    // Start is called before the first frame update
    public virtual void SetTargetBullet(float speed , int damage , int critChance,BulletElement element,List<BulletBuff> bulletBuffs,Transform target , float timeLife = 0)
    {
        isTargetDie = false;
        base.SetBullet(speed,damage,critChance,element,bulletBuffs,timeLife);
        this.target = target;
        target.GetComponent<EnemyController>().OnReset += OnTargetDie; // Đăng ký xem mục tiêu có chết không
    }
    protected void OnTargetDie()
    {
        isTargetDie = true;
        targetPos = target.position;
    }
}
