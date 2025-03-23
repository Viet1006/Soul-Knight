using UnityEngine;

public class Bow : BaseWeapon
{
    [SerializeField] Animator animator;
    int chargeCount; // Đếm số lần nạp
    float timeToNextLevelCharge; // thời gian đến mức nạp tiếp theo 
    bool isCharging;
    public float timeForEachLevelCharge; // Thời gian để thành 1 mức nạp
    [HideInInspector] public GameObject chargingBar;
    SpriteRenderer currentSquare;
    BaseBullet currentBullet;
    void Start()
    {
        if(GetComponent<PlayerWeapon>()) chargingBar = GameObject.Find("ChargingBar");
        if(chargingBar != null) chargingBar.SetActive(false);
    }
    public override void Attack(Transform target) // Được gọi mỗi frame khi player giữ nút tấn công
    {
        if(timeToNextFire<0 && isCharging == false)
        {
            isCharging = true;
            currentBullet = BulletPool.instance.GetBullet(weaponData.bullet,spawnBulletPos.position,transform.rotation).GetComponent<BaseBullet>();
            currentBullet.transform.SetParent(transform.GetChild(0).transform);
            animator.SetTrigger(Parameters.charge);
            timeToNextLevelCharge = timeForEachLevelCharge;
            if(chargingBar) // Nếu được dùng bởi Player
            {
                currentSquare = chargingBar.transform.GetChild(0).GetComponent<SpriteRenderer>();
                chargingBar.SetActive(true);
            }
        }
        if(isCharging)
        {
            timeToNextLevelCharge -= Time.deltaTime;
            if(chargingBar) currentSquare.color = Color.Lerp(Color.black,Color.white,1-timeToNextLevelCharge/timeForEachLevelCharge); // Chỉnh màu Square
            if(timeToNextLevelCharge<0 && chargeCount < 4)
            {
                chargeCount++;
                if(chargingBar)currentSquare = chargingBar.transform.GetChild(chargeCount).GetComponent<SpriteRenderer>();
                timeToNextLevelCharge = timeForEachLevelCharge;
            }
        }
    }
    public override void StopAttack() // Bắt đầu thả cung
    {
        if(!isCharging) return;
        isCharging = false;
        if(!chargingBar) chargeCount =0; // Nếu được dùng bởi Enemy
        currentBullet.SetBullet(weaponData.bulletSpeed,weaponData.damage + chargeCount,RandomChance.TryCrit(weaponData.critChance+10*chargeCount),weaponData.elements,3);
        currentBullet.transform.SetParent(null); // Tách arrow ra khỏi cung
        currentBullet.StartCounter(3);
        currentBullet.bulletCollider.enabled = true;
        currentBullet = null;
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
    public override void RotateToTarget(Transform target)
    {
        if(!isCharging) return; 
        base.RotateToTarget(target);
    }
    public override void ResetToOringin()
    {
        if(currentBullet) 
        {
            Destroy(currentBullet.gameObject);
            currentBullet = null;
        }
        isCharging = false;
        timeToNextLevelCharge= timeForEachLevelCharge;
        chargeCount =0 ;
        base.ResetToOringin();
    }
}