using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.LowLevelPhysics;
public class BaseBullet : MonoBehaviour 
{
    protected BulletElement element;
    protected float speed;
    protected int damage;
    protected int critChance;
    protected Tween lifeTimer ; // Đếm thời gian để hủy đạn
    protected List<BulletBuff> bulletBuffs;
    [SerializeField] protected GameObject explodeEffect;
    TrailRenderer trail;
    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }
    public virtual BaseBullet SetBullet(float speed, int damage,int critChance,BulletElement element ,List<BulletBuff> bulletBuffs,float timeLife = 0)
    {
        if(trail) trail.Clear();
        this.speed = speed;
        this.critChance = critChance;
        this.damage = damage;
        if(timeLife >0) StartLifeTimer(timeLife);
        this.bulletBuffs = bulletBuffs;
        this.element = element;
        return this;
    }
    public void StartLifeTimer(float timeLife) // Bắt đầu đếm để trả đạn về pool
    {
        lifeTimer.Kill();
        lifeTimer = DOVirtual.DelayedCall(timeLife , ReturnToPool  ,false);
    }
    public virtual void HandleCollision(Collider2D collider)
    {
        if(bulletBuffs == null) return;
        foreach(BulletBuff bulletBuff in bulletBuffs)
        {
            bulletBuff?.TryHandleCollision(collider , transform.position);
        }
    }
    public virtual void HandleOnObject(Collider2D collider) //Gây sát thương cho object
    {
        // Apply buff trước vì nếu chết thì gọi gethit sau và trong gethit có hủy các icon mà buff sinh ra 
        // Nếu gọi sau lúc chết sẽ hủy icon effect mà sau đó apply sẽ bị hiện lại
        ApplyBuffOnObject(collider);
        if (collider.TryGetComponent(out IGetHit hittedObject))
        {
            if(RandomChance.RollChance(critChance))
            {
                hittedObject.GetHit(damage * 2  ,element ,isCrit: true);
            }
            else hittedObject.GetHit(damage,element ,isCrit:  false);
        }
    }
    public void ApplyBuffOnObject(Collider2D collider)
    {
        if(bulletBuffs != null)
        {
            foreach(BulletBuff bulletBuff in bulletBuffs)
            {
                bulletBuff?.TryHandleOnObject(collider , transform.position);
            }
        }        
    }
    public virtual void ReturnToPool()
    {
        lifeTimer.Kill();
        BulletPool.Instance.ReturnBullet(this);
    }
}