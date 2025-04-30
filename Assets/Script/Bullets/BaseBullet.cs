using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class BaseBullet : MonoBehaviour
{
    protected BulletElement element;
    protected float speed;
    protected int damage;
    protected int critChance;
    protected Animator animator;
    [HideInInspector] public Collider2D bulletCollider;
    protected List<BulletBuff> bulletBuffs;
    protected ParticleSystem explodeEffect;
    protected Tween lifeTimer ; // Đếm thời gian để hủy đạn
    void Awake()
    {
        bulletCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        explodeEffect = GetComponentInChildren<ParticleSystem>();
    }
    public virtual void SetBullet(float speed, int damage,int critChance,BulletElement element ,List<BulletBuff> bulletBuffs,float timeLife = 0)
    {
        this.speed = speed;
        this.critChance = critChance;
        this.damage = damage;
        if(timeLife >0) StartLifeTimer(timeLife);
        this.bulletBuffs = bulletBuffs;
        this.element = element;
    }
    public void StartLifeTimer(float timeLife) // Bắt đầu đếm để trả đạn về pool
    {
        lifeTimer.Kill();
        lifeTimer = DOVirtual.DelayedCall(timeLife , ReturnToPool);
    }
    public virtual void HandleCollisionEffect(Collider2D collider){}
    public virtual void HandleOnObject(Collider2D collider) //Gây sát thương cho object
    {
        // Apply buff trước vì nếu chết thì gọi gethit sau và trong gethit có hủy các icon mà buff sinh ra 
        // Nếu gọi sau lúc chết sẽ hủy icon effect mà sau đó apply sẽ bị hiện lại
        foreach(BulletBuff bulletBuff in bulletBuffs) bulletBuff.ApplyBuff(collider , transform.position);
        if (collider.TryGetComponent(out IGetHit hittedObject)) hittedObject.GetHit(damage,element);
    }
    public virtual void ReturnToPool()
    {
        lifeTimer.Kill();
        if(bulletCollider) bulletCollider.enabled = true;
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}