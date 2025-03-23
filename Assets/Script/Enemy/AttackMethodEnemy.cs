using UnityEngine;

public abstract class AttackMethodEnemy : MonoBehaviour
{
    protected GameObject nearestPlayer;
    protected Transform target; // Giữ tạm vị trí để phòng khi đang tấn công thì ko thấy player và lỗi khi xuay về phía player
    protected bool isAttacking;
    float timeToNextAttack;
    protected EnemyBrain enemyBrain;
    protected virtual void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        ResetTimeToAttack();// Gọi luôn tránh được bắn trước khi hàm init kích hoạt 
    }
    protected virtual void Update()
    {
        nearestPlayer = FindTarget.GetNearestObject(transform.position,enemyBrain.enemyData.attackRange,LayerMask.GetMask("Player"));
        if(nearestPlayer) target = nearestPlayer.transform; 
        else if (!isAttacking) target = null; // Khi player ra khỏi vùng tấn công và ko đang tấn công thì mới set target = null
        if(!isAttacking) // Nếu ko đang tấn công và có player thì count down thời gian tấn công
        {
            timeToNextAttack-=Time.deltaTime;
            if(timeToNextAttack<=0  && nearestPlayer) // Khi count down hoàn thành thì tấn công
            {
                isAttacking = true;
                StartAttack();
            }
        }
        FlipToTarget();
    }
    public void FlipToTarget()
    {
        if (!target) return; // Nếu target đang null thì dừng
        if (target.position.x > transform.position.x) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (target.position.x < transform.position.x) transform.rotation = Quaternion.Euler(0, -180, 0);
    }
    protected abstract void StartAttack();
    public void ResetTimeToAttack() // Nên được gọi khi Enemy xong đợt tấn công để bắt đầu đếm ngược đợt tấn công mới
    {
        timeToNextAttack = 1/enemyBrain.enemyData.AttackRate;
        isAttacking = false;
    }
}
