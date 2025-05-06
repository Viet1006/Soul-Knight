using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class ElementZone : SerializedMonoBehaviour
{
    [System.Serializable]
    class Zone
    {
        public ParticleSystem effect;
        public Color colorZone;
    }
    [SerializeField] Dictionary<BulletElement,Zone> zones = new();
    readonly float radius = 2f;
    SpriteRenderer sprite;
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void SetElementZone(int damagePerHalfSeconder , float durationEffect , BulletElement element,LayerMask hitLayer)
    {
        if(!zones.ContainsKey(element)) // Nếu thuộc tính ko có thì ko set mà trả về pool luôn
        {
            BulletPool.Instance.ReturnBullet(this);
            return;
        } 
        sprite.color = zones[element].colorZone;
        transform.localScale = Vector2.zero;
        transform.DOScale(2 * radius * Vector2.one  , 0.3f ) //  hình ảnh ban đầu bán kính chỉ có 0.5 nên cần * 2   
            .OnComplete( () =>
                {
                    zones[element].effect.gameObject.SetActive(true);
                });
        DOVirtual.DelayedCall(0.5f , () => Burn(damagePerHalfSeconder , element , hitLayer))
            .SetLoops((int)(durationEffect * 2)) 
            .OnKill(() =>
                {
                    zones[element].effect.gameObject.SetActive(false);
                    BulletPool.Instance.ReturnBullet(this);
                });
    }
    void Burn(int damage , BulletElement element , LayerMask hitLayer)
    {
        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(transform.position,radius ,hitLayer);
        foreach (Collider2D hittedObject in hittedObjects)
        {
            if(hittedObject.TryGetComponent(out IGetHit getHit)) getHit.GetHit(damage,element,false); 
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}