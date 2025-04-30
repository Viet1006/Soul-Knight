using DG.Tweening;
using UnityEngine;

public class HandleEffectOnEnemy : MonoBehaviour,ICanStun,IPushable , ICanPoison , ICanFrozen , ICanBurn , ICanVulnerability
{
    Tween burnTween , poisonTween , frozenTween , stunTween , vulnerabilityTween;
    int moveBlock , attackBlock;
    EnemyController enemyController;
    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    // bắt đầu đẩy
    public void StartPush(Vector2 direction,float distance)
    {
        DOVirtual.DelayedCall(0.02f , () => 
            {
                if(!Physics2D.Raycast(transform.position,direction,distance/5,LayerMask.GetMask("Wall")+LayerMask.GetMask("Water") ))
                    transform.position += distance/5 * (Vector3)direction.normalized;
            }).SetLoops(5);
    }
    // Bắt đầu chạy Stun
    public void StartStun(float stunTime)
    {
        stunTween.Kill();
        GameObject effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f),BulletBuffType.Stun,transform);
        enemyController.moveToStatus.StopMove();  moveBlock += 1;
        enemyController.attackMethod.StopAttack(); attackBlock += 1;
        stunTween = DOVirtual.DelayedCall(stunTime , () =>{  })
            .OnKill(() => 
                {
                    moveBlock -= 1;
                    if(moveBlock <= 0) enemyController.moveToStatus.ContinueMove();
                    attackBlock -= 1;
                    if(attackBlock <= 0) enemyController.attackMethod.ContinueAttack();
                    IconEffectPool.Instance.ReTurnToPool(effectIcon);
                });
    }
    // Bắt đầu Fronzen
    public void StartFrozen(float frozenTime) 
    {
        frozenTween.Kill();
        GameObject effectIcon = IconEffectPool.Instance.GetIconEffect(Vector2.zero,BulletBuffType.Frozen,transform);
        enemyController.moveToStatus.StopMove();  moveBlock += 1;
        enemyController.attackMethod.StopAttack(); attackBlock += 1;
        frozenTween = DOVirtual.DelayedCall(frozenTime , () =>{})
            .OnKill(() => 
                {
                    moveBlock -= 1;
                    if(moveBlock <= 0) enemyController.moveToStatus.ContinueMove();
                    attackBlock -= 1;
                    if(attackBlock <= 0 ) enemyController.attackMethod.ContinueAttack();
                    IconEffectPool.Instance.ReTurnToPool(effectIcon);
                });
    }
    // Bắt đầu chạy Poison
    public void StartPoison(int damageHalfSecond, float poisonTime)
    {
        poisonTween.Kill();
        GameObject effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f), BulletBuffType.Poison, transform);
        poisonTween = DOVirtual.DelayedCall(0.5f, () => enemyController.GetHit(damageHalfSecond, BulletElement.Poison,false))
            .SetLoops(Mathf.FloorToInt(poisonTime *2 ))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(effectIcon));
    }
    // Bắt đầu chạy Burn
    public void StartBurn(int damageHalfSecond, float burnTime)
    {
        burnTween.Kill();
        GameObject effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f), BulletBuffType.Burn, transform);
        burnTween = DOVirtual.DelayedCall(0.5f, () => enemyController.GetHit(damageHalfSecond, BulletElement.Fire,false))
            .SetLoops(Mathf.FloorToInt(burnTime*2))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(effectIcon));
    }
    public void StartVulnerability(int additionDamage , float effectDuration)
    {
        vulnerabilityTween.Kill();
        void action() => enemyController.GetHit(additionDamage, BulletElement.Lightning, false);
        GameObject effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f), BulletBuffType.Vulnerability, transform);
        enemyController.OnGetHit += action;
        enemyController.moveToStatus.StopMove();  moveBlock += 1;
        enemyController.attackMethod.StopAttack(); attackBlock += 1;
        vulnerabilityTween = DOVirtual.DelayedCall(effectDuration, () => {})
            .OnKill(() => 
                {
                    IconEffectPool.Instance.ReTurnToPool(effectIcon);
                    moveBlock -= 1;
                    if(moveBlock <= 0) enemyController.moveToStatus.ContinueMove();
                    attackBlock -= 1;
                    if(attackBlock <= 0) enemyController.attackMethod.ContinueAttack();
                    enemyController.OnGetHit -= action;
                });
    }
    public void EndAllEffect()
    {
        burnTween.Kill();
        poisonTween.Kill();
        frozenTween.Kill();
        stunTween.Kill();
        vulnerabilityTween.Kill();
        moveBlock = 0;
        attackBlock = 0;
    }
}