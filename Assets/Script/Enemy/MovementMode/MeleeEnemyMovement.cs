using UnityEngine;
public class MeleeEnemyMove : MoveToStatus
{
    readonly float findRange = 10f;
    GameObject nearestPlayer;
    protected override void Update()
    {
        base.Update();
        nearestPlayer = FindTarget.GetNearestObject(transform.position,findRange,LayerMask.GetMask("Player"));
        if(nearestPlayer) destinationSetter.target = nearestPlayer.transform;
        else destinationSetter.target = Status.instance.transform;;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        nearestPlayer = null;
    }
}