using UnityEngine;
public class PierceBuff : BaseBulletBuff , IBuffTriggeredBullet
{
    public int pierceCount;
    public override void ApplyBuff(Collider2D collider)
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        BaseEnemy hittedEnemy = collider.gameObject.GetComponent<BaseEnemy>();
        if(!hittedEnemy || pierceCount == 0 )
        {
            bullet.HandleCollision(collider);
        } else
        {
            hittedEnemy.GetHit(bullet.damage,bullet.bulletData.colorDamage);
            pierceCount -= 1;
        }
    }
}
