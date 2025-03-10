using System.Collections;
using Pathfinding;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, ICanSelect, IGetHit
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] GameObject selectionCircle;
    [HideInInspector] public Transform target;
    Animator animator;
    Collider2D colliderEnemy;
    public delegate void enemyBehaviour();
    public enemyBehaviour currentBehaviour;
    protected GameObject nearestPlayer;
    [SerializeField] Material material;
    float currentHealth;
    Renderer rendererEnemy;
    MaterialPropertyBlock propertyBlock;
    public AIPath aIPath;
    protected void Awake()
    {
        currentHealth = enemyData.health;
        rendererEnemy = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        aIPath = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        colliderEnemy = GetComponent<Collider2D>();
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
        if (target == null) return;
        if (target.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (target.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }
    protected abstract void Attack(Vector2 Target);
}
