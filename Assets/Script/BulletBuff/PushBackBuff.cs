using UnityEngine;
[DisallowMultipleComponent]
public class PushBackBuff : BaseBulletBuff
{
    [SerializeField] float distancePushBack;
    public override void ApplyBuff(Collider2D collider)
    { 
        if(collider.TryGetComponent(out IPushable pushable)) 
        {
            pushable.StartPushCoroutine((collider.transform.position-transform.position).normalized,distancePushBack);
        }
    }
}