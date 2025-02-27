using System.Collections.Generic;
using UnityEngine;
// Lớp cơ sở các loại đạn
public class BaseBullet : MonoBehaviour
{
    public BulletData bulletData;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected Animator animator;
    [SerializeField] ParticleSystem explodeEffect;
    [HideInInspector] public List<BaseBulletBuff> bulletBuffs;
    public float damage;
    bool isExplode;
    void Awake() // Nhận tất cả các buff đã ném vào trong viên đạn
    {
        bulletBuffs.AddRange(GetComponents<BaseBulletBuff>());
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
            HandleCollision(collider);
        }
    }
    protected void Destroy()
    {
        gameObject.SetActive(false);
    }
    public virtual void HandleCollision(Collider2D collider) // hàm xử lý va chạm cơ bản
    {
        isExplode = true;
        animator.SetTrigger("Explode");
        explodeEffect.Play();
        BaseEnemy hittedEnemy = collider.gameObject.GetComponent<BaseEnemy>();
        if(hittedEnemy)
        {
            hittedEnemy.GetHit(damage,bulletData.colorDamage);
        }
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