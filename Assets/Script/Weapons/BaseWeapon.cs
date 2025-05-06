using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
public class BaseWeapon : MonoBehaviour
{
    [InlineEditor]
    public WeaponData weaponData;
    [SerializeField] protected Transform spawnBulletPos;
    [HideInEditorMode] public float timeToNextFire;
    [HideInEditorMode] public int level;
    protected List<BulletBuff> addedBuff = new();
    public event System.Action<List<BulletBuff>> OnAttack;
    public virtual void Attack(Transform target)
    {
        if(timeToNextFire <= 0)
        {
            NotifyAttack();
            CreateBullet(target); // Tạo đạn
            addedBuff = new(); // Làm mới buff được thêm sau khi bắn
            spawnBulletPos.gameObject.SetActive(true);
            DG.Tweening.DOVirtual.DelayedCall(0.05f,() => spawnBulletPos.gameObject.SetActive(false));
            timeToNextFire = 1/weaponData.FireRate(level);
        }
    }
    public virtual void StopAttack() {}
    public virtual void RotateToTarget(Transform target) // quay vũ khí vào target
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
    public virtual BaseBullet CreateBullet(Transform target)
    {
        List <BulletBuff> finalBuffs = new(weaponData.bulletBuffs);
        if(addedBuff != null) finalBuffs.AddRange( addedBuff) ;
        return BulletPool.Instance
            .GetBullet<BaseBullet>(weaponData.bulletPrefab
                ,spawnBulletPos.position // truyền vị trí spawn cho pool
                ,transform.rotation * Quaternion.Euler(0,0,Random.Range(-weaponData.inaccuracy,weaponData.inaccuracy)))
            .SetBullet(weaponData.speed // Set các giá trị
                ,weaponData.Damage(level)
                ,weaponData.CritChance(level)
                ,weaponData.element
                ,finalBuffs ,weaponData.bulletTimeLife);
            // Tạo đạn
    }
    public void Upgrade()
    {
        level+=1;
    }
    protected void NotifyAttack()
    {
        OnAttack?.Invoke(addedBuff);
    }
}