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
    public void SetElementZone(int damagePerHalfSeconder , float durationEffect , BulletElement element)
    {
        sprite.color = zones[element].colorZone;
        transform.localScale = Vector2.zero;
        transform.DOScale(2 * radius * Vector2.one  , 0.3f ) //  hình ảnh ban đầu bán kính chỉ có 0.5 nên cần * 2   
            .OnComplete( () =>
                {
                    zones[element].effect.gameObject.SetActive(true);
                    zones[element].effect.Stop();
                    zones[element].effect.Play();
                });
        DOVirtual.DelayedCall(0.5f , () => Burn(damagePerHalfSeconder , element))
            .SetLoops((int)(durationEffect * 2)) 
            .OnKill(() =>
                {
                    BulletPool.Instance.ReturnBullet(gameObject);
                    zones[element].effect.gameObject.SetActive(false);
                });
    }
    void Burn(int damage , BulletElement element)
    {
        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(transform.position,radius );
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