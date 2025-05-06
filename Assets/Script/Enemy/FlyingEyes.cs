using UnityEngine;

public class FlyingEyes : AttackMethodEnemy
{
    [SerializeField] GameObject childBullet;
    [SerializeField] float childBulletSpeed;
    [SerializeField] int numberOfBullet;
    protected override void StartAttack()
    {
        BulletPool.Instance.GetBullet<SurroundedBullet>(enemyData.bulletPrefab, transform.position, target.position)
             // Tốc độ đạn , sát thương đạn con , tốc độ đạn con , critchance , bullet buffs là null , số lượng đạn và timeLife là 3
            .SetSurroundedBullet(enemyData.bulletSpeed,enemyData.damage,childBulletSpeed,0,BulletElement.NoElement,null,3f,childBullet,numberOfBullet);
        ResetTimeToAttack();
    }
}