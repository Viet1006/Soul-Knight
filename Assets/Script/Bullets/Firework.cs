using UnityEngine;

public class Firework : StraightBullet
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        new BulletBuff(BulletBuffType.Explode , 0 , damage).ApplyBuff(collider,transform.position);
        ApplyAllBuff(collider);
        ReturnToPool();
    }
}
