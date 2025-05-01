
using UnityEngine;
[System.Serializable]
public class BurnBuff : IBulletBuff
{
    [SerializeField] int damageHalfSecond;
    [SerializeField] int effectDuration;
    public BurnBuff() { }
    public BurnBuff(int damageHalfSecond , int effectDuration , int critChance = 100)
    {
        this.damageHalfSecond = damageHalfSecond;
        this.effectDuration = effectDuration;
    }
    public override void HandleOnObject()
    {
        Debug.Log("Â");
    }

    public override void HandleCollision()
    {
        Debug.Log("B");
    }
}
