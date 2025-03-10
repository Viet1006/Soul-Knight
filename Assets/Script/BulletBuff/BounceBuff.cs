using UnityEngine;

public class BounceBuff : BaseBulletBuff , IBuffTriggeredBullet
{
    public int bounceCount;
    public override void ApplyBuff(Collider2D collider)
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        bullet.HandleOnObject(collider);
        if(bounceCount == 0 || collider.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            bullet.HandleCollision(collider);
        }else{
            Vector2 normal =  collider.ClosestPoint(transform.position)-(Vector2)transform.position; // lấy vector pháp tuyến để lật
            transform.right = Vector2.Reflect(transform.right,normal.normalized); // Đảo hướng dựa vào vector pháp tuyến
            bounceCount -= 1;
        }
    }
}
