using UnityEngine;
public class MeleeEnemyMove : MoveToStatus
{
    float findRange = 10f;
    GameObject nearestPlayer;
    void Update()
    {
        nearestPlayer = FindTarget.GetNearestObject(transform.position,findRange,LayerMask.GetMask("Player"));
        if(nearestPlayer) destinationSetter.target = nearestPlayer.transform;
        else destinationSetter.target = status;
    }
}