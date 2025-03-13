using System.Collections;
using Pathfinding;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, ICanSelect, IGetHit
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] GameObject selectionCircle;
    Animator animator;
    Collider2D colliderEnemy;
    public delegate void enemyBehaviour();
    public enemyBehaviour currentBehaviour;
    protected GameObject nearestPlayer;
    protected Transform target; // Giữ tạm vị trí để phòng khi đang tấn công thì ko thấy player và lỗi khi xuay về phía player
    [SerializeField] Material material;
    float currentHealth;
    Renderer rendererEnemy;
    MaterialPropertyBlock propertyBlock;
    public AIPath aIPath;
    protected bool isAttacking;
    float timeToNextAttack;
    protected void Awake()
    {
        currentHealth = enemyData.health;
        rendererEnemy = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        aIPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        colliderEnemy = GetComponent<Collider2D>();
        timeToNextAttack = 1/enemyData.AttackRate;
    }
    public void ShowSelectObject()
    {
        selectionCircle.SetActive(true);
    }
    public void HideSelectObject()
    {
        selectionCircle.SetActive(false);
    }
    public virtual void GetHit(float damage, Color colorDamage)
    {
        GameObject newTextDamage = Instantiate(enemyData.textDamage, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        newTextDamage.GetComponent<TextDamage>().SetText(damage, colorDamage);
        currentHealth -= damage;
        if(currentHealth <=0)
        {
            Die();
            return;
        } 
        StartCoroutine(Blink());
    }
    public virtual void Die()
    {
        animator.SetTrigger(Parameters.die);
        aIPath.enabled = false;
        colliderEnemy.enabled = false;
        Destroy(gameObject,1f);
        enabled = false;
    }
    IEnumerator Blink()
    {
        rendererEnemy.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_FlashAmount", 1f);  // Làm trắng
        rendererEnemy.SetPropertyBlock(propertyBlock);
        yield return new WaitForSeconds(0.05f);
        propertyBlock.SetFloat("_FlashAmount", 0f);  // Trở lại bình thường
        rendererEnemy.SetPropertyBlock(propertyBlock);
    }
    public void FlipToTarget()
    {
        if (!target) return; // Nếu target đang null thì dừng
        if (target.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (target.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }
    protected abstract void StartAttack( );
    protected virtual void Update()
    {
        nearestPlayer = FindTarget.GetNearestObject(transform.position,enemyData.attackRange,LayerMask.GetMask("Player"));
        if(nearestPlayer) target = nearestPlayer.transform; 
        else if (!isAttacking) target = null; // Khi player ra khỏi vùng tấn công và ko đang tấn công thì mới set target = null
        if(!isAttacking && nearestPlayer) // Nếu ko đang tấn công và có player thì count down thời gian tấn công
        {
            timeToNextAttack-=Time.deltaTime;
            if(timeToNextAttack<=0) // Khi count down hoàn thành thì tấn công
            {
                isAttacking = true;
                StartAttack();
            }
        }
        FlipToTarget();
    }
    public void ResetTimeToAttack() // Reset thời gian tấn công và cho isAttacking = false
    {
        timeToNextAttack = 1/enemyData.AttackRate;
        isAttacking = false;
    }
}
