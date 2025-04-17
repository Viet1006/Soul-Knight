using UnityEngine;

public class StraightBullet : BaseBullet
{
    protected virtual void Update()
    {
        transform.position += speed * Time.deltaTime *transform.right;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        HandleCollisionEffect(collider); // Gọi hàm xử lý va chạm
        if(collider.CompareTag("Wall")) return;
        HandleOnObject(collider);
    }
    public override void HandleCollisionEffect(Collider2D collider) // Xử lý hiệu ứng va chạm
    {
        speed = 0;
        transform.SetParent(null);
        animator.SetTrigger(Parameters.explode);
        bulletCollider.enabled = false;
        if(explodeEffect) explodeEffect.Play();
    }
}
