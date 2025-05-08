using UnityEngine;
[System.Serializable] // Phải có để tránh bị mất khi run time
public abstract class BulletBuff
{
    [Range(0,100)]
    [SerializeField] int applyChance;
    protected BulletBuff() {}
    public BulletBuff(int applyChance)
    {
        this.applyChance = applyChance;
    }
    public void TryHandleOnObject(Collider2D collider , Vector2 bulletPos)
    {
        if( !RandomChance.RollChance(applyChance)) return; // Nếu apply ko được thì return
        HandleOnObject(collider , bulletPos);
    }
    public void TryHandleCollision(Collider2D collider , Vector2 bulletPos)
    {
        if( !RandomChance.RollChance(applyChance)) return; // Nếu apply ko được thì return
        HandleCollision(collider , bulletPos );
    }
    protected virtual void HandleOnObject(Collider2D collider , Vector2 bulletPos){}  // Xử lý trên object
    protected virtual void HandleCollision(Collider2D collider , Vector2 bulletPos){} // Xử lý khi va chạm
}
