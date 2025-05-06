
using UnityEngine;
[System.Serializable]
public class BurnBuff : BulletBuff
{
    [SerializeField] int damageHalfSecond;
    [SerializeField] float burnDuration;
    public BurnBuff(){ } // Phải có constructor mặc định để unity có thể tạo đối tượng
    public BurnBuff(int damageHalfSecond , float burnDuration , int applyChance = 100) : base(applyChance)
    {
        this.damageHalfSecond = damageHalfSecond;
        this.burnDuration = burnDuration;
    }
    protected override void HandleOnObject(Collider2D collider , Vector2 bulletPos)
    {
        if(collider.TryGetComponent(out ICanBurn iCanBurn)) iCanBurn.StartBurn(damageHalfSecond,burnDuration);
    }
}
