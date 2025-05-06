using UnityEngine;
[System.Serializable]
public class PushBackBuff : BulletBuff
{
    [SerializeField] float forcePush;
    public PushBackBuff(){}// Phải có constructor mặc định để unity có thể tạo đối tượng
    public PushBackBuff(float forcePush,int applyChance) : base(applyChance)
    {
        this.forcePush = forcePush;
    }
    protected override void HandleOnObject(Collider2D collider, Vector2 bulletPos)
    {
        if(collider.TryGetComponent(out IPushable iPushable)) iPushable.StartPush(((Vector2)collider.transform.position - bulletPos).normalized,forcePush);
    }
}