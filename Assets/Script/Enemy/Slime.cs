using UnityEngine;

public class Slime : EnemyWithoutWeapon
{
    public int numberOfBullet;
    protected override void StartAttack()
    {
        for(int i=0 ; i<numberOfBullet ; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0,0,Random.Range(0,360))).GetComponent<BaseBullet>().SetBullet(damageBullet ,speedBullet ); // Spawn đạn lung tung
            ResetTimeToAttack();
        }
    }
}
