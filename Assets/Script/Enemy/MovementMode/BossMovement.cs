using UnityEngine;

public class BossMovement : MoveToStatus
{
    [SerializeField] SpawnArea moveArea;
    [SerializeField] Transform moveTarget;
    protected override void Awake()
    {
        base.Awake();
        destinationSetter.target = moveTarget;
        moveTarget.SetParent(null);
        moveTarget.position = moveArea.GetRandomPosition();
    }
    public override void OnTargetReached()
    {
        Vector2 movePos = moveArea.GetRandomPosition();
        if(Physics2D.OverlapCircle(movePos , 1))
        {
            OnTargetReached();
            return;
        }
        moveTarget.position = movePos;
    }
}
