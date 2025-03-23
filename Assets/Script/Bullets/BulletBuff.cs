using UnityEngine;

public class BulletBuff : MonoBehaviour
{
    public float pushbackDistance;
    public void ApplyPassiveBuff(Collider2D collider,BulletElements bulletElements, bool isCritical)
    {
        if (pushbackDistance > 0) PushBackBuff(collider);
        if(isCritical || collider.CompareTag("Player")) // Đạn chí mạng hoặc là Player thì luôn apply buff
        {
            if(bulletElements == BulletElements.Lightning) StunBuff(collider);
        }
    }
    void PushBackBuff(Collider2D collider)
    {
        if(collider.TryGetComponent( out IPushable iPushable)) 
        iPushable.StartPushCoroutine(collider.transform.position-transform.position,pushbackDistance); 
    }
    void StunBuff(Collider2D collider)
    {
        if(collider.TryGetComponent( out ICanStun iCanStun)) iCanStun.StartStunCoroutine(1);
    }
}