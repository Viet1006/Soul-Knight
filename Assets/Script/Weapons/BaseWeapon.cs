using UnityEngine;
using Sirenix.OdinInspector;
public class BaseWeapon : MonoBehaviour
{
    [InlineEditor]
    public WeaponData weaponData;
    [SerializeField] protected Transform spawnBulletPos;
    [HideInEditorMode] public float timeToNextFire;
    [HideInEditorMode] public int level;
    public virtual void Attack(Transform target)
    {
        if(timeToNextFire <= 0)
        {
            CreateBullet(target);
            spawnBulletPos.gameObject.SetActive(true);
            DG.Tweening.DOVirtual.DelayedCall(0.05f,() => spawnBulletPos.gameObject.SetActive(false));
            timeToNextFire = 1/weaponData.FireRate(level);
        }
    }
    public virtual void StopAttack() {}
    public virtual void RotateToTarget(Transform target) // quay vũ khí vào target, clientId là Id người quay
    {
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        // trả về góc từ 0 -> 90 với vector thuộc góc phần tư 1,3 và từ -90 -> 0 với góc phần tư 2,4
        if(direction != Vector2.zero)
        {
            float angle = Mathf.Atan(direction.y/direction.x) * Mathf.Rad2Deg;
            if(transform.parent.rotation.y == 0 || direction.x == 0) // Khi quay sang bên trái thì angle khi x=0 vẫn được coi là dương khi tính góc nên ngược dấu => ko cần đổi dấu
            {
                transform.localRotation = Quaternion.Euler(0,0,angle);
            }else{
                transform.localRotation = Quaternion.Euler(0,0,-angle);
            }
        }
    }
    protected virtual void Update()
    {
        timeToNextFire -= Time.deltaTime;
    }
    public virtual void ResetToOringin() // Đặt lại trạng thái ban đầu cho vũ khí
    {
        WeaponPool.instance.ReturnWeapon(gameObject);
    }
    public virtual void CreateBullet(Transform target)
    {
        BulletPool.instance
                .GetBullet(weaponData.bulletPrefab
                    ,spawnBulletPos.position // truyền vị trí spawn cho pool
                    ,transform.rotation * Quaternion.Euler(0,0,Random.Range(-weaponData.inaccuracy,weaponData.inaccuracy)))
                .GetComponent<BaseBullet>() // Lấy baseBUllet từ bullet vừa tạo 
                .SetBullet(weaponData.speed // Set các giá trị
                    ,weaponData.Damage(level)
                    ,weaponData.CritChance(level)
                    ,weaponData.element
                    ,weaponData.bulletBuffs,3);
                // Tạo đạn
    }
    public void Upgrade()
    {
        level+=1;
    }
}