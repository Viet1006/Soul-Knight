using System.Collections.Generic;
using UnityEngine;

public class BulletPierceAndBounce : StraightBullet
{
    public int pierceCount;
    public int bounceCount;
    int pierceCountTemp;
    int bounceCountTemp;
    public override BaseBullet SetBullet(float speed, int damage, int critChance, BulletElement element, List<BulletBuff> bulletBuffs, float timeLife = 0)
    {
        pierceCountTemp = pierceCount;
        bounceCountTemp = bounceCount;
        return base.SetBullet(speed, damage, critChance, element, bulletBuffs, timeLife);
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Wall"))
        {
            if(bounceCountTemp > 0 || bounceCountTemp <= -1) // Đặt -1 nếu muốn bật vô hạn
            {
                Bounce(collider);
                bounceCountTemp-=1;
                return;
            }
        }else
        {
            HandleOnObject(collider);
            if(bulletBuffs != null)
            {
                foreach(BulletBuff bulletBuff in bulletBuffs)
                {
                    bulletBuff?.TryHandleCollision(collider , transform.position);
                }
            }
            if(pierceCountTemp >0 || pierceCountTemp <= -1) // Đặt -1 nếu muốn xuyên vô hạn 
            {
                pierceCountTemp -=1;
                return;
            }
        }
        HandleCollision(collider);
    }
    void Bounce(Collider2D collider) // Đổi hướng đạn
    {
        Vector3 contact = collider.ClosestPoint(transform.position);
        Vector2 normal = (transform.position - contact).normalized; 
        transform.right = Vector2.Reflect(transform.right,normal);
    }
}
