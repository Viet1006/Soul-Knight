using DG.Tweening;
using UnityEngine;

public class HandleEffectOnEnemy : MonoBehaviour,ICanStun,IPushable , ICanPoison , ICanFrozen , ICanBurn , ICanVulnerability
{
    Tween burnTween , poisonTween , frozenTween , stunTween , vulnerabilityTween;
    EnemyController enemyController;
    void Awake()
    {
        enemyController = GetComponent<EnemyController>();
    }
    // bắt đầu đẩy
    public void StartPush(Vector2 direction,float distance)
    {
        if(enemyController.moveToStatus) enemyController.moveToStatus.StopMove();
        DOVirtual.DelayedCall(0.02f , () => 
            {
                if(!Physics2D.Raycast(transform.position,direction,distance/5,LayerMask.GetMask("Wall")+LayerMask.GetMask("Water")))
                    transform.position += distance/5 * (Vector3)direction;
            }).SetLoops(5)
            .OnKill(() => 
            {
                if(enemyController.moveToStatus) enemyController.moveToStatus.ContinueMove();
            });
    }
    // Bắt đầu chạy Stun
    public void StartStun(float stunTime)
    {
        stunTween.Kill();
        SpriteRenderer effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f),BuffIconEnum.Stun,transform);
        if(enemyController.moveToStatus) enemyController.moveToStatus.StopMove();
        if(enemyController.attackMethod) enemyController.attackMethod.StopAttack();
        stunTween = DOVirtual.DelayedCall(stunTime , () =>{})
            .OnKill(() => 
                {
                    if(enemyController.moveToStatus) enemyController.moveToStatus.ContinueMove();
                    if(enemyController.attackMethod) enemyController.attackMethod.ContinueAttack();
                    IconEffectPool.Instance.ReTurnToPool(effectIcon);
                });
    }
    // Bắt đầu Fronzen
    public void StartFrozen(float frozenTime) 
    {
        frozenTween.Kill();
        SpriteRenderer effectIcon = IconEffectPool.Instance.GetIconEffect(Vector2.zero,BuffIconEnum.Frozen,transform);
        if(enemyController.moveToStatus) enemyController.moveToStatus.StopMove();
        if(enemyController.attackMethod) enemyController.attackMethod.StopAttack();
        frozenTween = DOVirtual.DelayedCall(frozenTime , () =>{})
            .OnKill(() => 
                {
                    if(enemyController.moveToStatus) enemyController.moveToStatus.ContinueMove();
                    if(enemyController.attackMethod) enemyController.attackMethod.ContinueAttack();
                    IconEffectPool.Instance.ReTurnToPool(effectIcon);
                });
    }
    // Bắt đầu chạy Poison
    public void StartPoison(int damageHalfSecond, float poisonTime)
    {
        poisonTween.Kill();
        SpriteRenderer effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f), BuffIconEnum.Poison, transform);
        poisonTween = DOVirtual.DelayedCall(0.5f, () => enemyController.GetHit(damageHalfSecond, BulletElement.Poison,false))
            .SetLoops(Mathf.FloorToInt(poisonTime *2 ))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(effectIcon));
    }
    // Bắt đầu chạy Burn
    public void StartBurn(int damageHalfSecond, float burnTime)
    {
        burnTween.Kill();
        SpriteRenderer effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f), BuffIconEnum.Burn, transform);
        burnTween = DOVirtual.DelayedCall(0.5f, () => enemyController.GetHit(damageHalfSecond, BulletElement.Fire,false))
            .SetLoops(Mathf.FloorToInt(burnTime*2))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(effectIcon));
    }
    public void StartVulnerability(int additionDamage , float effectDuration)
    {
        vulnerabilityTween.Kill();
        void action() => enemyController.GetHit(additionDamage, BulletElement.Lightning, false);
        SpriteRenderer effectIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.5f), BuffIconEnum.Vulnerability, transform);
        enemyController.OnGetHit += action;
        vulnerabilityTween = DOVirtual.DelayedCall(effectDuration, () => {})
            .OnKill(() => 
                {
                    IconEffectPool.Instance.ReTurnToPool(effectIcon);
                    enemyController.OnGetHit -= action;
                });
    }
    protected virtual void OnDisable()
    {
        burnTween.Kill();
        poisonTween.Kill();
        frozenTween.Kill();
        stunTween.Kill();
        vulnerabilityTween.Kill();
    }
}