using UnityEngine;

public class Bow : BaseWeapon
{
    public Animator animator;
    int chargeCount; // Đếm số lần nạp
    public float timeToNextLevelCharge; // thời gian đến mức nạp tiếp theo 
    bool isCharging;
    public float timeForEachLevelCharge; // Thời gian để thành 1 mức nạp
    Arrow currentArrow;
    PlayerBow playerBow; // Lấy tham chiếu đến Playerbow nếu có
    void Start()
    {
        playerBow = GetComponent<PlayerBow>();
    }
    public override void Attack(Transform target) // Được gọi mỗi frame khi player giữ nút tấn công
    {
        if(timeToNextFire<0 && isCharging == false) // Kiểm tra xem có đạng nạp và tới thời gian chưa để bắn
        {
            isCharging = true;
            currentArrow = BulletPool.instance.GetBullet(weaponData.bulletPrefab,spawnBulletPos.position,transform.rotation).GetComponent<Arrow>();
            currentArrow.transform.SetParent(transform.GetChild(0).transform);
            animator.SetTrigger(Parameters.charge);
            timeToNextLevelCharge = timeForEachLevelCharge;
            if(playerBow) playerBow.OnAttack();
        }
        if(isCharging) // Kiểm tra trạng thái đang nạp
        {
            timeToNextLevelCharge -= Time.deltaTime;
            if(playerBow) playerBow.OnCharging(timeToNextLevelCharge,timeForEachLevelCharge);
            if(timeToNextLevelCharge<0 && chargeCount < 4)
            {
                chargeCount++;
                if(playerBow) playerBow.currentSquare = ChargingBar.instance.GetSquare(chargeCount);
                timeToNextLevelCharge = timeForEachLevelCharge;
            }
        }
    }
    public override void StopAttack() // Bắt đầu thả cung
    {
        if(!isCharging) return;
        isCharging = false;
        if(!playerBow) chargeCount =0; // Nếu được dùng bởi Enemy
        currentArrow.SetBullet(weaponData.speed,weaponData.damage + chargeCount,weaponData.critChance,weaponData.element,weaponData.bulletBuffs,3);
        currentArrow.transform.SetParent(null); // Tách arrow ra khỏi cung
        currentArrow.bulletCollider.enabled = true; // bật collider để xác định va chạm
        currentArrow = null;
        chargeCount = 0;
        if(playerBow) playerBow.ResetAllSquare();
        animator.SetTrigger(Parameters.endAttack);
        timeToNextFire = 1/weaponData.fireRate;
        transform.localRotation = Quaternion.identity;
    }
    public override void RotateToTarget(Transform target)
    {
        if(!isCharging) return; 
        base.RotateToTarget(target);
    }
    public override void ResetToOringin()
    {
        if(currentArrow) 
        {
            BulletPool.instance.ReturnBullet(currentArrow.gameObject);
            currentArrow = null;
        }
        isCharging = false;
        timeToNextLevelCharge= timeForEachLevelCharge;
        chargeCount =0 ;
        base.ResetToOringin();
    }
}