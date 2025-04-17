using UnityEngine;
public class AttackZone : BaseBullet
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("EnemyBullet"))
        {
            collider.gameObject.GetComponent<BaseBullet>().HandleCollisionEffect(collider);
        }
        HandleOnObject(collider);
    }
}