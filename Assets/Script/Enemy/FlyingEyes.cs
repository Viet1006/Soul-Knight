using UnityEngine;

public class FlyingEyes : EnemyWithoutWeapon
{
    [SerializeField] GameObject childBullet;
    [SerializeField] float childBulletSpeed;
    [SerializeField] int numberOfBullet;
    protected override void StartAttack()
    {
        BulletPool.Instance.GetBullet(bulletData.bulletPrefab, transform.position, target.position)
            .GetComponent<SurroundedBullet>()
             // Tốc độ đạn , sát thương đạn con , tốc độ đạn con , critchance , bullet buffs là null , số lượng đạn và timeLife là 3
            .SetSurroundedBullet(bulletData.speed,bulletData.damage,childBulletSpeed,0,BulletElement.NoElement,null,3f,childBullet,numberOfBullet);
        ResetTimeToAttack();
    }
}