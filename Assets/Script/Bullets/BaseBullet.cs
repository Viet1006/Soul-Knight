using UnityEngine;
public class BaseBullet : MonoBehaviour
{
    [HideInInspector] public BulletElements bulletElement;
    protected float speed;
    protected float damage;
    public float critMultiplier = 1; // Hệ số nhân
    [SerializeField] ParticleSystem explodeEffect;
    [SerializeField] protected Animator animator;
    [SerializeField] public Collider2D bulletCollider;
    BulletBuff bulletBuff;
    bool isCritical;
    public void SetBullet(float speed , float damage , bool isCritical , BulletElements elements,float timeLife)
    {
        this.speed = speed;
        if(isCritical) this.damage = damage* critMultiplier;
        else this.damage = damage;
        this.isCritical = isCritical;
        bulletBuff = GetComponent<BulletBuff>();
        bulletElement = elements;
        if(timeLife >0) StartCounter(timeLife);
    }
    protected virtual void Update()
    {
        transform.position += speed * Time.deltaTime *transform.right;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        HandleCollisionEffect(collider);
        if(collider.CompareTag("Wall")) return;
        HandleOnObject(collider);
    }
    public virtual void HandleOnObject(Collider2D collider) //Gây sát thương cho object
    {
        if (collider.TryGetComponent(out IGetHit hittedObject)) hittedObject.GetHit(damage,SetColor.SetElementColor(bulletElement));
        if(bulletBuff) bulletBuff.ApplyPassiveBuff(collider,bulletElement,isCritical);
    }
    public virtual void HandleCollisionEffect(Collider2D collider) // Xử lý hiệu ứng va chạm
    {
        speed = 0;
        transform.SetParent(null);
        animator.SetTrigger(Parameters.explode);
        bulletCollider.enabled = false;
        if(explodeEffect) explodeEffect.Play();
    }
    public void StartCounter(float timeLife) // Bắt đầu đếm để trả đạn về pool
    {
        Invoke(nameof(ReturnToPool),timeLife);
    }
    public virtual void ReturnToPool()
    {
        if(IsInvoking(nameof(ReturnToPool))) CancelInvoke(nameof(ReturnToPool)); // tắt các invoke thực hiện hàm này
        if(bulletCollider) bulletCollider.enabled = true;
        BulletPool.instance.ReturnBullet(gameObject);
    }
}