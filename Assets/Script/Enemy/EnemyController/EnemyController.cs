//using DG.Tweening;
using DG.Tweening;
using UnityEngine;
public class EnemyController : MonoBehaviour, IGetHit , ICanSelect
{
    [Sirenix.OdinInspector.InlineEditor] public EnemyData enemyData;
    [SerializeField] GameObject selectionCircle;
    Collider2D colliderEnemy;
    int currentHealth;
    SpriteRenderer spriteEnemy;
    [HideInInspector] public MoveToStatus moveToStatus;
    [HideInInspector] public AttackMethodEnemy attackMethod;
    public event System.Action OnResetToOringin;
    public event System.Action OnGetHit;
    Tween flashTween;
    void Awake() // Lấy tham chiếu để gọi Init
    {
        moveToStatus = GetComponent<MoveToStatus>();
        attackMethod = GetComponent<AttackMethodEnemy>(); // nếu RandomWeapon sẽ được gán ở InitEnemy
        colliderEnemy = GetComponent<Collider2D>();
        spriteEnemy = GetComponent<SpriteRenderer>();
        currentHealth = enemyData.health;
        moveToStatus.SetSpeed(enemyData.speed); // Gọi sau Init để lấy được data
        moveToStatus.OnTarget += ResetToOringin; // Đăng ký sự kiện khi đến đích thì reset về origin sau đó trả về pool
        flashTween = DOVirtual.DelayedCall(0.1f,()=> spriteEnemy.material = ObjectHolder.Instance.defaultMaterial )
            .SetAutoKill(false)
            .Pause();
    }
    public virtual int InitEnemy() // Sẽ được gọi trước Start
    {
        attackMethod.OnInit();
        currentHealth = enemyData.health;
        return enemyData.cost;
    }
    public void GetHit(int damage, BulletElement bulletElements , bool notify = true) // Sát thương nhận thêm từ đạn
    {
        TextDamePool.Instance.GetTextDamage( transform.position + new Vector3(0, 1f, 0),bulletElements,damage);
        currentHealth -= damage;
        if(currentHealth <=0)
        {
            Die();
            flashTween.Pause();
            return;
        }
        if(notify) OnGetHit?.Invoke();
        spriteEnemy.material = ObjectHolder.Instance.flashMaterial;
        flashTween.Rewind(); // Đưa tween về lại từ đầu
        flashTween.Play();
    }
    void Die()
    {
        DieEffectPool.Instance.GetDieEffect(transform.position); // Tạo die effect
        CoinPool.Instance.GetCoin(transform.position,(int)(Random.Range(0.8f,1.2f)*enemyData.cost )); // Tạo coin rơi ra giá trị ngẫu nhiên
        ResetToOringin();
    }
    public virtual void ResetToOringin()
    {
        OnResetToOringin?.Invoke();
        OnResetToOringin = null; // Khi trả về pool thì hủy đăng ký để trả lại trạng thái ban đầu
        moveToStatus.ContinueMove(); // trả lại trạng thái trước khi cho vào pool
        attackMethod.ContinueAttack(); // trả lại trạng thái trước khi cho vào pool
        spriteEnemy.material = ObjectHolder.Instance.defaultMaterial;
        colliderEnemy.enabled = true; 
        GetComponent<HandleEffectOnEnemy>().EndAllEffect();
        ManageSpawnEnemy.instance.ReturnToPool(gameObject); // trả về pool
        OnGetHit = null;
    }
    public void ShowSelectObject() => selectionCircle.SetActive(true);
    public void HideSelectObject() => selectionCircle.SetActive(false);
}
