using UnityEngine;
[DisallowMultipleComponent]
public class PierceBuff : BaseBulletBuff , IBuffTriggeredBullet
{
    public int pierceCount;
    public override void ApplyBuff(Collider2D collider)
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Nếu va với tường hoặc hết lượt xuyên thì dừng
        Debug.Log(collider.name);
        if(collider.gameObject.layer == LayerMask.NameToLayer("Wall") || pierceCount == 0 )
        {
            bullet.HandleCollision();
            bullet.HandleOnObject(collider);
        } else
        {
            bullet.HandleOnObject(collider);
            pierceCount -= 1;
        }
    }
}
