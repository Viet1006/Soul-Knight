using UnityEngine;

public class SkeletonEffect : HandleEffectOnEnemy
{
    public override void StartPush(Vector2 direction, float distance)
    {
    }
    public override void StartFrozen(float frozenTime)
    {
        base.StartFrozen(frozenTime/5);
    }
    public override void StartStun(float stunTime)
    {
        base.StartStun(stunTime/5);
    }
}
