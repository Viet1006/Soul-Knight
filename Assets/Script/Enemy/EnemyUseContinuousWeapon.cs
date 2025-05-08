using DG.Tweening;
using UnityEngine;
// Để cho những enemy sử dụng vũ khí tấn công nhiều lần trong 1 đợt tấn công
public class EnemyUseContinuousWeapon : EnemyWithWeapon
{
    [SerializeField]float timeAttack = 1;
    [SerializeField] float fireRate;
    [SerializeField] protected float inaccuracy;
    Tween attackTween;
    protected override void StartAttack()
    {
        attackTween = DOVirtual.DelayedCall(1/fireRate ,()=> {
            CreateBullet();
        },false).SetLoops((int)(fireRate * timeAttack))
        .OnComplete(ResetTimeToAttack);
    }
    public override void CreateBullet()
    {
        BulletPool.Instance
            .GetBullet<BaseBullet>(enemyData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,weapon.rotation * Quaternion.Euler(0,0,Random.Range(-inaccuracy,inaccuracy)))
            .SetBullet(enemyData.bulletSpeed // Set các giá trị
                ,enemyData.damage
                ,critChance: 0
                ,enemyData.element
                ,enemyData.bulletBuffs ,enemyData.bulletTimeLife);
    }
    public override void StopAttack()
    {
        base.StopAttack();
        attackTween.Kill(false);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        attackTween.Kill(false);
    }
}
