
using Unity.Mathematics;
using UnityEngine;

public class FlyingEyes : BaseEnemy
{
    [SerializeField] float damage;
    [SerializeField] float bulletSpeed;
    [SerializeField] float childeBulletSpeed;
    [SerializeField] float timmLife;
    [SerializeField] GameObject bullet;
    float timeToAttack; // Thời gian đến đợt tấn công kế tiếp
    protected override void Attack(Vector2 Target)
    {
        GameObject newBullet = Instantiate(bullet,transform.position,Quaternion.identity);
        newBullet.transform.right = Target - (Vector2)transform.position;
        newBullet.GetComponent<SurroundedBullet>().SetSurroundedBullet(bulletSpeed,damage,childeBulletSpeed);
    }
    protected virtual void Update()
    {
        nearestPlayer = FindTarget.GetNearestObject(transform.position,enemyData.radiusFindPlayer,LayerMask.GetMask("Player"));
        timeToAttack -= Time.deltaTime;
        if(nearestPlayer != null && timeToAttack <= 0)
        {
            Attack(nearestPlayer.transform.position);
            timeToAttack = enemyData.AttackRate;
        }else{
            target = null;
        }
        FlipToTarget();
    }
    public override void Die()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        base.Die();
    }
}
