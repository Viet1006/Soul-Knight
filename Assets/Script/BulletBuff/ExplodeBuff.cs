using UnityEngine;
public class ExplodeBuff : BaseBulletBuff , IBuffTriggeredBullet
{
    public float explosionRadius;
    public override void ApplyBuff(Collider2D collider)
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        bullet.HandleCollision();
        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(transform.position,explosionRadius,LayerMask.GetMask("Enemy")+LayerMask.GetMask("Player"));
        foreach(Collider2D hittedObject in hittedObjects)
        {
            bullet.HandleOnObject(hittedObject);
        }
    }
}
