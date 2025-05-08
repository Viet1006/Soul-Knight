using DG.Tweening;
using UnityEngine;
public class Arrow : StraightBullet
{
    SpriteRenderer spriteRenderer;
    public Collider2D bulletCollider;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override void HandleCollision(Collider2D collider)
    {
        base.HandleCollision(collider); 
        bulletCollider.enabled = false;
        transform.SetParent(collider.transform);
        spriteRenderer.sortingOrder = -19;
        speed = 0;
    }
    public override void ReturnToPool()
    {
        spriteRenderer.sortingOrder = 0;
        lifeTimer.Kill();
        DOVirtual.DelayedCall(5,() => BulletPool.Instance.ReturnBullet(this),false);
        speed=0;
        bulletCollider.enabled = false;
    }
    void OnEnable()
    {
        bulletCollider.enabled = true;
    }
}