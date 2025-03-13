using Unity.Mathematics;
using UnityEngine;

public class Bow : BaseWeapon
{
    [SerializeField] Animator animator;
    int chargeCount; // Đếm số lần nạp
    float timeToNextLevelCharge; // thời gian đến mức nạp tiếp theo 
    bool isCharging;
    public float timeForEachLevelCharge; // Thời gian để thành 1 mức nạp
    BaseBullet currentBullet;
    [SerializeField] GameObject chargingBar;
    SpriteRenderer currentSquare;
    void Start()
    {
        if(chargingBar != null)
        {
            chargingBar.transform.SetParent(null);   
            chargingBar.GetComponent<ChargingBar>().target = transform;
        }
    }
    public override void Attack(Transform target) // Được gọi mỗi frame khi player giữ nút tấn công
    {
        if(timeToNextFire<0 && isCharging == false)
        {
            isCharging = true;
            currentBullet = Instantiate(weaponData.bullet,spawnBulletPos.position,transform.rotation).GetComponent<BaseBullet>();
            currentBullet.transform.SetParent(transform.GetChild(0).transform);
            animator.SetTrigger(Parameters.charge);
            timeToNextLevelCharge = timeForEachLevelCharge;
            if(chargingBar)
            {
                currentSquare = chargingBar.transform.GetChild(0).GetComponent<SpriteRenderer>();
                chargingBar.SetActive(true);
            }
        }
        if(isCharging)
        {
            timeToNextLevelCharge -= Time.deltaTime;
            if(chargingBar) currentSquare.color = Color.Lerp(Color.black,Color.white,1-timeToNextLevelCharge/timeForEachLevelCharge);
            if(timeToNextLevelCharge<0 && chargeCount < 4)
            {
                chargeCount++;
                if(chargingBar)currentSquare=chargingBar.transform.GetChild(chargeCount).GetComponent<SpriteRenderer>();
                timeToNextLevelCharge = timeForEachLevelCharge;
            }
        }
    }
    public override void StopAttack() // Bắt đầu thả cung
    {
        if(!isCharging) return;
        isCharging = false;
        currentBullet.SetBullet(weaponData.damage + chargeCount,weaponData.bulletSpeed);
        currentBullet.transform.SetParent(null);
        currentBullet.colliderBullet.enabled = true;
        chargeCount = 0;
        if(chargingBar != null) 
        {
            for (int i = 0 ; i< 5 ;i++)
            {
                chargingBar.transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.black;
                chargingBar.SetActive(false);
            }
        }
        animator.SetTrigger(Parameters.endAttack);
        timeToNextFire = 1/weaponData.fireRate;
        transform.localRotation = Quaternion.identity;
    }
    public override void PickUp(Transform parent)
    {
        base.PickUp(parent);
        animator.enabled = true;
        transform.localRotation = Quaternion.identity;
    }
    public override void RotateToTarget(Transform target)
    {
        if(!isCharging) return; 
        base.RotateToTarget(target);
    }
}