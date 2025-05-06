using UnityEngine;
[System.Serializable]
public class FrozenBuff : BulletBuff
{
    [SerializeField] float forzenDuration;
    public FrozenBuff(){ } // Phải có constructor mặc định để unity có thể tạo đối tượng
    public FrozenBuff(float forzenDuration,int applyChance) : base(applyChance)
    {
        this.forzenDuration = forzenDuration;
    }
    protected override void HandleOnObject(Collider2D collider, Vector2 bulletPos)
    {
        if(collider && collider.TryGetComponent( out ICanFrozen iCanFrozen)) iCanFrozen.StartFrozen(forzenDuration);
    }
}
