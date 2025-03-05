using System.Collections;
using Pathfinding;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, ICanSelect, IGetHit, IPushable
{
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] GameObject selectionCircle;
    [HideInInspector] public Transform target;
    public delegate void enemyBehaviour();
    public enemyBehaviour currentBehaviour;
    protected GameObject nearestPlayer;
    [SerializeField] Material material;
    float currentHealth;
    Renderer rendererEnemy;
    MaterialPropertyBlock propertyBlock;
    AIPath aIPath;
    protected void Awake()
    {
        currentHealth = enemyData.health;
        rendererEnemy = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
        aIPath = GetComponent<AIPath>();
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
        StartCoroutine(Blink());
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
    public void PushBack(Vector2 direction,float distance)
    {
        StartCoroutine(PushBackIEnum(direction,distance));
    }
    public virtual IEnumerator PushBackIEnum(Vector2 direction, float distance)
    {
        float timePushBack = 0.15f;
        aIPath.enabled = false;
        while(timePushBack > 0)
        {
            timePushBack -= Time.fixedDeltaTime;
            // Kiểm tra hướng đẩy có dính tường hay water ko
            if(!Physics2D.Raycast(transform.position,direction,distance*Time.deltaTime/timePushBack,LayerMask.GetMask("Wall")+LayerMask.GetMask("Water")))
            {
                transform.position += distance*Time.deltaTime/timePushBack * (Vector3)direction.normalized; 
            }
            yield return null;
        }
        aIPath.enabled = true;
    }
}
