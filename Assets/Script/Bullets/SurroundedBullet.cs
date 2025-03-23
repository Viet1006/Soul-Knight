using UnityEngine;
public class SurroundedBullet : BaseBullet
{
    public void SetSurroundedBullet(float speed, float childBulletDamage,float childBulletSpeed,GameObject childBullet,int numberOfBullet,float timeLife)
    {
        this.speed = speed;
        for(int i=0 ; i < numberOfBullet;i++)
        {
            GameObject newBullet = BulletPool.instance.GetBullet(childBullet,transform.position,Quaternion.Euler(0,0,360/numberOfBullet*i));
            newBullet.GetComponent<BaseBullet>().SetBullet(childBulletSpeed,childBulletDamage,false,bulletElement,timeLife);
            newBullet.transform.SetParent(transform);
        }
        StartCounter(timeLife);
    }
}