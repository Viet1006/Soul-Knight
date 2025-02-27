using UnityEngine;
public abstract class BaseBulletBuff : MonoBehaviour
{
    protected BaseBullet bullet;
    void Start()
    {
        bullet = GetComponent<BaseBullet>();
    }
    public abstract void ApplyBuff(Collider2D collider);
}
