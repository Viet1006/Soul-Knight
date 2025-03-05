using System.Collections.Generic;
using UnityEngine;
// Lớp cơ sở các loại đạn
public class BaseBullet : MonoBehaviour
{
    public BulletData bulletData;
    [SerializeField] protected Animator animator;
    [SerializeField] ParticleSystem explodeEffect;
    [HideInInspector] public List<BaseBulletBuff> bulletBuffs;
    [HideInInspector] public float damage;
    Collider2D colliderBullet;
    bool isExplode;
    void Awake() // Nhận tất cả các buff đã ném vào trong viên đạn
    {
        bulletBuffs.AddRange(GetComponents<BaseBulletBuff>());
        colliderBullet = GetComponent<Collider2D>();
    }
    void Update()
    {
        if(!isExplode)
        {
            transform.position += Time.deltaTime * bulletData.speed * transform.right;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!IsHaveIBuffTriggered()) // Nếu có loại buff nào ko kích hoạt khi va chạm như xuyên qua enemy hoặc bật tường thì để loại buff đấy tự gọi khi destroy đạn
        {
            HandleCollision();
            HandleOnObject(collider);
        }
    }
    protected void Destroy()
    {
        gameObject.SetActive(false);
    }
    public virtual void HandleCollision() // hàm xử lý va chạm cơ bản
    {
        isExplode = true;
        animator.SetTrigger("Explode");
        colliderBullet.enabled = false;
        if(explodeEffect != null) explodeEffect.Play();
    }
    public void HandleOnObject(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent<IGetHit>(out var hittedObject)) hittedObject.GetHit(damage,bulletData.colorDamage);
        foreach (BaseBulletBuff buff in bulletBuffs) // Dùng tất cả các loại buff
        {
            buff.ApplyBuff(collider);
        }
    }
    bool IsHaveIBuffTriggered() // Kiểm tra có buff nào thuộc dạng tự trigger không
    {
        foreach(BaseBulletBuff buff in bulletBuffs)
        {
            if(buff.GetComponent<IBuffTriggeredBullet>() != null)
            {
                return true;
            }
        }
        return false;
    }
}