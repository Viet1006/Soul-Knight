using UnityEngine;

public class BounceBuff : BaseBulletBuff , IBuffTriggeredBullet
{
    public int bounceCount;
    public override void ApplyBuff(Collider2D collider)
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        bullet.HandleOnObject(collider);
        if(bounceCount == 0)
        {
            bullet.HandleCollision();
        }else{
            Debug.Log("bat nay");
        }
    }
}
