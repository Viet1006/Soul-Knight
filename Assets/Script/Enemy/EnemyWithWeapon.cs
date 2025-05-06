using UnityEngine;

public abstract class EnemyWithWeapon : AttackMethodEnemy
{
    [SerializeField] protected Transform weapon;
    [SerializeField] protected Transform spawnBulletPos;
    protected override void Update()
    {
        if(target) RotateWeapon();
        else weapon.localRotation = Quaternion.identity;
        base.Update();
    }
    protected virtual void RotateWeapon()
    {
        Vector2 direction = (Vector2)target.position - (Vector2)weapon.position;
        // trả về góc từ 0 -> 90 với vector thuộc góc phần tư 1,3 và từ -90 -> 0 với góc phần tư 2,4
        if(direction != Vector2.zero)
        {
            float angle = Mathf.Atan(direction.y/direction.x) * Mathf.Rad2Deg;
            if(transform.rotation.y == 0 || direction.x == 0) // Khi quay sang bên trái thì angle khi x=0 vẫn được coi là dương khi tính góc nên ngược dấu => ko cần đổi dấu
            {
                weapon.localRotation = Quaternion.Euler(0,0,angle);
            }else{
                weapon.localRotation = Quaternion.Euler(0,0,-angle);
            }
        }
    }
    public override void CreateBullet()
    {
        BulletPool.Instance
            .GetBullet<BaseBullet>(enemyData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,weapon.rotation)
            .SetBullet(enemyData.bulletSpeed // Set các giá trị
                ,enemyData.damage
                ,critChance : 0
                ,enemyData.element
                ,enemyData.bulletBuffs ,enemyData.bulletTimeLife);
            // Tạo đạn
    }
}