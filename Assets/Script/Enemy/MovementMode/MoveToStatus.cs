using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(AIDestinationSetter),typeof(AIPath))]
public class MoveToStatus : MonoBehaviour
{
    protected AIDestinationSetter destinationSetter;
    protected Transform status;
    Animator animator;
    EnemyBrain enemyBrain;
    void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        status = GameObject.Find("Status").transform;
        destinationSetter = GetComponent<AIDestinationSetter>();
        animator = GetComponent<Animator>();
        destinationSetter.target = status;
        if(animator)
        {
            
            animator.runtimeAnimatorController = enemyBrain.enemyData.animatorController;
            animator.SetInteger(Parameters.state,StateEnum.RUN);
        }
    }
}
