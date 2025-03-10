using System.Collections;
using UnityEngine;

public class HandleEffectOnEnemy : MonoBehaviour,ICanStun,IPushable
{
    [SerializeField] GameObject stunIcon;
    [SerializeField]BaseEnemy baseEnemy;
    Coroutine currentStunCoroutine; // Giữ Coroutine Stun để kiểm tra và dừng khi có coroutine stun khác muốn chạy
    Coroutine currentPushCoroutine;
    int holdMovementCount =0; // Số coroutine đang muốn sửa quyền di chuyển của enemy này
    public void StartPushCoroutine(Vector2 direction,float distance)
    {
        if(currentPushCoroutine != null)
        {
            StopCoroutine(currentPushCoroutine);
            holdMovementCount -= 1; // Trả nếu coroutine bị dừng giữa chừng
        }
        currentPushCoroutine = StartCoroutine(PushBackIEnum(direction,distance));
        holdMovementCount +=1;
    }
    public virtual IEnumerator PushBackIEnum(Vector2 direction, float distance)
    {
        int pushBackCount = 3; // Chia nhỏ ra đẩy tạo hiệu ứng đẩy mượt hơn
        baseEnemy.aIPath.canMove = false;
        while(pushBackCount > 0)
        {
            pushBackCount -= 1;
            // Kiểm tra hướng đẩy có dính tường hay water ko
            if(!Physics2D.Raycast(transform.position,direction,distance/3,LayerMask.GetMask("Wall")+LayerMask.GetMask("Water")))
            {
                transform.position += distance/3 * (Vector3)direction.normalized;
            }
            yield return null;
        }
        ReturnMovementForEnemy();
        baseEnemy.aIPath.canMove = true;
        currentPushCoroutine = null;
    }
    public void StartStunCoroutine(float stunTime)
    {
        if(currentStunCoroutine != null)
        {
            StopCoroutine(currentStunCoroutine);
            holdMovementCount -= 1;
        }
        currentStunCoroutine = StartCoroutine(StunIEnum(stunTime));
        holdMovementCount +=1;
    }
    public virtual IEnumerator StunIEnum(float stunTime)
    {
        baseEnemy.enabled = false;
        baseEnemy.aIPath.canMove = false;
        Destroy(Instantiate(stunIcon,new Vector2(transform.position.x,transform.position.y + 1.2f),Quaternion.identity),stunTime);
        while (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            yield return null;
        }
        ReturnMovementForEnemy();
        baseEnemy.enabled = true;
        currentStunCoroutine = null;
    }
    void ReturnMovementForEnemy()
    {
        if(holdMovementCount ==1){
            baseEnemy.aIPath.canMove = true;
            holdMovementCount -=1;
        }
    }
}
