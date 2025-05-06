using UnityEngine;
[System.Serializable]
public class StunBuff : BulletBuff
{
    [SerializeField] float stunDuration;
    public StunBuff(){ } // Phải có constructor mặc định để unity có thể tạo đối tượng
    public StunBuff(int stunDuration,int applyChance) : base(applyChance)
    {
        this.stunDuration = stunDuration;
    }
    protected override void HandleOnObject(Collider2D collider, Vector2 bulletPos)
    {
        if(collider && collider.TryGetComponent( out ICanStun iCanStun)) iCanStun.StartStun(stunDuration);
    }
}
