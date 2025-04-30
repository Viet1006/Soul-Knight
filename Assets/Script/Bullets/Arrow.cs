using DG.Tweening;
using UnityEngine;
public class Arrow : BulletPierceAndBounce
{
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void HandleCollisionEffect(Collider2D collider)
    {
        bulletCollider.enabled = false;
        transform.SetParent(collider.transform);
        spriteRenderer.sortingOrder = -19;
        speed = 0;
    }
    public override void ReturnToPool()
    {
        spriteRenderer.sortingOrder = 0;
        lifeTimer.Kill();
        BulletPool.Instance.ReturnBullet(gameObject);
        speed=0;
        bulletCollider.enabled = false;
    }
}