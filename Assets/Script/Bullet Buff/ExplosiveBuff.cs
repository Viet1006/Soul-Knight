using UnityEngine;
[System.Serializable]
public class ExplosiveBuff : BulletBuff
{
    [SerializeField] int damage;
    [SerializeField] LayerMask hitLayer;
    readonly float radius = 2.5f;
    public ExplosiveBuff(){}// Phải có constructor mặc định để unity có thể tạo đối tượng
    public ExplosiveBuff(int damage,LayerMask hitLayer, int applyChance = 100) : base(applyChance)
    {
        this.damage = damage;
        this.hitLayer = hitLayer;
    }
    protected override void HandleCollision(Collider2D collider, Vector2 bulletPos)
    {
        Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(bulletPos,radius,hitLayer);
        foreach (Collider2D hittedObject in hittedObjects)
        {
            if(hittedObject.TryGetComponent(out IGetHit getHit)) getHit.GetHit(damage,BulletElement.Fire);
        }
        ExplodeEffectPool.Instance.GetExplodeEffect(ObjectHolder.Instance.explodeEffectPrefab,bulletPos , 10); // Tạo hiệu ứng nổ
        ShakeCamera.Instance.ShakeCam(bulletPos,1,1,0.5f); // Shake camera  
    }
}