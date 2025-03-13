using Pathfinding;
using UnityEngine;
[RequireComponent(typeof(AIDestinationSetter),typeof(AIPath))]
public class MoveToStatus : MonoBehaviour
{
    AIDestinationSetter destinationSetter;
    AIPath aiPath;
    Transform status;
    [SerializeField] EnemyData enemyData;
    [SerializeField] Animator animator;
    void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();
        status = GameObject.Find("Status").transform;
        destinationSetter.target = status;
        aiPath.maxSpeed = enemyData.speed;
        animator.SetInteger(Parameters.state,StateEnum.RUN);
        
    }
}
