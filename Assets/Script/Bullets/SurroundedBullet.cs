
using UnityEngine;

public class SurroundedBullet : BaseBullet
{
    [SerializeField]GameObject childBullet;
    [SerializeField]float childBulletSpeed;
    public float childBulletDamage;
    public int numberOfBullet =3;
    void Start()
    {
        for(int i=0 ; i < numberOfBullet;i++)
        {
            GameObject newBullet = Instantiate(childBullet,transform.position,Quaternion.identity);
            newBullet.transform.right = transform.right;
            newBullet.transform.Rotate(0,0,i * 360 / numberOfBullet);
            newBullet.GetComponent<BaseBullet>().SetBullet(childBulletDamage,childBulletSpeed);
            newBullet.transform.SetParent(transform);
        }
    }
    public void SetSurroundedBullet(float speed, float childBulletDamage,float childBulletSpeed)
    {
        this.speed = speed;
        this.childBulletDamage = childBulletDamage;
        this.childBulletSpeed = childBulletSpeed; 
    }
}
