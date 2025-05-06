
using UnityEngine;
[System.Serializable]
public class PoisonBuff : BulletBuff
{
    [SerializeField] int damageHalfSecond;
    [SerializeField] float poisonDuration;
    public PoisonBuff(){}// Phải có constructor mặc định để unity có thể tạo đối tượng
    public PoisonBuff(int damageHalfSecond,float poisonDuration,int applyChance) : base(applyChance)
    {
        this.damageHalfSecond = damageHalfSecond;
        this.poisonDuration = poisonDuration;
    }
    protected override void HandleOnObject(Collider2D collider, Vector2 bulletPos)
    {
        if(collider.TryGetComponent(out ICanPoison iCanPoison)) iCanPoison.StartPoison(damageHalfSecond,poisonDuration);
    }
}
