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
    int currentHealth;
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
    public void GetHit(int damage, BulletElement bulletElement , bool notify =true)
    {
        TextDamePool.Instance.GetTextDamage( transform.position + new Vector3(0, 1f, 0),bulletElement,damage);
        currentHealth -= damage;
        OnHealthChange?.Invoke(currentHealth);
        if(currentHealth <=0)
        {
            playerCollider.enabled = false;
            playerMovement.canMove = false;
            playerBehaviour.enabled = false;
            StartCoroutine(DieEffect());
            playerMovement.animator.SetTrigger(Parameters.die);
            return;
        }
        StartCoroutine(Blink());
    }
    IEnumerator DieEffect()
    {
        float distanceUp = 0.5f;
        Vector2 targetPos = (Vector2)transform.position + new Vector2(0,distanceUp);
        while (Vector2.Distance(transform.position,targetPos) > 0.1f)
        {
            transform.position += 2 * Time.deltaTime * Vector3.up;
            yield return null;
        }
        while (Vector2.Distance(transform.position,targetPos-new Vector2(0,distanceUp)) > 0.1f)
        {
            transform.position -= 2 * Time.deltaTime * Vector3.up;
            yield return null;
        }
    }
    IEnumerator Blink()
    {
        playerCollider.contactCaptureLayers -= LayerMask.GetMask("Enemy Bullet"); // xóa Enemy Bullet khỏi callback
        spritePlayer.material = ObjectHolder.Instance.flashMaterial; // Làm trắng
        yield return new WaitForSeconds(0.2f);
        spritePlayer.material = ObjectHolder.Instance.defaultMaterial; // Trả về bình thường
        playerCollider.contactCaptureLayers += LayerMask.GetMask("Enemy Bullet"); // Thêm Enemy Bullet callback
    }
    public void StartPush(Vector2 direction, float distance)
    {
        playerMovement.SetVelocity(distance*HeroData.acceleration*10/6 * direction);  // Đảm bảo đẩy đúng khoảng cách , công thức được rút ra từ quá trình test
    }
    public void StartFrozen(float frozenTime)
    {
        GameObject frozenIcon = IconEffectPool.Instance.GetIconEffect(Vector2.zero,BulletBuffType.Frozen,transform);
        playerMovement.canMove = false; moveBlock += 1;
        playerBehaviour.enabled = false; attackBlock += 1;
        frozenTween.Kill();
        frozenTween = DOVirtual.DelayedCall(frozenTime , () =>
            {
                moveBlock -= 1;
                if(moveBlock <= 0)  playerMovement.canMove = true;
                attackBlock -= 1;
                if(attackBlock <= 0) playerBehaviour.enabled = true;
                
            }).OnKill(() => IconEffectPool.Instance.ReTurnToPool(frozenIcon));
    }
    public void StartStun(float stunTime)
    {
        GameObject stunIcon = IconEffectPool.Instance.GetIconEffect(Vector2.zero,BulletBuffType.Stun,transform);
        playerMovement.canMove = false; moveBlock += 1;
        playerBehaviour.enabled = false; attackBlock += 1;
        stunTween.Kill();
        stunTween = DOVirtual.DelayedCall(stunTime , () =>
            {
                moveBlock -= 1;
                if(moveBlock <= 0)  playerMovement.canMove = true;
                attackBlock -= 1;
                if(attackBlock <= 0) playerBehaviour.enabled = true;
                
            }).OnKill(() => IconEffectPool.Instance.ReTurnToPool(stunIcon));
    }
    public void StartPoison(int damagePerSecond, float poisonTime)
    {
        poisonTween.Kill();
        GameObject poisonIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.2f), BulletBuffType.Poison, transform);
        poisonTween = DOVirtual.DelayedCall(1, () => GetHit(damagePerSecond, BulletElement.Poison))
            .SetLoops(Mathf.FloorToInt(poisonTime))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(poisonIcon));
    }
    public void StartBurn(int damagePerSecond, float burnTime)
    {
        burnTween.Kill();
        GameObject burnIcon = IconEffectPool.Instance.GetIconEffect(new Vector2(0, 1.2f), BulletBuffType.Burn, transform);
        burnTween = DOVirtual.DelayedCall(1, () => GetHit(damagePerSecond, BulletElement.Fire))
            .SetLoops(Mathf.FloorToInt(burnTime))
            .OnKill(() => IconEffectPool.Instance.ReTurnToPool(burnIcon));
    }
}