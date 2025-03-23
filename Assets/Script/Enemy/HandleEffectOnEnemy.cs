using System.Collections;
using UnityEngine;

public class HandleEffectOnEnemy : MonoBehaviour,ICanStun,IPushable
{
    [SerializeField] GameObject stunIcon;
    EnemyBrain enemyBrain;
    int pushBackCount; // Chia nhỏ khoảng cách đẩy cho mỗi frame để mượt hơn
    float stunTimeRemain;
    void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    public void StartPushCoroutine(Vector2 direction,float distance)
    {
        if(pushBackCount == 0) // Nếu = 0 là ko có coroutine nào đang đẩy
        {
            pushBackCount = 10;
            StartCoroutine(PushBackIEnum(direction,distance));
        }
        pushBackCount = 10; // Nếu có thì chỉ cần đặt lại số lần đẩy
    }
    public virtual IEnumerator PushBackIEnum(Vector2 direction, float distance)
    {
        while(pushBackCount > 0)
        {
            pushBackCount -= 10;
            enemyBrain.aIPath.canMove = false;
            // Kiểm tra hướng đẩy có dính tường hay water ko
            if(!Physics2D.Raycast(transform.position,direction,distance/3,LayerMask.GetMask("Wall")+LayerMask.GetMask("Water")))
            {
                transform.position += distance/10 * (Vector3)direction.normalized;
            }
            yield return null;
        }
        enemyBrain.aIPath.canMove = true;
    }
    public void StartStunCoroutine(float stunTime)
    {
        if(stunTimeRemain <= 0) // Nếu < 0 là ko có coroutine nào đang stun
        {
            stunTimeRemain = stunTime;
            StartCoroutine(StunIEnum());
        } 
        pushBackCount = 10; // Nếu có thì chỉ cần đặt lại số lần đẩy
    }
    public virtual IEnumerator StunIEnum()
    {
        GameObject newIcon = Instantiate(stunIcon,new Vector2(transform.position.x,transform.position.y + 1.2f),Quaternion.identity);
        while (stunTimeRemain >0)
        {
            stunTimeRemain -= Time.deltaTime;
            enemyBrain.enabled = false;
            enemyBrain.aIPath.canMove = false;
            yield return null;
        }
        Destroy(newIcon);
        enemyBrain.enabled = true;
        enemyBrain.aIPath.canMove = true;
    }
}
