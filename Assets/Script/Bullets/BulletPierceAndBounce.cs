using UnityEngine;

public class BulletPierceAndBounce : BaseBullet
{
    public int pierceCount;
    public int bounceCount;
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Wall"))
        {
            if(bounceCount > 0 || bounceCount <= -1) // Đặt -1 nếu muốn bật vô hạn
            {
                Bounce(collider);
                bounceCount-=1;
                return;
            }
        }else
        {
            HandleOnObject(collider);
            if(pierceCount >0 || pierceCount <= -1) // Đặt -1 nếu muốn xuyên vô hạn 
            {
                pierceCount -=1;
                return;
            }
        }
        HandleCollisionEffect(collider);
    }
    void Bounce(Collider2D collider) // Đổi hướng đạn
    {
        Vector3 contact = collider.ClosestPoint(transform.position);
        Vector2 normal = (transform.position - contact).normalized; 
        transform.right = Vector2.Reflect(transform.right,normal);
    }
}
