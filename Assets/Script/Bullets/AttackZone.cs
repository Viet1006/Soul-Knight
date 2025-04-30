using UnityEngine;
public class AttackZone : BaseBullet
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("EnemyBullet"))
        {
            collider.gameObject.GetComponent<BaseBullet>().ReturnToPool(); // Xóa các đạn
            return; // Nếu là đạn thì không cần xử lý trên đạn nữa
        }
        HandleOnObject(collider);
    }
}