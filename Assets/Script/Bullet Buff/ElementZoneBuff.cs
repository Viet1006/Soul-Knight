using UnityEngine;
[System.Serializable]
public class ElementZoneBuff : BulletBuff
{
    [SerializeField] int damageHalfSecond;
    [SerializeField] float timeLife;
    [SerializeField] BulletElement bulletElement;
    [SerializeField] LayerMask hitLayer;
    [SerializeField] bool isEnemy;
    public ElementZoneBuff(){ } // Phải có constructor mặc định để unity có thể tạo đối tượng
    public ElementZoneBuff(int damageHalfSecond , float timeLife ,BulletElement bulletElement , LayerMask hitLayer, int applyChance = 100) 
        : base(applyChance)
    {
        this.damageHalfSecond = damageHalfSecond;
        this.timeLife = timeLife;
        this.bulletElement = bulletElement;
        this.hitLayer = hitLayer;
    }
    protected override void HandleCollision(Collider2D collider , Vector2 bulletPos)
    {
        BulletPool.Instance.GetBullet<ElementZone>(ObjectHolder.Instance.elementZone,bulletPos)
            .SetElementZone(damageHalfSecond,timeLife ,bulletElement , hitLayer,isEnemy);

    }
}
