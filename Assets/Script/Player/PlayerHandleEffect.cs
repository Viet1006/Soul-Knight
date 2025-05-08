using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerHandleEffect : MonoBehaviour, IPushable, ICanStun, IGetHit , ICanPoison , ICanFrozen , ICanBurn
{
    [SerializeField] PlayerBehaviour playerBehaviour;
    [SerializeField] PlayerMovement playerMovement;
    [InlineEditor]
    public HeroData heroData;
    public event System.Action<int> OnHealthChange;
    public int currentHealth;
    int moveBlock , attackBlock;
    SpriteRenderer spritePlayer;
    Collider2D playerCollider;
    Tween frozenTween , stunTween , burnTween , poisonTween;
    public void Awake()
    {
        currentHealth = heroData.health;
        playerMovement.speed = heroData.speed;
        playerCollider = GetComponent<Collider2D>();
        spritePlayer = GetComponent<SpriteRenderer>();
}
    public void GetHit(int damage, BulletElement bulletElement , bool isCrit = false, bool notify =true)
    {
        TextDamePool.Instance.GetTextDamage( transform.position + new Vector3(0, 1f, 0),bulletElement,damage , isCrit);
        currentHealth -= damage;
        OnHealthChange?.Invoke(currentHealth);
        HealthBar.instance.SetHealth(currentHealth);
        if(currentHealth <=0)
        {
            HealthBar.instance.SetHealth(0);
            playerCollider.enabled = false;
            playerMovement.canMove = false;
            playerBehaviour.enabled = false;
            playerMovement.animator.SetTrigger(Parameters.die);
            Status.instance.GetDamage(30); // thua games
            return;
        }
        StartCoroutine(Blink());
    }
    IEnumerator Blink()
    {
        playerCollider.contactCaptureLayers -= LayerMask.GetMask("Enemy Bullet"); // xóa Enemy Bullet khỏi callback
        spritePlayer.material = ObjectHolder.Instance.flashMaterial; // Làm trắng
        yield return new WaitForSeconds(0.2f);
        spritePlayer.material = ObjectHolder.Instance.defaultMaterial; // Trả về bình thường
        playerCollider.contactCaptureLayers += LayerMask.GetMask("Enemy Bullet"); // Thêm Enemy Bullet callback
    }
    public void StartPush(Vector2 direction, float forcePush)
    {
        playerMovement.SetVelocity(forcePush*HeroData.acceleration*10/6 * direction);  // Đảm bảo đẩy đúng khoảng cách , công thức được rút ra từ quá trình test
    }
    public void StartFrozen(float frozenTime)
    {
        SpriteRenderer frozenIcon = IconEffectPool.Instance.GetIconEffect(Vector2.zero,BuffIconEnum.Frozen,transform);
        BlockMove(); BlockAttack();
        frozenTween.Kill();
        frozenTween = DOVirtual.DelayedCall(frozenTime , () =>
            {
                UnBlockMove(); UnBlockAttack();
            },false).OnKill(() => IconEffectPool.Instance.ReTurnToPool(frozenIcon));
    }
    public void StartStun(float stunTime)
    {
        SpriteRenderer stunIcon = IconEffectPool.Instance.GetIconEffect(Vector2.zero,BuffIconEnum.Stun,transform);
        BlockMove(); BlockAttack();
        stunTween.Kill();
        stunTween = DOVirtual.DelayedCall(stunTime , () =>
            {
                UnBlockMove(); UnBlockAttack();
            },false).OnKill(() => IconEffectPool.Instance.ReTurnToPool(stunIcon));
    }
    public void StartPoison(int damagePerSecond, float poisonTime)
    {
        poisonTween.Kill();
        SpriteRenderer poisonIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.2f), BuffIconEnum.Poison, transform);
        poisonTween = DOVirtual.DelayedCall(1, () => GetHit(damagePerSecond, BulletElement.Poison),false)
            .SetLoops(Mathf.FloorToInt(poisonTime))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(poisonIcon));
    }
    public void StartBurn(int damagePerSecond, float burnTime)
    {
        burnTween.Kill();
        SpriteRenderer burnIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.2f), BuffIconEnum.Burn, transform);
        burnTween = DOVirtual.DelayedCall(1, () => GetHit(damagePerSecond, BulletElement.Fire),false)
            .SetLoops(Mathf.FloorToInt(burnTime))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(burnIcon));
    }
    void BlockAttack()
    {
        playerBehaviour.StopAttack(); attackBlock += 1;
        playerBehaviour.enabled = false;
    }
    void BlockMove()
    {
        playerMovement.canMove = false; moveBlock += 1;
    }
    void UnBlockMove()
    {
        moveBlock -= 1;
        if(moveBlock <= 0)  playerMovement.canMove = true;
    }
    void UnBlockAttack()
    {
        attackBlock -= 1;
        if(attackBlock <= 0) playerBehaviour.enabled = true;
    }
}