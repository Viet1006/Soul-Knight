
using UnityEngine;

public class AttackZone : BaseBullet
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("EnemyBullet"))
        {
            collider.gameObject.GetComponent<BaseBullet>().HandleCollision(collider);
        }
        HandleOnObject(collider);
    }
}