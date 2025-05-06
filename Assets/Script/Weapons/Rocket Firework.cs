using System.Collections.Generic;
using UnityEngine;

public class RocketFirework : BaseWeapon
{
    SpriteRenderer spriteRenderer;
    [SerializeField] LayerMask hitLayer;
    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        base.Update();
        if(timeToNextFire >= 1/weaponData.FireRate(level))
        {
            spriteRenderer.enabled = false;
        }else{
            spriteRenderer.enabled = true;
        }
    }
    public override BaseBullet CreateBullet(Transform target)
    {
        List <BulletBuff> finalBuffs = new(weaponData.bulletBuffs);
        if(addedBuff != null) finalBuffs.AddRange( addedBuff) ;
        return BulletPool.Instance
            .GetBullet<Firework>(weaponData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,transform.rotation * Quaternion.Euler(0,0,Random.Range(-weaponData.inaccuracy,weaponData.inaccuracy)))// Lấy baseBUllet từ bullet vừa tạo 
            .SetFireWork(weaponData.speed // Set các giá trị
                ,weaponData.Damage(level)
                ,weaponData.CritChance(level)
                ,weaponData.element
                ,finalBuffs ,hitLayer , weaponData.bulletTimeLife);
            // Tạo đạn
    }
}
