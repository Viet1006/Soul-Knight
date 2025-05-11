using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : StraightBullet 
{
    readonly float rotateSpeed = 120;
    protected Transform target;
    protected bool isTargetDie; // Kiểm tra xem mục tiêu còn tồn tại ko
    protected Vector2 targetPos; // Lưu vị trí đích nếu target chết trong khi mây chưa đánh
    protected bool useUpdate = true;
    public virtual BaseBullet SetFollowBullet(float speed, int damage, int critChance, BulletElement element, List<BulletBuff> bulletBuffs, Transform target, float timeLife = 0)
    {
        isTargetDie = false;
        if(target)
        {
            this.target = target;
            if(target.TryGetComponent(out EnemyController enemy)) enemy.OnReset += OnTargetDie;
        }
        return base.SetBullet(speed, damage, critChance, element, bulletBuffs, timeLife);
    }
    protected override void FixedUpdate()
    {
        if(!useUpdate) return;
        if (target != null)
        {
            // Tính hướng đến mục tiêu
            Vector2 direction = (target.position - transform.position).normalized;
            
            // Tính góc hiện tại và góc mục tiêu
            float currentAngle = GetAngle(transform.right);
            float targetAngle = GetAngle(direction);
            
            // Tính toán góc quay với tốc độ rotateSpeed
            float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotateSpeed * Time.fixedDeltaTime);
            
            // Áp dụng phép quay
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        base.FixedUpdate(); // vẫn gọi hàm di chuyển
    }
    protected float GetAngle(Vector2 vector) //Đổi vector sang góc từ 0-360 độ
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
    protected void OnTargetDie()
    {
        isTargetDie = true;
        targetPos = target.position;
    }
}