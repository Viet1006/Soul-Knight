using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeReference] BulletBuff bulletBuff;
    void Awake()
    {
        EnemyController enemyController = GetComponent<EnemyController>();
        enemyController.OnReset += () => bulletBuff.TryHandleCollision(null, transform.position);
    }
}
