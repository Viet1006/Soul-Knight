using UnityEngine;
// Các đạn di chuyển theo đường thẳng
public class StraightBullet : BaseBullet
{
    protected virtual void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime *transform.right;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        HandleCollision(collider); // Gọi hàm xử lý va chạm
        if(collider.CompareTag("Wall")) return;
        HandleOnObject(collider);
    }
    public override void HandleCollision(Collider2D collider) // Xử lý hiệu ứng va chạm
    {
        base.HandleCollision(collider);
        speed = 0;
        transform.SetParent(null);
        if(explodeEffect) ExplodeEffectPool.Instance.GetExplodeEffect(explodeEffect,transform.position);
        ReturnToPool();
    }
}
