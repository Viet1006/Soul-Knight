
using UnityEngine;

public class MiniExplode : BulletBuff
{
    [SerializeField] int damage;
    [SerializeField] LayerMask hitLayer;
    readonly bool isCrit = false;
    readonly float radius = 1;
    public MiniExplode(){}// Phải có constructor mặc định để unity có thể tạo đối tượng
    public MiniExplode(int damage,LayerMask hitLayer, int applyChance = 100 , bool isCrit = false) : base(applyChance)
    {
        this.damage = damage;
        this.hitLayer = hitLayer;
        this.isCrit = isCrit;
    }
    protected override void HandleCollision(Collider2D collider, Vector2 bulletPos)
    {
        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(bulletPos,radius,hitLayer);
        foreach (Collider2D hittedObject in hittedObjects)
        {
            if(hittedObject.TryGetComponent(out IGetHit getHit)) getHit.GetHit(damage,BulletElement.Fire,isCrit);
        }
        ExplodeEffectPool.Instance.GetExplodeEffect(ObjectHolder.Instance.miniExplode,bulletPos , 1); // Tạo hiệu ứng nổ
        ShakeCamera.Instance.ShakeCam(bulletPos,0.5f,1,0.5f); // Shake camera  
    }
}
