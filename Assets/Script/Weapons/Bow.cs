using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon
{
    public Animator animator;
    int chargeCount; // Đếm số lần nạp
    public float timeToNextLevelCharge; // thời gian đến mức nạp tiếp theo 
    bool isCharging;
    public float timeForEachLevelCharge; // Thời gian để thành 1 mức nạp
    [SerializeField] int damagePerCount;
    SpriteRenderer currentSquare;
    public override void Attack(Transform target) // Được gọi mỗi frame khi player giữ nút tấn công
    {
        if(timeToNextFire<0 && isCharging == false) // Kiểm tra xem có đạng nạp và tới thời gian chưa để bắn
        {
            isCharging = true;
            animator.SetTrigger(Parameters.charge); // Bắt đầu animation nạp đạn
            timeToNextLevelCharge = timeForEachLevelCharge;
            currentSquare = ChargingBar.instance.GetSquare(0);
            ChargingBar.instance.Show(transform);
        }
        if(isCharging) // Kiểm tra trạng thái đang nạp
        {
            timeToNextLevelCharge -= Time.deltaTime;
            currentSquare.color = Color.Lerp(Color.black,Color.white,1-timeToNextLevelCharge/timeForEachLevelCharge);
            if(timeToNextLevelCharge<0 && chargeCount < 4)
            {
                chargeCount++;
                currentSquare = ChargingBar.instance.GetSquare(chargeCount);
                timeToNextLevelCharge = timeForEachLevelCharge;
            }
            spawnBulletPos.gameObject.SetActive(true);
        }
    }
    public override void StopAttack() // Bắt đầu thả cung
    {
        if(!isCharging) return;
        isCharging = false;
        NotifyAttack();
        List <BulletBuff> finalBuffs = new(weaponData.bulletBuffs);
        if(addedBuff != null) finalBuffs.AddRange( addedBuff) ;
        BulletPool.Instance
            .GetBullet<BaseBullet>(weaponData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,transform.rotation * Quaternion.Euler(0,0,Random.Range(-weaponData.inaccuracy,weaponData.inaccuracy)))
            .SetBullet(weaponData.speed // Set các giá trị
                ,weaponData.Damage(level) + chargeCount * damagePerCount // tăng damage theo số lần nạp
                ,weaponData.CritChance(level)
                ,weaponData.element
                ,finalBuffs ,weaponData.bulletTimeLife);
            // Tạo đạn
        chargeCount = 0;
        ChargingBar.instance.Hide();
        animator.SetTrigger(Parameters.endAttack);
        timeToNextFire = 1/weaponData.FireRate(level);
        transform.localRotation = Quaternion.identity;
        spawnBulletPos.gameObject.SetActive(false);
    }
    public override void RotateToTarget(Transform target)
    {
        if(!isCharging) return; 
        base.RotateToTarget(target);
    }
}