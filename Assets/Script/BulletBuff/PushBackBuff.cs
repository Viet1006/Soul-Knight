using UnityEngine;
[DisallowMultipleComponent]
public class PushBackBuff : BaseBulletBuff
{
    [SerializeField] float distancePushBack;
    public override void ApplyBuff(Collider2D collider)
    { 
        if(collider.TryGetComponent<IPushable>(out IPushable pushable)) 
        {
            StartCoroutine(pushable.PushBackIEnum((collider.transform.position-transform.position).normalized,distancePushBack));
        }
    }
}