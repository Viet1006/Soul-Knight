using UnityEngine;

public class Boss10Effect : HandleEffectOnEnemy
{
    public override void StartPush(Vector2 direction, float distance)
    {
    }
    public override void StartFrozen(float frozenTime)
    {
        base.StartFrozen(frozenTime/2);
    }
    public override void StartStun(float stunTime)
    {
        base.StartStun(stunTime/2);
    }
}
