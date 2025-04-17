using UnityEngine;

public abstract class AttackMethodEnemy : MonoBehaviour
{
    protected GameObject nearestPlayer;
    protected Transform target; // Giữ tạm vị trí để phòng khi đang tấn công thì ko thấy player và lỗi khi xuay về phía player
    protected bool isAttacking;
    float timeToNextAttack;
    protected EnemyController enemyController;
    protected virtual void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    protected virtual void Update()
    {
        FlipToTarget();
        nearestPlayer = FindTarget.GetNearestObject(transform.position,enemyController.enemyData.attackRange,LayerMask.GetMask("Player"));
        if(nearestPlayer) target = nearestPlayer.transform; 
        else if (!isAttacking) target = null; // Khi player ra khỏi vùng tấn công và ko đang tấn công thì mới set target = null
        if(!isAttacking && target) // Nếu ko đang tấn công và có player thì count down thời gian tấn công
        {
            timeToNextAttack-=Time.deltaTime;
            if(timeToNextAttack<=0  && nearestPlayer) // Khi count down hoàn thành thì tấn công
            {
                isAttacking = true;
                StartAttack();
            }
        }
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
        timeToNextAttack = 1/enemyController.enemyData.AttackRate;
        isAttacking = false;
    }
    public virtual void OnInit()
    {
        timeToNextAttack = timeToNextAttack = 1/enemyController.enemyData.AttackRate;
    }
    public virtual void StopAttack()
    {
        StopAllCoroutines(); // Dừng tất cả Coroutine
        // Các corutine bị dừng giữa chừng vẫn sẽ lưu các biến trạng thái và sẽ chạy tiếp khi gọi lại StartAttack
        enabled = false; // Dừng đếm thời gian và dừng quay sang player
    }
    public virtual void ContinueAttack()
    {
        enabled = true; // bật lại bộ đếm thời gian
        isAttacking = false; // trường hợp đang tấn công mà bị dừng Coroutine thì phải tắt để có thể bắt đầu tấn công lại
    }
}
