using System.Collections.Generic;
using UnityEngine;
// Lớp cơ sở các loại đạn
public class BaseBullet : MonoBehaviour
{
    public Color colorDamage;
    [SerializeField] protected Animator animator;
    [SerializeField] ParticleSystem explodeEffect;
    [HideInInspector] public List<BaseBulletBuff> bulletBuffs;
    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    public float timeLife;
    public Collider2D colliderBullet;
    void Awake() // Nhận tất cả các buff đã ném vào trong viên đạn
    {
        bulletBuffs.AddRange(GetComponents<BaseBulletBuff>());
        Destroy(gameObject,timeLife);
    }
    void Update()
    {
        transform.position += Time.deltaTime * speed * transform.right;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("PlayerBullet") || collider.CompareTag("EnemyBullet")) return;
        if(!IsHaveIBuffTriggered()) // Nếu có loại buff nào ko kích hoạt khi va chạm như xuyên qua enemy hoặc bật tường thì để loại buff đấy tự gọi khi destroy đạn
        {
            HandleCollision(collider);
            HandleOnObject(collider);
        }
    }
    protected void Destroy()
    {
        gameObject.SetActive(false);
    }
    public virtual void HandleCollision(Collider2D collider) // hàm xử lý va chạm cơ bản
    {
        speed = 0;
        animator.SetTrigger(Parameters.explode);
        colliderBullet.enabled = false; // Tắt Collider để tránh Enemy đi vào trong quá trình xử lý hiệu ứng phát nổ
        if(explodeEffect != null) explodeEffect.Play();
        transform.SetParent(null);
    }
    public void HandleOnObject(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out IGetHit hittedObject)) hittedObject.GetHit(damage,colorDamage);
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
    public virtual void SetBullet(float damage, float speed)
    {
        this.damage = damage;
        this.speed = speed;
    }
    public virtual void StopAttack()
    {
    }
}