using UnityEngine;

public class ExplodeBullet : BaseBullet
{
    public float explodeRadius;
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(transform.position,explodeRadius,~LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer).Replace("Bullet", "")));
        foreach (Collider2D hittedObject in hittedObjects)
        {
            HandleOnObject(hittedObject);
        }
        HandleCollisionEffect(collider);
    }
}
