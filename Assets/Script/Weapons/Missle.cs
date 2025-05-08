
using UnityEngine;

public class Missle : BaseWeapon
{
    public override BaseBullet CreateBullet(Transform target)
    {
        for(int i = -1 ; i<=1 ;i++)
        {
            BaseBullet newBullet =  base.CreateBullet(target);
            newBullet.transform.rotation = transform.rotation * Quaternion.Euler(0,0,i * 20);
        }
        return null;
    }
}
