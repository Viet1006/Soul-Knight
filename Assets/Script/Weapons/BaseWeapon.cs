using System.Collections;
using UnityEngine;
public class BaseWeapon : MonoBehaviour
{
    public WeaponData weaponData;
    [SerializeField] protected Transform spawnBulletPos;
    public float timeToNextFire;
    public virtual void Attack(Transform target)
    {
        if(timeToNextFire <= 0)
        {
            Quaternion quaternion = transform.rotation * Quaternion.Euler(0,0,Random.Range(-weaponData.inaccuracy,weaponData.inaccuracy));
            BaseBullet baseBullet = BulletPool.instance.GetBullet(weaponData.bullet,spawnBulletPos.position,quaternion).GetComponent<BaseBullet>();
            baseBullet.SetBullet(weaponData.bulletSpeed,weaponData.damage,RandomChance.TryCrit(weaponData.critChance),weaponData.elements,3);
            spawnBulletPos.gameObject.SetActive(true);
            StartCoroutine(DisableFireEffect());
            timeToNextFire = 1/weaponData.fireRate;
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
    protected IEnumerator DisableFireEffect()
    {
        yield return new WaitForSeconds(0.05f);
        spawnBulletPos.gameObject.SetActive(false);
    }
    protected virtual void Update()
    {
        timeToNextFire -= Time.deltaTime;
    }
    public virtual void ResetToOringin() // Đặt lại trạng thái ban đầu cho vũ khí
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        timeToNextFire = 1/weaponData.fireRate;
    }
}
