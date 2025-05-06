using System.Collections.Generic;
using UnityEngine;
public class SurroundedBullet : StraightBullet // Empty object được bao quanh bởi các đạn con
{
    public void SetSurroundedBullet(float speed, int damage,float childSpeed,int critChance, BulletElement element,List<BulletBuff> bulletBuffs,float timeLife,GameObject childBullet,int numberOfBullet)
    {
        this.speed = speed;
        for(int i=0 ; i < numberOfBullet;i++)
        {
            BulletPool.Instance
            .GetBullet<StraightBullet>(childBullet,transform.position,Quaternion.Euler(0,0,360/numberOfBullet*i))
                .SetBullet(childSpeed,damage,critChance,element,bulletBuffs,timeLife);
        }
        StartLifeTimer(timeLife);
    }
}