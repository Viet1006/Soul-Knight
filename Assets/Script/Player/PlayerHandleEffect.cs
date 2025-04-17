using System.Collections;
using UnityEngine;

public class PlayerHandleEffect : MonoBehaviour, IPushable, ICanStun, IGetHit , ICanPoison , ICanFrozen
{
    [SerializeField] PlayerBehaviour playerBehaviour;
    [SerializeField] PlayerMovement playerMovement;
    public HeroData heroData;
    public delegate void OnHealthChangeDelegate(int damage);
    public event OnHealthChangeDelegate OnHealthChange;
    int currentHealth;
    SpriteRenderer spritePlayer;
    Collider2D playerCollider;
    public void Awake()
    {
        GetComponent<Animator>().runtimeAnimatorController = heroData.animatorController;
        currentHealth = heroData.health;
        playerMovement.speed = heroData.speed;
        playerCollider = GetComponent<Collider2D>();
        spritePlayer = GetComponent<SpriteRenderer>();
    }
    public void GetHit(int damage, BulletElement bulletElement)
    {
        TextDamePool.instance.GetTextDamage( transform.position + new Vector3(0, 1f, 0),bulletElement,damage);
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
        spritePlayer.material = ObjectHolder.instance.flashMaterial; // Làm trắng
        yield return new WaitForSeconds(0.2f);
        spritePlayer.material = ObjectHolder.instance.defaultMaterial; // Trả về bình thường
        playerCollider.contactCaptureLayers += LayerMask.GetMask("Enemy Bullet"); // Thêm Enemy Bullet callback
    }
    public void StartPush(Vector2 direction, float distance)
    {
        playerMovement.SetVelocity(distance*HeroData.acceleration*10/6 * direction);  // Đảm bảo đẩy đúng khoảng cách
    }
    public void StartFrozen(float frozenTime) => StartCoroutine(FrozenCoroutine(frozenTime));
    IEnumerator FrozenCoroutine(float frozenTime)
    {
        GameObject newIcon = IconEffectPool.instance.GetIconEffect(Vector2.zero,BulletElement.Frozen,transform);
        while (frozenTime >0)
        {
            frozenTime -= Time.deltaTime;
            playerMovement.canMove = false;
            playerBehaviour.enabled = false;
            yield return null;
        }
        IconEffectPool.instance.ReTurnToPool(newIcon);
        playerMovement.canMove = true;
        playerBehaviour.enabled = true;
    }
    public void StartStun(float stunTime) => StartCoroutine(StunCoroutine(stunTime));
    IEnumerator StunCoroutine(float stunTime)
    {
        GameObject newIcon = IconEffectPool.instance.GetIconEffect(new Vector2(0,1.2f),BulletElement.Lightning,transform);
        while (stunTime >0)
        {
            stunTime -= Time.deltaTime;
            playerMovement.canMove = false;
            playerBehaviour.enabled = false;
            yield return null;
        }
        IconEffectPool.instance.ReTurnToPool(newIcon);
        playerMovement.canMove = true;
        playerBehaviour.enabled = true;
    }
    float poisonTime;
    public void StartPoison(int damagePerSecond, float poisonTime)
    {
        if(this.poisonTime == 0)
        {
            this.poisonTime = poisonTime;
            StartCoroutine(PoisonCoroutine(damagePerSecond));
        }
        this.poisonTime = poisonTime;
    }
    IEnumerator PoisonCoroutine(int damagePerSecond)
    {
        GameObject newIcon = IconEffectPool.instance.GetIconEffect(new Vector2(0,1.2f),BulletElement.Poison,transform);
        while (poisonTime >0)
        {
            yield return new WaitForSeconds(1);
            poisonTime -= 1;
            GetHit(damagePerSecond , BulletElement.Poison);
        }
        IconEffectPool.instance.ReTurnToPool(newIcon);
    }
}