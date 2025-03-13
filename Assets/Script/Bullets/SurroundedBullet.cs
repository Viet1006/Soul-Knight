
using UnityEngine;

public class SurroundedBullet : BaseBullet
{
    GameObject childBullet;
    [SerializeField]float childBulletSpeed;
    public float childBulletDamage;
    int numberOfBullet;
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
    public void SetSurroundedBullet(float speed, float childBulletDamage,float childBulletSpeed,GameObject childBullet,int numberOfBullet,Vector2 right)
    {
        this.speed = speed;
        this.childBulletDamage = childBulletDamage;
        this.childBulletSpeed = childBulletSpeed; 
        this.childBullet = childBullet;
        this.numberOfBullet = numberOfBullet;
        transform.right = right;
    }
}
