using UnityEngine;

public class DuplicateBullet : StraightBullet
{
    int duplicateCount;
    GameObject childBullet;
    float timeLife;
    int scale;
    public void SetDuplicateBullet(float speed, int damage,int duplicateCount , float timeLife ,GameObject childBullet , int scale)
    {
        this.duplicateCount = duplicateCount;
        this.childBullet = childBullet;
        this.timeLife = timeLife;
        this.scale = scale;
        transform.localScale = Vector2.one * scale;
        base.SetBullet(speed, damage, 0, BulletElement.NoElement, null, timeLife);
    }
    protected override void OutOfTime()
    {
        if(duplicateCount >1)
        {
            for(int i = -1 ; i<= 1  ; i ++)
            {
                BulletPool.Instance.GetBullet<DuplicateBullet>(childBullet , transform.position , transform.rotation * Quaternion.Euler(0,0,i*20))
                    .SetDuplicateBullet(speed,GetChildDamage(),duplicateCount - 1 , timeLife , childBullet,GetScale());
            }
        }else if(duplicateCount == 1)
        {
            for(int i = -1 ; i<= 1  ; i ++)
            {
                BulletPool.Instance.GetBullet<DuplicateBullet>(childBullet , transform.position , transform.rotation * Quaternion.Euler(0,0,i*20))
                    .SetDuplicateBullet(speed,GetChildDamage(),duplicateCount - 1 , 3 , childBullet,GetScale());
            }
        }
        base.OutOfTime();
    }
    int GetChildDamage()
    {
        if(damage - 1 <=0) return 1;
        return damage - 1;
    }
    int GetScale()
    {
        if(scale - 1 <=0) return 1;
        return scale - 1;
    }
}
