using UnityEngine;

public class MiniExploderBullet : StraightBullet
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(RandomChance.RollChance(critChance))
        {
            new MiniExplode(damage * 2,LayerMask.GetMask("Enemy"),100, true).TryHandleCollision(collider , transform.position );
        } else new MiniExplode(damage,LayerMask.GetMask("Enemy")).TryHandleCollision(collider , transform.position );
        
        HandleCollision(collider);
        ApplyBuffOnObject(collider);
    }
}
