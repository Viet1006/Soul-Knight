using UnityEngine;

public abstract class AttackMethodEnemy : MonoBehaviour
{
    [SerializeField] float attackRange = 10;
    protected GameObject nearestPlayer;
    protected Transform target; // Giữ tạm vị trí để phòng khi đang tấn công thì ko thấy player và lỗi khi xuay về phía player
    protected bool isAttacking;
    float timeToNextAttack;
    protected EnemyData enemyData;
    int blockCount;
    protected virtual void Awake()
    {
        enemyData = GetComponent<EnemyController>().enemyData;
        timeToNextAttack = timeToNextAttack = 1/enemyData.AttackRate;
    }
    protected virtual void Update()
    {
        FlipToTarget();
        if(!nearestPlayer || Vector2.Distance(transform.position, nearestPlayer.transform.position) < attackRange) // Nếu chưa tìm thấy player
        {
            nearestPlayer = FindTarget.GetNearestObject(transform.position,attackRange,LayerMask.GetMask("Player"));
        }
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
    public virtual void FlipToTarget()
    {
        if (!target) return; // Nếu target đang null thì dừng
        if (target.position.x > transform.position.x) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (target.position.x < transform.position.x) transform.rotation = Quaternion.Euler(0, -180, 0);
    }
    protected abstract void StartAttack();
    public virtual void CreateBullet(){}
    public void ResetTimeToAttack() // Nên được gọi khi Enemy xong đợt tấn công để bắt đầu đếm ngược đợt tấn công mới
    {
        timeToNextAttack = 1/enemyData.AttackRate;
        isAttacking = false;
    }
    public virtual void StopAttack()
    {
        blockCount +=1 ;
        // Các corutine bị dừng giữa chừng vẫn sẽ lưu các biến trạng thái và sẽ chạy tiếp khi gọi lại StartAttack
        enabled = false; // Dừng đếm thời gian và dừng quay sang player
        timeToNextAttack = timeToNextAttack = 1/enemyData.AttackRate; // Đặt lại thời gian tấn công
    }
    public virtual void ContinueAttack()
    {
        blockCount -= 1;
        if(blockCount == 0)
        {
            enabled = true; // bật lại bộ đếm thời gian
            isAttacking = false; // trường hợp đang tấn công mà bị dừng Coroutine thì phải tắt để có thể bắt đầu tấn công lại
        }
    }
    protected virtual void OnDisable() // Khi enemy trả về pool sẽ gọi hàm này
    {
        blockCount = 0;
        enabled = true;
        isAttacking = false; 
        timeToNextAttack = timeToNextAttack = 1/enemyData.AttackRate; // Đặt lại thời gian tấn công
        target= null;
        nearestPlayer= null;
    }
}