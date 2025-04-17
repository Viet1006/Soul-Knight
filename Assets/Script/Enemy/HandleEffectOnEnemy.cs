using System.Collections;
using UnityEngine;

public class HandleEffectOnEnemy : MonoBehaviour,ICanStun,IPushable , ICanPoison , ICanFrozen , ICanBurn
{
    int pushBackCount ; // Chia nhỏ khoảng cách đẩy cho mỗi frame để mượt hơn
    float frozenTime , stunTime ,burnTime , poisonTime; 
    EnemyController enemyController;
    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    // bắt đầu đẩy
    public void StartPush(Vector2 direction,float distance)
    {
        if(pushBackCount == 0) // Nếu = 0 là ko có coroutine nào đang đẩy
        {
            pushBackCount = 5;
            StartCoroutine(PushBackCoroutine(direction,distance));
        }
        pushBackCount = 5; // Nếu có thì chỉ cần đặt lại số lần đẩy
    }
    public virtual IEnumerator PushBackCoroutine(Vector2 direction, float distance)
    {
        while(pushBackCount > 0)
        {
            pushBackCount -= 1;
            enemyController.moveToStatus.StopMove();
            // Kiểm tra hướng đẩy có dính tường hay water ko
            if(!Physics2D.Raycast(transform.position,direction,distance/3,LayerMask.GetMask("Wall")+LayerMask.GetMask("Water")))
            {
                transform.position += distance/10 * (Vector3)direction.normalized;
            }
            yield return null;
        }
        enemyController.moveToStatus.ContinueMove();
    }
    // Bắt đầu chạy Poison Coroutine
    public void StartPoison(int damagePerSecond, float poisonTime)
    {
        if(this.poisonTime <= 0)
        {
            this.poisonTime = poisonTime;
            StartCoroutine(PoisonCoroutine(damagePerSecond));
        }
        this.poisonTime = poisonTime;
    }
    public virtual IEnumerator PoisonCoroutine(int damagePerSecond)
    {
        GameObject newIcon = IconEffectPool.instance.GetIconEffect(new Vector2(0,1.2f),BulletElement.Poison,transform);
        float poisonRate = 0; //Thời gian 2 lần đốt
        while (poisonTime >0)
        {
            poisonTime -= Time.deltaTime;
            poisonRate += Time.deltaTime;
            if(poisonRate >= 1) // 1s đốt 1 lần
            {
                enemyController.GetHit(damagePerSecond , BulletElement.Poison);
                poisonRate = 0;
            }
            yield return null;
        }
        IconEffectPool.instance.ReTurnToPool(newIcon);
    }
    // Bắt đầu chạy Stun Coroutine
    public void StartStun(float stunTime)
    {
        if(this.stunTime <= 0)
        {
            this.stunTime = stunTime;
            StartCoroutine(StunCoroutine());
        }
        this.stunTime = stunTime;
    }
    public virtual IEnumerator StunCoroutine()
    {
        GameObject newIcon = IconEffectPool.instance.GetIconEffect(new Vector2(0,1.2f),BulletElement.Lightning,transform);
        while (stunTime >=0)
        {
            stunTime -= Time.deltaTime;
            enemyController.moveToStatus.StopMove();
            enemyController.attackMethod.StopAttack();
            yield return null;
        }
        IconEffectPool.instance.ReTurnToPool(newIcon);
        enemyController.moveToStatus.ContinueMove();
        enemyController.attackMethod.ContinueAttack();
    }
    // Bắt đầu FronzenCoroutine
    public void StartFrozen(float frozenTime) 
    { 
        if(this.frozenTime <= 0)
        {
            this.frozenTime = frozenTime;
            StartCoroutine(FrozenCoroutine());
        }
        this.frozenTime = frozenTime;
    }
    public virtual IEnumerator FrozenCoroutine()
    {
        GameObject newIcon = IconEffectPool.instance.GetIconEffect(Vector2.zero,BulletElement.Frozen,transform);
        while (frozenTime >0)
        {
            frozenTime -= Time.deltaTime;
            enemyController.moveToStatus.StopMove();
            enemyController.attackMethod.StopAttack();
            yield return null;
        }
        IconEffectPool.instance.ReTurnToPool(newIcon);
        enemyController.moveToStatus.ContinueMove();
        enemyController.attackMethod.ContinueAttack();
    }
    // Bắt đầu chạy Burn Coroutine
    public void StartBurn(int damagePerSecond, float burnTime)
    {
        if(this.burnTime <= 0)
        {
            this.burnTime = burnTime;
            StartCoroutine(BurnCoroutine(damagePerSecond));
        }
        this.burnTime = burnTime;
    }
    public virtual IEnumerator BurnCoroutine(int damagePerSecond)
    {
        GameObject newIcon = IconEffectPool.instance.GetIconEffect(new Vector2(0,1.2f),BulletElement.Fire,transform);
        float burnRate = 0; //Thời gian 2 lần đốt
        while (burnTime >0)
        {
            burnTime -= Time.deltaTime;
            burnRate += Time.deltaTime;
            if(burnRate >= 1) // 1s đốt 1 lần
            {
                enemyController.GetHit(damagePerSecond , BulletElement.Fire);
                burnRate = 0;
            }
            yield return null;
        }
        IconEffectPool.instance.ReTurnToPool(newIcon);
    }
    public void EndAllEffect()
    {
        poisonTime = 0;
        stunTime = 0;
        frozenTime = 0;
        pushBackCount = 0;
    }
}