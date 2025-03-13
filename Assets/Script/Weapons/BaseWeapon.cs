
using System.Collections;
using UnityEngine;
public class BaseWeapon : MonoBehaviour , ICanSelect
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected Transform spawnBulletPos;
    [SerializeField] protected Collider2D weaponCollider;
    protected float timeToNextFire;
    [SerializeField] GameObject SelectedItemName;
    [SerializeField] SpriteRenderer sprite;
    public virtual void Attack(Transform target)
    {
        if(timeToNextFire<0)
        {
            Quaternion quaternion = transform.rotation * Quaternion.Euler(0,0,Random.Range(-weaponData.inaccuracy,weaponData.inaccuracy));
            Instantiate(weaponData.bullet,spawnBulletPos.position,quaternion).GetComponent<BaseBullet>().SetBullet(weaponData.damage,weaponData.bulletSpeed);
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
    public void ShowSelectObject() // hiện tên vũ khí
    {
         SelectedItemName.SetActive(true);
    }
    public void HideSelectObject() // Ẩn tên vũ khí
    {
        SelectedItemName.SetActive(false);
    }
    protected IEnumerator DisableFireEffect()
    {
        yield return new WaitForSeconds(0.05f);
        spawnBulletPos.gameObject.SetActive(false);
    }
    public virtual void PickUp(Transform parent) // Nhặt vũ khí
    {
        transform.SetParent(parent);
        weaponCollider.enabled = false;
        transform.localPosition = Vector2.zero;
        GetWeapon();
    }
    public void Drop() // Thả vũ khí
    {
        transform.parent = null;
        weaponCollider.enabled= true;
        transform.rotation = Quaternion.identity;
    }
    public void GetWeapon() // lấy vũ khí
    {
        sprite.sortingOrder = 4;
    }
    public void PutAwayWeapon() // Cất vũ khí
    {
        sprite.sortingOrder = 2;
        transform.localRotation = Quaternion.Euler(0,180,-20);
    } 
    protected void Awake()
    {
        if(SelectedItemName != null)
        {
            TextMesh textMesh = SelectedItemName.GetComponent<TextMesh>();
            textMesh.text = name;
            textMesh.color = SetColor.SetColorRare(weaponData.rareColor);
        }
    }
    protected virtual void Update()
    {
        timeToNextFire -= Time.deltaTime;
    }
}
