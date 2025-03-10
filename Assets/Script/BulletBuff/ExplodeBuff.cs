using UnityEngine;
public class ExplodeBuff : BaseBulletBuff , IBuffTriggeredBullet
{
    public float explosionRadius;
    void Awake()
    {
    }
    public override void ApplyBuff(Collider2D collider)
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        bullet.HandleCollision(collider);
        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(transform.position,explosionRadius,LayerMask.GetMask("Enemy")+LayerMask.GetMask("Player"));
        foreach(Collider2D hittedObject in hittedObjects)
        {
            bullet.HandleOnObject(hittedObject);
        }
    }
}
