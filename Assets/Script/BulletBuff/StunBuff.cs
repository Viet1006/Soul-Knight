using UnityEngine;

public class StunBuff : BaseBulletBuff
{
    [Range(0,100)]
    public int stunRate;
    public float stunTime;
    [SerializeField] GameObject stunIcon;
    public override void ApplyBuff(Collider2D collider)
    {
        if(Random.Range(1,100)<=stunRate)
        {
            if(collider.TryGetComponent(out ICanStun stunable)) 
            {
                stunable.StartStunCoroutine(stunTime);
            }
        }
    }
}
