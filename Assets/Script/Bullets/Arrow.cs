using UnityEngine;

public class Arrow : BaseBullet
{
    public override void HandleCollision(Collider2D collider)
    {
        colliderBullet.enabled = false;
        transform.SetParent(collider.transform);
        enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = -19;
    }
}