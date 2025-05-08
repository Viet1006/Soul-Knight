using System.Collections.Generic;
using UnityEngine;

public class FollowBullet : StraightBullet
{
    readonly float rotateSpeed = 120;
    private Transform target;

    public BaseBullet SetFollowBullet(float speed, int damage, int critChance, BulletElement element, List<BulletBuff> bulletBuffs, Transform target, float timeLife = 0)
    {
        this.target = target;
        return base.SetBullet(speed, damage, critChance, element, bulletBuffs, timeLife);
    }

    protected override void FixedUpdate()
    {
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

    float GetAngle(Vector2 vector) //Đổi vector sang góc từ 0-360 độ
    {
        float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        return angle;
    }
}