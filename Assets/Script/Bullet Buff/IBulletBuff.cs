using UnityEngine;

public abstract class IBulletBuff
{
    [SerializeField] protected int appltChance;
    public abstract void HandleOnObject();
    public abstract void HandleCollision();
}
