using UnityEngine;

public class Firework : StraightBullet
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        HandleCollisionEffect(collider);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.7f );
        foreach(Collider2D col in colliders)
        {
            HandleOnObject(col);
        }
    }
}
